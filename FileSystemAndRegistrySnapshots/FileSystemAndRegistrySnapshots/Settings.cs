using SevenZip;

namespace FileSystemAndRegistrySnapshots
{
    public static class Settings
    {
        public static void SetZipLibrary() => SevenZipBase.SetLibraryPath(@"7z1900\x64-dll\7z.dll");
        public const string DataFolder = "E:\\Temp\\FileSystemAndRegistrySnapshots";
    }
}
