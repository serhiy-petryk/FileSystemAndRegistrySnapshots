using SevenZip;

namespace FileSystemAndRegistrySnapshots
{
    public static class Settings
    {
        public static void SetZipLibrary() => SevenZipBase.SetLibraryPath(@"7z1900\x64-dll\7z.dll");
        public const string DataFolder = "E:\\Temp\\FileSystemAndRegistrySnapshots";

        public static readonly string[] FileSystemSkipKeys2 = new string[]
        {
            "\\Program Files\\Avast Software\\", "\\ProgramData\\Avast Software\\", "\\ProgramData\\Microsoft\\RAC\\StateData\\",
            "\\AppData\\Local\\Google\\Chrome\\User Data\\", "\\AppData\\Local\\JetBrains\\",
            "\\AppData\\Local\\Microsoft\\VisualStudio\\",
            "\\AppData\\Local\\Microsoft\\Windows\\Temporary Internet Files\\",
            "\\AppData\\Local\\Microsoft\\Windows\\WebCache\\", "\\AppData\\Local\\Psiphon3\\",
            "\\AppData\\Local\\Temp\\","\\AppData\\Roaming\\GitHub Desktop\\", "\\AppData\\Roaming\\Microsoft\\"
        };

        public static readonly string[] FileSystemSkipKeys = new string[]
        {
            "\\Avast Software\\Avast\\", "\\JetBrains\\", "\\Microsoft\\VisualStudio\\", "\\TortoiseHg\\", "\\Psiphon3\\", "\\GitHub Desktop\\",
            "\\ProgramData\\Microsoft\\RAC\\StateData\\", "\\AppData\\Local\\Google\\Chrome\\User Data\\",
            "\\AppData\\Local\\Microsoft\\Windows\\Temporary Internet Files\\",
            "\\AppData\\Local\\Microsoft\\Windows\\WebCache\\",
            "\\AppData\\Local\\Temp\\", "\\AppData\\Roaming\\Microsoft\\"
        };

        public static readonly string[] RegistrySkipKeys = new[]
        {
            "\\Avast Software\\Avast\\", "\\Office\\16.0\\", "\\VSCommon\\16.0\\", "\\Windows\\Shell\\Bags\\",
            "\\Windows\\Shell\\BagMRU\\"/*, "\\CurrentVersion\\Explorer\\UserAssist\\"*/
        };


    }
}
