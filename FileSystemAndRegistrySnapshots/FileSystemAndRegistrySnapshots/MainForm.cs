using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystemAndRegistrySnapshots
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            lblStatus.Text = "";
            txtDataFolder.Text = Settings.DataFolder;
        }

        private void btnSelectFolder_Click(object sender, System.EventArgs e)
        {
            btnSelectFolder.Enabled = false;

            var folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = @"Select the directory that you want to use as the data folder.";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            folderBrowserDialog1.ShowNewFolderButton = false;

            folderBrowserDialog1.SelectedPath = GetDataFolder();

            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Personal;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(folderBrowserDialog1.SelectedPath))
                    txtDataFolder.Text = folderBrowserDialog1.SelectedPath;
            }

            /*
             *             if (CsUtils.OpenZipFileDialog(Data.Actions.Yahoo.YahooCommon.MinuteYahooDataFolder) is string fn &&
                !string.IsNullOrWhiteSpace(fn))
                Data.Actions.Yahoo.YahooMinuteLogToTextFile.YahooMinuteLogSaveToTextFile(new[] { fn }, ShowStatus);

             */

            /*
             *         private void ShowStatus(string message)
        {
            if (statusStrip1.InvokeRequired)
                Invoke(new MethodInvoker(delegate { ShowStatus(message); }));
            else
                StatusLabel.Text = message;

            Application.DoEvents();
        }

             */
            btnSelectFolder.Enabled = true;
        }

        private async void btnFileSystemSnapshot_Click(object sender, EventArgs e)
        {
            btnFileSystemSnapshot.Enabled = false;
            try
            {
                await Task.Factory.StartNew(() => ScanFileSystem.SaveFileSystemInfoIntoFile(GetDataFolder()));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            btnFileSystemSnapshot.Enabled = true;
        }

        private void btnSelectFirstFileSystemSnapshotFile_Click(object sender, EventArgs e)
        {
            btnSelectFirstFileSystemSnapshotFile.Enabled = false;
            if (Helpers.OpenFileSystemZipFileDialog(GetDataFolder()) is string fn &&
                !string.IsNullOrWhiteSpace(fn))
            {
                txtFirstFileSystemSnapshotFile.Text = fn;
            }

            btnSelectFirstFileSystemSnapshotFile.Enabled = true;

        }

        private void btnSelectSecondFileSystemSnapshotFile_Click(object sender, EventArgs e)
        {

        }

        private void btnCompareFileSystemSnapshots_Click(object sender, EventArgs e)
        {

        }
        private void btnSelectFirstRegistrySnapshotFile_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectSecondRegistrySnapshotFile_Click(object sender, EventArgs e)
        {

        }

        private void btnCompareRegistrySnapshots_Click(object sender, EventArgs e)
        {

        }

        private string GetDataFolder()
        {
            var dataFolder = txtDataFolder.Text;
            if (!Directory.Exists(dataFolder)) dataFolder = Settings.DataFolder;
            if (!Directory.Exists(dataFolder)) dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return dataFolder;
        }
    }
}
