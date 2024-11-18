using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystemAndRegistrySnapshots
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings.SetZipLibrary();

            // var b = Helpers.IsAdministrator();

            // ScanRegistry.Start2();
            // ScanFiles.Start();
            // ScanRegistry.Start();

            Application.Run(new MainForm());
        }
    }
}
