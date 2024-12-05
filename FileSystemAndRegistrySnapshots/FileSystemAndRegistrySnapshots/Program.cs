using System;
using System.Windows.Forms;

namespace FileSystemAndRegistrySnapshots
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ScanRegistry.ReadRegistry();
            // Helpers.GetAllSpecialFolders();
            // ScanServices.Start();
            // ScanFirewall.Temp();
            // ScanTaskScheduler.Test();
            // Helpers.Test();

            Settings.SetZipLibrary();

            Application.Run(new MainForm());
        }
    }
}
