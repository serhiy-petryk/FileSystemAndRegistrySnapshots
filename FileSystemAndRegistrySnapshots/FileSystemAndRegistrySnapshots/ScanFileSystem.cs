using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FileSystemAndRegistrySnapshots
{
    public static class ScanFileSystem
    {
        private static readonly string[] SkipItems2 = new string[]
        {
            "\\Program Files\\Avast Software\\", "\\ProgramData\\Avast Software\\", "\\ProgramData\\Microsoft\\RAC\\StateData\\",
            "\\AppData\\Local\\Google\\Chrome\\User Data\\", "\\AppData\\Local\\JetBrains\\",
            "\\AppData\\Local\\Microsoft\\VisualStudio\\",
            "\\AppData\\Local\\Microsoft\\Windows\\Temporary Internet Files\\",
            "\\AppData\\Local\\Microsoft\\Windows\\WebCache\\", "\\AppData\\Local\\Psiphon3\\",
            "\\AppData\\Local\\Temp\\","\\AppData\\Roaming\\GitHub Desktop\\", "\\AppData\\Roaming\\Microsoft\\"
        };

        private static readonly string[] SkipItems = new string[]
        {
            "\\Avast Software\\Avast\\", "\\JetBrains\\", "\\Microsoft\\VisualStudio\\", "\\TortoiseHg\\", "\\Psiphon3\\", "\\GitHub Desktop\\",
            "\\ProgramData\\Microsoft\\RAC\\StateData\\", "\\AppData\\Local\\Google\\Chrome\\User Data\\",
            "\\AppData\\Local\\Microsoft\\Windows\\Temporary Internet Files\\",
            "\\AppData\\Local\\Microsoft\\Windows\\WebCache\\",
            "\\AppData\\Local\\Temp\\", "\\AppData\\Roaming\\Microsoft\\"
        };

        public static void Start()
        {
            var zipFileName = $"E:\\Temp\\Reg\\FileSystem_{Helpers.GetSystemDriveLabel()}_{DateTime.Now:yyyyMMddHHmm}.zip";
            SaveFileSystemInfoIntoFile(zipFileName);

            /*var logFileName1 = "E:\\Temp\\Reg\\FileSystem_SSD_240_202411162300.zip";
            var logFileName2 = "E:\\Temp\\Reg\\FileSystem_SSD_240_202411162330.zip";
            // 68 items
            var differenceFileName = CompareScanFiles(logFileName1, logFileName2, SkipItems);*/
        }

        private static string CompareScanFiles(string firstFile, string secondFile, string[] skipKeys)
        {
            var s = Path.GetFileNameWithoutExtension(firstFile);
            var i1 = s.IndexOf('_');
            var i2 = s.LastIndexOf('_');
            var diskLabel = s.Substring(i1 + 1, i2 - i1 - 1);
            var differenceFileName = Path.Combine(Path.GetDirectoryName(firstFile), $"FileDiff_{diskLabel}_{DateTime.Now:yyyyMMddHHmmss}.txt");

            var data1 = ParseZipScanFile(firstFile); // 1'879'365
            var data2 = ParseZipScanFile(secondFile);

            var difference = new Dictionary<string, (string, string)>();
            foreach (var kvp in data1)
            {
                if (data2.ContainsKey(kvp.Key))
                {
                    if (!object.Equals(data1[kvp.Key], data2[kvp.Key]) && !skipKeys.Any(a => kvp.Key.Contains(a, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        difference.Add(kvp.Key, (data1[kvp.Key], data2[kvp.Key]));
                    }
                }
                else if (!skipKeys.Any(a => kvp.Key.Contains(a, StringComparison.InvariantCultureIgnoreCase)))
                    difference.Add(kvp.Key, (data1[kvp.Key], null));
            }

            foreach (var kvp in data2)
            {
                if (!data2.ContainsKey(kvp.Key) && !skipKeys.Any(a => kvp.Key.Contains(a, StringComparison.InvariantCultureIgnoreCase)))
                    difference.Add(kvp.Key, (null, data2[kvp.Key]));
            }

            // Save difference
            using (var writer = new StreamWriter(differenceFileName))
            {
                writer.WriteLine($"#\tFiles difference: {Path.GetFileName(firstFile)} and {Path.GetFileName(secondFile)}");
                writer.WriteLine("Type\tName\tDiff\tWritten1\tWritten2\tCreated1\tCreated2\tAccessed1\tAccessed2\tSize1\tSize2");
                foreach (var kvp in difference)
                    writer.WriteLine(GetDiffLine(kvp));
            }

            string GetDiffLine(KeyValuePair<string, (string, string)> kvp)
            {
                var ss1 = kvp.Value.Item1 == null ? new string[0] : kvp.Value.Item1.Split('\t');
                var ss2 = kvp.Value.Item2 == null ? new string[0] : kvp.Value.Item2.Split('\t');
                var s1 = ss1.Length > 2 ? ss1[2] : null;
                var s2 = ss2.Length > 2 ? ss2[2] : null;
                var s3 = ss1.Length > 3 ? ss1[3] : null;
                var s4 = ss2.Length > 3 ? ss2[3] : null;
                var s5 = ss1.Length > 4 ? ss1[4] : null;
                var s6 = ss2.Length > 4 ? ss2[4] : null;
                var s7 = ss1.Length > 5 ? ss1[5] : null;
                var s8 = ss2.Length > 5 ? ss2[5] : null;

                string s0;
                if (ss1.Length == 0 && ss2.Length == 0) s0 = "Denied";
                else if (ss1.Length == 0) s0 = "Only2";
                else if (ss2.Length == 0) s0 = "Only1";
                else if (ss1.Length > 5 && ss2.Length > 5 && !string.Equals(s7, s8)) s0 = "Size";
                else if (ss1.Length > 2 && ss2.Length > 2 && !string.Equals(s1, s2)) s0 = "Written";
                else if (ss1.Length > 3 && ss2.Length > 3 && !string.Equals(s3, s4)) s0 = "Created";
                else if (ss1.Length > 4 && ss2.Length > 4 && !string.Equals(s5, s6)) s0 = "Accessed";
                else throw new Exception("Check program!");

                return ($"{kvp.Key}\t{s0}\t{s1}\t{s2}\t{s3}\t{s4}\t{s5}\t{s6}\t{s7}\t{s8}").Trim();
            }

            return differenceFileName;
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
                            if (!string.Equals(s, "Type\tName\tWritten\tCreated\tAccessed\tSize", StringComparison.InvariantCulture))
                                throw new Exception($"Check header of scan file {Path.GetFileName(zipFileName)}");
                            checkHeader = true;
                            continue;
                        }

                        var i1 = s.IndexOf('\t');
                        var i2 = s.IndexOf('\t', i1 + 1);
                        var key = i2 == -1 ? s : s.Substring(0, i2);
                        data.Add(key, s);
                    }
                }
            return data;

        }

        public static void SaveFileSystemInfoIntoFile(string zipFileName)
        {
            if (!Helpers.IsAdministrator())
                throw new Exception("To read ALL(!!!) files, please, run program in administrator mode");

            /*var specialFolders = Enum.GetValues(typeof(Environment.SpecialFolder));
            foreach (Environment.SpecialFolder specialFolder in specialFolders)
            {
                Debug.Print($"{specialFolder}\t{Environment.GetFolderPath(specialFolder)}");
            }*/

            var pf86Folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var pfFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var pdFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData); // ProgramData
            var udFolder = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName; // User/AppData
            var wndFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var folders = new[] { pf86Folder, pfFolder, pdFolder, udFolder, wndFolder }.Distinct().ToArray();

            var log = new List<string> { $"Type\tName\tWritten\tCreated\tAccessed\tSize" };
            foreach (var folder in folders)
                ProcessFolder(folder, log);

            var entry = new VirtualFileEntry($"{Path.GetFileNameWithoutExtension(zipFileName)}.txt",
                System.Text.Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, log)));
            Helpers.ZipVirtualFileEntries(zipFileName, new[] { entry });
        }

        private static void ProcessFolder(string folder, List<string> log)
        {
            var dirInfo = new DirectoryInfo(folder);
            log.Add(GetLogString(dirInfo));

            try
            {
                var tmpFiles = Directory.GetFiles(folder);
                log.AddRange(tmpFiles.Select(a => GetLogString(new FileInfo(a))));
            }
            catch (UnauthorizedAccessException)
            {
                log.Add($"Denied\t{dirInfo.FullName}");
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var tmpFolders = Directory.GetDirectories(folder);
            foreach (var tmpFolder in tmpFolders)
                ProcessFolder(tmpFolder, log);
        }

        private static string GetLogString(FileSystemInfo info)
        {
            if (info is FileInfo fi)
                return $"File\t{info.FullName}\t{DateToString(info.LastWriteTime)}\t{DateToString(info.CreationTime)}\t{DateToString(info.LastAccessTime)}\t{fi.Length}";
            else
                return $"Dir\t{info.FullName}\t{DateToString(info.LastWriteTime)}\t{DateToString(info.CreationTime)}\t{DateToString(info.LastAccessTime)}";

            string DateToString(DateTime dt) => dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
