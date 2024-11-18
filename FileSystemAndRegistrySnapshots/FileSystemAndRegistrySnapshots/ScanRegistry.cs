using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security;
using Microsoft.Win32;

namespace FileSystemAndRegistrySnapshots
{
    public static class ScanRegistry
    {
        private static readonly string[] SkipKeys = new[]
        {
            "\\Avast Software\\Avast\\", "\\Office\\16.0\\", "\\VSCommon\\16.0\\", "\\Windows\\Shell\\Bags\\",
            "\\Windows\\Shell\\BagMRU\\"/*, "\\CurrentVersion\\Explorer\\UserAssist\\"*/
        };

        private static readonly Dictionary<Type, int> ValueTypes = new Dictionary<Type, int>();
        private static int _blankValues = 0;
        private static int _errors = 0;

        public static void Start()
        {
            var firstFile = @"E:\Temp\Reg\Registry_SSD_240_202411151520.zip";
            var secondFile = @"E:\Temp\Reg\Registry_SSD_240_202411152215.zip";
            // 334 items
            var differenceFileName = CompareRegistryFiles(firstFile, secondFile, SkipKeys);
        }

        private static string CompareRegistryFiles(string firstFile, string secondFile, string[] skipKeys)
        {
            var s = Path.GetFileNameWithoutExtension(firstFile);
            var i1 = s.IndexOf('_');
            var i2 = s.LastIndexOf('_');
            var diskLabel = s.Substring(i1 + 1, i2 - i1 - 1);
            var differenceFileName = Path.Combine(Path.GetDirectoryName(firstFile), $"RegistryDiff_{diskLabel}_{DateTime.Now:yyyyMMddHHmmss}.txt");

            var difference = new Dictionary<string, (string, string)>();
            var data1 = ParseZipRegistryFile(firstFile); // 1'879'365
            var data2 = ParseZipRegistryFile(secondFile);
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
                    difference.Add(kvp.Key, (data1[kvp.Key] ?? "<OLD>", null));
            }

            foreach (var kvp in data2)
            {
                if (!data1.ContainsKey(kvp.Key) && !skipKeys.Any(a => kvp.Key.Contains(a, StringComparison.InvariantCultureIgnoreCase)))
                    difference.Add(kvp.Key, (null, data2[kvp.Key] ?? "<NEW>"));
            }

            using (var writer = new StreamWriter(differenceFileName))
            {
                writer.WriteLine($"Registry difference: {Path.GetFileName(firstFile)} and {Path.GetFileName(secondFile)}");
                writer.WriteLine("Key\tValue1\tValue2");
                foreach (var kvp in difference)
                {
                    writer.WriteLine($"{kvp.Key}\t{GetLogValue(kvp.Value.Item1)}\t{GetLogValue(kvp.Value.Item2)}");
                }

                string GetLogValue(string value) => value == null ? "<NULL>" : value.Substring(0, Math.Min(value.Length, 32000));
            }

            return differenceFileName;
        }

        private static Dictionary<string, string> ParseZipRegistryFile(string zipFileName)
        {
            var data = new Dictionary<string, string>();
            string mainKey = null;
            string key = null;
            var valueStrings = new List<string>();
            var entryName = Path.GetFileNameWithoutExtension(zipFileName) + ".reg";
            using (var zip = ZipFile.Open(zipFileName, ZipArchiveMode.Read))
                foreach (var entry in zip.Entries.Where(a => string.Equals(a.Name, entryName)))
                {
                    var checkHeader = false;
                    foreach (var s in entry.GetLinesOfZipEntry())
                    {
                        if (!checkHeader)
                        {
                            if (!s.StartsWith("Windows Registry Editor Version"))
                                throw new Exception($"Check header of registry file {Path.GetFileName(zipFileName)}");
                            checkHeader = true;
                            continue;
                        }

                        if (string.IsNullOrEmpty(s))
                        {
                            if (valueStrings.Count > 0 && valueStrings[0].StartsWith("\"") &&
                                !valueStrings[valueStrings.Count - 1].EndsWith("\""))
                            {
                                valueStrings.Add("\n");
                                continue;
                            }

                            if (!string.IsNullOrEmpty(mainKey) && !string.IsNullOrEmpty(key))
                                SaveValue(mainKey, key, valueStrings, data);

                            mainKey = null;
                            key = null;
                        }
                        else
                        {
                            if (mainKey == null)
                            {
                                if (s.StartsWith("[") && s.EndsWith("]"))
                                {
                                    mainKey = s.Substring(1, s.Length - 2);
                                    SaveValue("@" + mainKey, null, valueStrings, data);
                                }
                                else
                                    throw new Exception("Check registry parser");
                            }
                            else
                            {
                                if (s.StartsWith("@="))
                                {
                                    if (key != null)
                                        SaveValue(mainKey, key, valueStrings, data);

                                    key = "@";
                                    valueStrings.Add(GetValue(s.Substring(2).Trim()));
                                }
                                else if (s.StartsWith("\"") &&
                                         s.IndexOf("\"=", StringComparison.CurrentCultureIgnoreCase) != -1)
                                {
                                    if (key != null)
                                        SaveValue(mainKey, key, valueStrings, data);

                                    var i1 = s.IndexOf("\"=", StringComparison.CurrentCultureIgnoreCase);
                                    key = s.Substring(0, i1 + 1).Trim();
                                    valueStrings.Add(GetValue(s.Substring(i1 + 2).Trim()));
                                }
                                else
                                {
                                    if (key == null)
                                        throw new Exception("Check registry parser");

                                    valueStrings.Add(GetValue(s.TrimStart()));
                                }
                            }
                        }
                    }
                }

            if (!string.IsNullOrEmpty(mainKey))
                SaveValue(mainKey, key, valueStrings, data);

            return data;

            static void SaveValue(string mainKey, string key, List<string> valueStrings, Dictionary<string, string> data)
            {
                var dataKey = mainKey + ConvertKeyToString(key);
                if (valueStrings.Count == 0)
                    data.Add(dataKey, null);
                else
                    data.Add(dataKey, string.Join(null, valueStrings));
                valueStrings.Clear();
            }
            static string GetValue(string value)
            {
                if (value.EndsWith("\\")) return value.Substring(0, value.Length - 1);
                return value;
            }
        }

        private static string ConvertKeyToString(string key)
        {
            if (key == null || key == "@") return "\\";

            if (key.StartsWith("\"") || key.EndsWith("\""))
                key = key.Substring(1, key.Length - 2);
            else
                throw new Exception("Check registry parser");

            var newKey = key.Replace("\\\"", ((char)1).ToString()).Replace("\\\\", ((char)2).ToString());
            newKey = newKey.Replace(((char)1).ToString(), "\"").Replace(((char)2).ToString(), "\\");

            return "\\" + newKey;
        }

        /// <summary>
        /// Read only ~30% of registry keys and some types of registry entries don't support in C#
        /// The best way is to export registry values in regedit.exe program and then to zip reg file
        /// </summary>
        public static void ReadRegistry()
        {
            var sw = new Stopwatch();
            sw.Start();
            var registryKeys = new[]
            {
                Registry.ClassesRoot, Registry.CurrentUser, Registry.LocalMachine, Registry.Users,
                Registry.CurrentConfig/*, Registry.PerformanceData*/
            };

            foreach (var key in registryKeys)
            {
                OutputRegKey(key);
            }

            int count = _blankValues;
            foreach (var kvp in ValueTypes)
            {
                count += kvp.Value;
                Debug.Print($"{kvp.Key}\t{kvp.Value:N0}");
            }

            Debug.Print($"EmptyKeys\t{_blankValues:N0}");
            Debug.Print($"ErrorKeys\t{_errors:N0}");
            Debug.Print($"TotalKeys\t{count:N0}"); // 450'701

            sw.Stop();
            Debug.Print($"\nDuration: {sw.ElapsedMilliseconds / 1000:N0} seconds");
        }

        private static void ProcessValueNames(RegistryKey key)
        { //function to process the valueNames for a given key
            var valueNames = key.GetValueNames();
            if (valueNames.Length == 0)
            {
                _blankValues++;
                return;
            }

            foreach (var valueName in valueNames)
            {
                var obj = key.GetValue(valueName);
                var type = obj.GetType();
                if (!ValueTypes.ContainsKey(type))
                    ValueTypes.Add(type, 0);
                ValueTypes[type]++;
                // Debug.Print($"{key.ToString()}\t{obj.ToString()}");
            }
        }

        public static void OutputRegKey(RegistryKey key)
        {
            if (key == null) return;

            try
            {
                ProcessValueNames(key);
            }
            catch (Exception e)
            {
            }

            try
            {
                var subKeyNames = key.GetSubKeyNames(); //means deeper folder
                foreach (string keyName in subKeyNames)
                {
                    using (RegistryKey key2 = key.OpenSubKey(keyName))
                        OutputRegKey(key2);
                }
            }
            catch (SecurityException)
            {
                _errors++;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
