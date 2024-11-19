using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystemAndRegistrySnapshots
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            if (Helpers.IsAdministrator()) this.Text += " (ADMINISTRATOR mode)";
            lblStatus.Text = "";
            txtDataFolder.Text = Settings.DataFolder;

            var dataFolder = GetDataFolder();
            if (Directory.Exists(dataFolder))
            {
                var files = Directory.GetFiles(dataFolder, "FileSystem_*.zip")
                    .OrderByDescending(a => new FileInfo(a).CreationTime).Take(2).ToArray();
                if (files.Length == 1) txtFirstFileSystemSnapshotFile.Text = files[0];
                else if (files.Length > 1)
                {
                    txtFirstFileSystemSnapshotFile.Text = files[1];
                    txtSecondFileSystemSnapshotFile.Text = files[0];
                }

                files = Directory.GetFiles(dataFolder, "Registry_*.zip")
                    .OrderByDescending(a => new FileInfo(a).CreationTime).Take(2).ToArray();
                if (files.Length == 1) txtFirstRegistrySnapshotFile.Text = files[0];
                else if (files.Length > 1)
                {
                    txtFirstRegistrySnapshotFile.Text = files[1];
                    txtSecondRegistrySnapshotFile.Text = files[0];
                }

            }
        }

        private void ShowStatus(string message)
        {
            if (statusStrip1.InvokeRequired)
                Invoke(new MethodInvoker(delegate { ShowStatus(message); }));
            else
                lblStatus.Text = message;

            Application.DoEvents();
        }


        private void btnSelectFolder_Click(object sender, System.EventArgs e)
        {
            using (var folderBrowserDialog1 = new FolderBrowserDialog())
            {
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
            }
        }

        private async void btnFileSystemSnapshot_Click(object sender, EventArgs e)
        {
            btnFileSystemSnapshot.Enabled = false;
            try
            {
                var task = ScanFileSystem.SaveFileSystemInfoIntoFile(GetDataFolder(), ShowStatus);
                await Task.Factory.StartNew(() => task);
                ShowStatus($"New FileSystem snapshot file is {task}");
                MessageBox.Show($"New FileSystem snapshot file is {task}");
            }
            catch (Exception exception)
            {
                ShowStatus(exception.Message);
                MessageBox.Show(exception.Message);
            }

            btnFileSystemSnapshot.Enabled = true;
        }

        private void btnSelectFirstFileSystemSnapshotFile_Click(object sender, EventArgs e)
        {
            if (Helpers.OpenFileSystemZipFileDialog(GetDataFolder(), txtFirstFileSystemSnapshotFile.Text,
                    @"file system zip files (*.zip)|FileSystem_*.zip") is string fn && !string.IsNullOrWhiteSpace(fn))
            {
                txtFirstFileSystemSnapshotFile.Text = fn;
            }
        }

        private void btnSelectSecondFileSystemSnapshotFile_Click(object sender, EventArgs e)
        {
            if (Helpers.OpenFileSystemZipFileDialog(GetDataFolder(), txtSecondFileSystemSnapshotFile.Text,
                    @"file system zip files (*.zip)|FileSystem_*.zip") is string fn && !string.IsNullOrWhiteSpace(fn))
            {
                txtSecondFileSystemSnapshotFile.Text = fn;
            }
        }

        private async void btnCompareFileSystemSnapshots_Click(object sender, EventArgs e)
        {
            btnCompareFileSystemSnapshots.Enabled = false;
            try
            {
                var task = ScanFileSystem.CompareScanFiles(txtFirstFileSystemSnapshotFile.Text, txtSecondFileSystemSnapshotFile.Text, ShowStatus);
                await Task.Factory.StartNew(() => task);
                ShowStatus($"New FileSystem difference file is {task}");
                MessageBox.Show($"New FileSystem difference file is {task}");
            }
            catch (Exception exception)
            {
                ShowStatus(exception.Message);
                MessageBox.Show(exception.Message);
            }

            btnCompareFileSystemSnapshots.Enabled = true;
        }

        private void btnSelectFirstRegistrySnapshotFile_Click(object sender, EventArgs e)
        {
            if (Helpers.OpenFileSystemZipFileDialog(GetDataFolder(), txtFirstRegistrySnapshotFile.Text,
                    @"registry zip files (*.zip)|Registry_*.zip") is string fn && !string.IsNullOrWhiteSpace(fn))
            {
                txtFirstRegistrySnapshotFile.Text = fn;
            }
        }

        private void btnSelectSecondRegistrySnapshotFile_Click(object sender, EventArgs e)
        {
            if (Helpers.OpenFileSystemZipFileDialog(GetDataFolder(), txtSecondRegistrySnapshotFile.Text,
                    @"registry zip files (*.zip)|Registry_*.zip") is string fn && !string.IsNullOrWhiteSpace(fn))
            {
                txtSecondRegistrySnapshotFile.Text = fn;
            }
        }

        private async void btnCompareRegistrySnapshots_Click(object sender, EventArgs e)
        {
            btnCompareRegistrySnapshots.Enabled = false;
            try
            {
                var task = ScanRegistry.CompareRegistryFiles(txtFirstRegistrySnapshotFile.Text, txtSecondRegistrySnapshotFile.Text, ShowStatus);
                await Task.Factory.StartNew(() => task);
                ShowStatus($"New registry difference file is {task}");
                MessageBox.Show($"New registry difference file is {task}");
            }
            catch (Exception exception)
            {
                ShowStatus(exception.Message);
                MessageBox.Show(exception.Message);
            }

            btnCompareRegistrySnapshots.Enabled = true;
        }

        private string GetDataFolder()
        {
            if (Directory.Exists(txtDataFolder.Text)) return txtDataFolder.Text;
            if (Directory.Exists(Settings.DataFolder)) return Settings.DataFolder;
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}
