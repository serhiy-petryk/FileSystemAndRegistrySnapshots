using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using SevenZip;

namespace FileSystemAndRegistrySnapshots
{
    public static class Helpers
    {
        public static string OpenFileSystemZipFileDialog(string folder) => OpenFileDialogGeneric(folder, @"file system zip files (*.zip)|FileSystem_*.zip");
        public static string OpenZipFileDialog(string folder) => OpenFileDialogGeneric(folder, @"zip files (*.zip)|*.zip");
        public static string OpenFileDialogGeneric(string folder, string filter)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = folder;
                ofd.RestoreDirectory = true;
                ofd.Multiselect = false;
                ofd.Filter = filter;
                if (ofd.ShowDialog() == DialogResult.OK)
                    return ofd.FileName;
                return null;
            }
        }

        public static string[] OpenFileDialogMultiselect(string folder, string filter, string title = null)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (!string.IsNullOrWhiteSpace(title))
                    ofd.Title = title;
                ofd.InitialDirectory = folder;
                ofd.RestoreDirectory = true;
                ofd.Multiselect = true;
                ofd.Filter = filter;
                if (ofd.ShowDialog() == DialogResult.OK)
                    return ofd.FileNames;
                return null;
            }
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
    }
}
