using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Resources;
using System.ServiceProcess;

namespace FileSystemAndRegistrySnapshots
{
    public static class ScanServices
    {
        public static void Start()
        {
            var folder = $"E:\\Temp\\FileSystemAndRegistrySnapshots";
            var s = SaveServiceInfosIntoFile(folder, Helpers.FakeShowStatus);
        }

        public static string CompareServicesFiles(string firstFile, string secondFile, Action<string> showStatusAction)
        {
            if (!File.Exists(firstFile))
                throw new Exception($"ERROR! The first file '{Path.GetFileName(firstFile)}' doesn't exist'");
            if (!File.Exists(secondFile))
                throw new Exception($"ERROR! The second file '{Path.GetFileName(secondFile)}' doesn't exist'");

            var s = Path.GetFileNameWithoutExtension(firstFile);
            var i1 = s.IndexOf('_');
            var i2 = s.LastIndexOf('_');
            var diskLabel = s.Substring(i1 + 1, i2 - i1 - 1);
            var differenceFileName = Path.Combine(Path.GetDirectoryName(firstFile), $"ServicesDiff_{diskLabel}_{DateTime.Now:yyyyMMddHHmm}.txt");

            showStatusAction($"Parsing the first file ..");
            var data1 = ParseZipScanFile(firstFile); // 1'879'365

            showStatusAction($"Parsing the second file ..");
            var data2 = ParseZipScanFile(secondFile);

            var difference = new Dictionary<string, (string, string)>();
            foreach (var kvp in data1)
            {
                if (data2.ContainsKey(kvp.Key))
                {
                    if (!object.Equals(data1[kvp.Key], data2[kvp.Key]))
                    {
                        difference.Add(kvp.Key, (data1[kvp.Key], data2[kvp.Key]));
                    }
                }
                else 
                    difference.Add(kvp.Key, (data1[kvp.Key], null));
            }
            
            foreach (var kvp in data2)
            {
                if (!data2.ContainsKey(kvp.Key))
                    difference.Add(kvp.Key, (null, data2[kvp.Key]));
            }

            // Save difference
            showStatusAction($"Saving data ..");
            var data = new List<string>();
            data.Add($"Services difference: {Path.GetFileName(firstFile)} and {Path.GetFileName(secondFile)}");
            data.Add("Service\tDifference\tDisplayName1\tDisplayName2\tStatus1\tStatus2\tStartType1\tStartType2");
            data.AddRange(difference.Select(GetDiffLine));
            Helpers.SaveStringsToZipFile(differenceFileName, data);
            
            showStatusAction($"Data saved into {Path.GetFileName(differenceFileName)}");
            return differenceFileName;

            string GetDiffLine(KeyValuePair<string, (string, string)> kvp)
            {
                var ss1 = kvp.Value.Item1 == null ? new string[0] : kvp.Value.Item1.Split('\t');
                var ss2 = kvp.Value.Item2 == null ? new string[0] : kvp.Value.Item2.Split('\t');
                var s1 = ss1.Length > 0 ? ss1[1] : null;
                var s2 = ss2.Length > 0 ? ss2[1] : null;
                var s3 = ss1.Length > 1 ? ss1[2] : null;
                var s4 = ss2.Length > 1 ? ss2[2] : null;
                var s5 = ss1.Length > 2 ? ss1[3] : null;
                var s6 = ss2.Length > 2 ? ss2[3] : null;

                string s0;
                if (ss1.Length == 0) s0 = "Only2";
                else if (ss2.Length == 0) s0 = "Only1";
                else if (ss1.Length > 0 && ss2.Length > 0 && !string.Equals(s1, s2)) s0 = "DisplayName";
                else if (ss1.Length > 1 && ss2.Length > 1 && !string.Equals(s3, s4)) s0 = "Status";
                else if (ss1.Length > 2 && ss2.Length > 2 && !string.Equals(s5, s6)) s0 = "StartType";
                else throw new Exception("Check program!");

                return ($"{kvp.Key}\t{s0}\t{s1}\t{s2}\t{s3}\t{s4}\t{s5}\t{s6}").Trim();
            }
        }

        private static Dictionary<string, string> ParseZipScanFile(string zipFileName)
        {
            var entryName = Path.GetFileNameWithoutExtension(zipFileName) + ".txt";
            var data = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            using (var zip = ZipFile.Open(zipFileName, ZipArchiveMode.Read))
                foreach (var entry in zip.Entries.Where(a => string.Equals(a.Name, entryName)))
                {
                    var checkHeader = false;
                    foreach (var s in entry.GetLinesOfZipEntry())
                    {
                        if (!checkHeader)
                        {
                            if (!string.Equals(s, "Name\tDisplay name\tStatus\tStart type", StringComparison.InvariantCulture))
                                throw new Exception($"Check header of scan file {Path.GetFileName(zipFileName)}");
                            checkHeader = true;
                            continue;
                        }

                        var i1 = s.IndexOf('\t');
                        var key = i1 == -1 ? s : s.Substring(0, i1);
                        data.Add(key, s);
                    }
                }
            return data;

        }

        public static string SaveServiceInfosIntoFile(string dataFolder, Action<string> showStatusAction)
        {
            if (!Directory.Exists(dataFolder)) throw new Exception($"ERROR! Data folder {dataFolder} doesn't exist");

            showStatusAction("Started");

            var log = new List<string> { $"Name\tDisplay name\tStatus\tStart type" };
            var services = ServiceController.GetServices().OrderBy(a=>a.ServiceName).ToArray();
            foreach(var service in services)
                log.Add($"{service.ServiceName}\t{service.DisplayName}\t{service.Status}\t{service.StartType}");

            showStatusAction("Saving data ..");
            var zipFileName = Path.Combine(dataFolder, $"Services_{Helpers.GetSystemDriveLabel()}_{DateTime.Now:yyyyMMddHHmm}.zip");
            Helpers.SaveStringsToZipFile(zipFileName, log);

            showStatusAction($"Data saved into {Path.GetFileName(zipFileName)}");
            return zipFileName;
        }
    }
}
