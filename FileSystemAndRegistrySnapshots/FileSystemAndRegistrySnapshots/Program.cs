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

            // Helpers.GetAllSpecialFolders();
            // ScanServices.Start();

            Settings.SetZipLibrary();

            Application.Run(new MainForm());
        }
    }
}
