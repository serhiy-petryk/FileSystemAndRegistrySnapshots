using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using SevenZip;

namespace FileSystemAndRegistrySnapshots
{
    public static class Helpers
    {
        public static void Test()
        {
            var fn = @"I:\ProgramData\Avast Software\Avast\FileInfo2.db";
            var bytes = ReadAllBytes(fn);
            using (var fileStream = new FileStream(fn, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    var content = sr.ReadToEnd();
                }
            }
            /*using fileStream = New FileStream("path", FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Using streamReader = New StreamReader(fileStream)
            Dim content = streamReader.ReadToEnd()
            End Using
            End Using*/

            var a = ComputeSha256Hash(File.ReadAllBytes(fn));
        }

        private static byte[] ReadAllBytes(string fileName)
        {
            byte[] buffer = null;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }

        public static string ComputeFileSha256Hash(string fullFileName) =>
            ComputeSha256Hash(ReadAllBytes(fullFileName));

        public static string ComputeSha256Hash(byte[] rawData)
        {
            // Create a SHA256
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                var bytes = sha256Hash.ComputeHash(rawData);

                // Convert byte array to a string
                var builder = new StringBuilder();
                for (var i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GetUsersFolderPath() => Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)).FullName;

        public static void GetAllSpecialFolders()
        {
            var folders = Enum.GetValues(typeof(Environment.SpecialFolder));
            foreach (Environment.SpecialFolder folder in folders)
                Debug.Print($"{folder}\t{Environment.GetFolderPath(folder)}");
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp) => source?.IndexOf(toCheck, comp) >= 0;

        public static string OpenFileSystemZipFileDialog(string folder, string initialFileName, string filter)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (File.Exists(initialFileName))
                {
                    ofd.InitialDirectory = Path.GetDirectoryName(initialFileName);
                    ofd.FileName = Path.GetFileName(initialFileName);
                }
                else
                    ofd.InitialDirectory = folder;
                ofd.RestoreDirectory = true;
                ofd.Multiselect = false;
                ofd.Filter = filter;
                if (ofd.ShowDialog() == DialogResult.OK)
                    return ofd.FileName;
                return null;
            }
        }

        public static void SaveStringsToZipFile(string zipFileName, IEnumerable<string> data)
        {
            var entry = new VirtualFileEntry($"{Path.GetFileNameWithoutExtension(zipFileName)}.txt",
                System.Text.Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, data)));
            Helpers.ZipVirtualFileEntries(zipFileName, new[] { entry });
        }

        public static IEnumerable<string> GetLinesOfZipEntry(this ZipArchiveEntry entry)
        {
            using (var entryStream = entry.Open())
            using (var reader = new StreamReader(entryStream, System.Text.Encoding.UTF8, true))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    yield return line;
            }
        }

        public static void ZipVirtualFileEntries(string zipFileName, IEnumerable<VirtualFileEntry> entries)
        {
            // -- Very slowly: another way for 7za -> use Process/ProcessStartInfo class
            // see https://stackoverflow.com/questions/71343454/how-do-i-use-redirectstandardinput-when-running-a-process-with-administrator-pri

            var tmp = new SevenZipCompressor { ArchiveFormat = OutArchiveFormat.Zip };
            var dict = entries.ToDictionary(a => a.Name, a => a.Stream);
            tmp.CompressStreamDictionary(dict, zipFileName);

            foreach (var item in entries) item?.Dispose();
        }


        public static string GetSystemDriveLabel()
        {
            var systemDriveLetter = (new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Windows))).Root.Name.Replace("\\", "");
            return (new DriveInfo(systemDriveLetter)).VolumeLabel;
        }

        public static bool IsAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static void FakeShowStatus(string message) => Debug.Print(message);

        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
