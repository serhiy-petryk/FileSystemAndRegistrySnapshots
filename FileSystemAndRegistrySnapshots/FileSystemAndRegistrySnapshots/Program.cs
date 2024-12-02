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

            Settings.SetZipLibrary();

            Helpers.GetAllSpecialFolders();
            // ScanServices.Start();

            Application.Run(new MainForm());
        }
    }
}
