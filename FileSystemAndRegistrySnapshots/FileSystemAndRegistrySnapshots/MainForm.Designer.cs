
namespace FileSystemAndRegistrySnapshots
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtDataFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnFileSystemSnapshot = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCompareFileSystemSnapshots = new System.Windows.Forms.Button();
            this.btnSelectSecondFileSystemSnapshotFile = new System.Windows.Forms.Button();
            this.btnSelectFirstFileSystemSnapshotFile = new System.Windows.Forms.Button();
            this.txtSecondFileSystemSnapshotFile = new System.Windows.Forms.TextBox();
            this.txtFirstFileSystemSnapshotFile = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCompareRegistrySnapshots = new System.Windows.Forms.Button();
            this.btnSelectSecondRegistrySnapshotFile = new System.Windows.Forms.Button();
            this.btnSelectFirstRegistrySnapshotFile = new System.Windows.Forms.Button();
            this.txtSecondRegistrySnapshotFile = new System.Windows.Forms.TextBox();
            this.txtFirstRegistrySnapshotFile = new System.Windows.Forms.TextBox();
            this.btnServicesSnapshot = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCompareServicesSnapshots = new System.Windows.Forms.Button();
            this.btnSelectSecondServicesSnapshotFile = new System.Windows.Forms.Button();
            this.btnSelectFirstServicesSnapshotFile = new System.Windows.Forms.Button();
            this.txtSecondServicesSnapshotFile = new System.Windows.Forms.TextBox();
            this.txtFirstServicesSnapshotFile = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 316);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(762, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(70, 17);
            this.lblStatus.Text = "Status Label";
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataFolder.Location = new System.Drawing.Point(10, 10);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.Size = new System.Drawing.Size(472, 20);
            this.txtDataFolder.TabIndex = 1;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Location = new System.Drawing.Point(487, 10);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(100, 22);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "Select Data folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // btnFileSystemSnapshot
            // 
            this.btnFileSystemSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileSystemSnapshot.Location = new System.Drawing.Point(592, 10);
            this.btnFileSystemSnapshot.Name = "btnFileSystemSnapshot";
            this.btnFileSystemSnapshot.Size = new System.Drawing.Size(156, 22);
            this.btnFileSystemSnapshot.TabIndex = 3;
            this.btnFileSystemSnapshot.Text = "Make file system snapshot";
            this.btnFileSystemSnapshot.UseVisualStyleBackColor = true;
            this.btnFileSystemSnapshot.Click += new System.EventHandler(this.btnFileSystemSnapshot_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCompareFileSystemSnapshots);
            this.groupBox1.Controls.Add(this.btnSelectSecondFileSystemSnapshotFile);
            this.groupBox1.Controls.Add(this.btnSelectFirstFileSystemSnapshotFile);
            this.groupBox1.Controls.Add(this.txtSecondFileSystemSnapshotFile);
            this.groupBox1.Controls.Add(this.txtFirstFileSystemSnapshotFile);
            this.groupBox1.Location = new System.Drawing.Point(10, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(743, 78);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compare file system snapshots";
            // 
            // btnCompareFileSystemSnapshots
            // 
            this.btnCompareFileSystemSnapshots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompareFileSystemSnapshots.Location = new System.Drawing.Point(583, 19);
            this.btnCompareFileSystemSnapshots.Name = "btnCompareFileSystemSnapshots";
            this.btnCompareFileSystemSnapshots.Size = new System.Drawing.Size(156, 45);
            this.btnCompareFileSystemSnapshots.TabIndex = 6;
            this.btnCompareFileSystemSnapshots.Text = "Compare file system snapshots";
            this.btnCompareFileSystemSnapshots.UseVisualStyleBackColor = true;
            this.btnCompareFileSystemSnapshots.Click += new System.EventHandler(this.btnCompareFileSystemSnapshots_Click);
            // 
            // btnSelectSecondFileSystemSnapshotFile
            // 
            this.btnSelectSecondFileSystemSnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSecondFileSystemSnapshotFile.Location = new System.Drawing.Point(477, 44);
            this.btnSelectSecondFileSystemSnapshotFile.Name = "btnSelectSecondFileSystemSnapshotFile";
            this.btnSelectSecondFileSystemSnapshotFile.Size = new System.Drawing.Size(100, 22);
            this.btnSelectSecondFileSystemSnapshotFile.TabIndex = 5;
            this.btnSelectSecondFileSystemSnapshotFile.Text = "Select second file";
            this.btnSelectSecondFileSystemSnapshotFile.UseVisualStyleBackColor = true;
            this.btnSelectSecondFileSystemSnapshotFile.Click += new System.EventHandler(this.btnSelectSecondFileSystemSnapshotFile_Click);
            // 
            // btnSelectFirstFileSystemSnapshotFile
            // 
            this.btnSelectFirstFileSystemSnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFirstFileSystemSnapshotFile.Location = new System.Drawing.Point(477, 19);
            this.btnSelectFirstFileSystemSnapshotFile.Name = "btnSelectFirstFileSystemSnapshotFile";
            this.btnSelectFirstFileSystemSnapshotFile.Size = new System.Drawing.Size(100, 22);
            this.btnSelectFirstFileSystemSnapshotFile.TabIndex = 4;
            this.btnSelectFirstFileSystemSnapshotFile.Text = "Select first file";
            this.btnSelectFirstFileSystemSnapshotFile.UseVisualStyleBackColor = true;
            this.btnSelectFirstFileSystemSnapshotFile.Click += new System.EventHandler(this.btnSelectFirstFileSystemSnapshotFile_Click);
            // 
            // txtSecondFileSystemSnapshotFile
            // 
            this.txtSecondFileSystemSnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecondFileSystemSnapshotFile.Location = new System.Drawing.Point(5, 44);
            this.txtSecondFileSystemSnapshotFile.Name = "txtSecondFileSystemSnapshotFile";
            this.txtSecondFileSystemSnapshotFile.Size = new System.Drawing.Size(467, 20);
            this.txtSecondFileSystemSnapshotFile.TabIndex = 3;
            // 
            // txtFirstFileSystemSnapshotFile
            // 
            this.txtFirstFileSystemSnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstFileSystemSnapshotFile.Location = new System.Drawing.Point(5, 19);
            this.txtFirstFileSystemSnapshotFile.Name = "txtFirstFileSystemSnapshotFile";
            this.txtFirstFileSystemSnapshotFile.Size = new System.Drawing.Size(467, 20);
            this.txtFirstFileSystemSnapshotFile.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnCompareRegistrySnapshots);
            this.groupBox2.Controls.Add(this.btnSelectSecondRegistrySnapshotFile);
            this.groupBox2.Controls.Add(this.btnSelectFirstRegistrySnapshotFile);
            this.groupBox2.Controls.Add(this.txtSecondRegistrySnapshotFile);
            this.groupBox2.Controls.Add(this.txtFirstRegistrySnapshotFile);
            this.groupBox2.Location = new System.Drawing.Point(10, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(743, 78);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Compare registry snapshots";
            // 
            // btnCompareRegistrySnapshots
            // 
            this.btnCompareRegistrySnapshots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompareRegistrySnapshots.Location = new System.Drawing.Point(583, 19);
            this.btnCompareRegistrySnapshots.Name = "btnCompareRegistrySnapshots";
            this.btnCompareRegistrySnapshots.Size = new System.Drawing.Size(156, 45);
            this.btnCompareRegistrySnapshots.TabIndex = 6;
            this.btnCompareRegistrySnapshots.Text = "Compare registry snapshots";
            this.btnCompareRegistrySnapshots.UseVisualStyleBackColor = true;
            this.btnCompareRegistrySnapshots.Click += new System.EventHandler(this.btnCompareRegistrySnapshots_Click);
            // 
            // btnSelectSecondRegistrySnapshotFile
            // 
            this.btnSelectSecondRegistrySnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSecondRegistrySnapshotFile.Location = new System.Drawing.Point(477, 43);
            this.btnSelectSecondRegistrySnapshotFile.Name = "btnSelectSecondRegistrySnapshotFile";
            this.btnSelectSecondRegistrySnapshotFile.Size = new System.Drawing.Size(100, 22);
            this.btnSelectSecondRegistrySnapshotFile.TabIndex = 5;
            this.btnSelectSecondRegistrySnapshotFile.Text = "Select second file";
            this.btnSelectSecondRegistrySnapshotFile.UseVisualStyleBackColor = true;
            this.btnSelectSecondRegistrySnapshotFile.Click += new System.EventHandler(this.btnSelectSecondRegistrySnapshotFile_Click);
            // 
            // btnSelectFirstRegistrySnapshotFile
            // 
            this.btnSelectFirstRegistrySnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFirstRegistrySnapshotFile.Location = new System.Drawing.Point(477, 18);
            this.btnSelectFirstRegistrySnapshotFile.Name = "btnSelectFirstRegistrySnapshotFile";
            this.btnSelectFirstRegistrySnapshotFile.Size = new System.Drawing.Size(100, 22);
            this.btnSelectFirstRegistrySnapshotFile.TabIndex = 4;
            this.btnSelectFirstRegistrySnapshotFile.Text = "Select first file";
            this.btnSelectFirstRegistrySnapshotFile.UseVisualStyleBackColor = true;
            this.btnSelectFirstRegistrySnapshotFile.Click += new System.EventHandler(this.btnSelectFirstRegistrySnapshotFile_Click);
            // 
            // txtSecondRegistrySnapshotFile
            // 
            this.txtSecondRegistrySnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecondRegistrySnapshotFile.Location = new System.Drawing.Point(5, 44);
            this.txtSecondRegistrySnapshotFile.Name = "txtSecondRegistrySnapshotFile";
            this.txtSecondRegistrySnapshotFile.Size = new System.Drawing.Size(467, 20);
            this.txtSecondRegistrySnapshotFile.TabIndex = 3;
            // 
            // txtFirstRegistrySnapshotFile
            // 
            this.txtFirstRegistrySnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstRegistrySnapshotFile.Location = new System.Drawing.Point(5, 19);
            this.txtFirstRegistrySnapshotFile.Name = "txtFirstRegistrySnapshotFile";
            this.txtFirstRegistrySnapshotFile.Size = new System.Drawing.Size(467, 20);
            this.txtFirstRegistrySnapshotFile.TabIndex = 2;
            // 
            // btnServicesSnapshot
            // 
            this.btnServicesSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServicesSnapshot.Location = new System.Drawing.Point(592, 35);
            this.btnServicesSnapshot.Name = "btnServicesSnapshot";
            this.btnServicesSnapshot.Size = new System.Drawing.Size(156, 22);
            this.btnServicesSnapshot.TabIndex = 6;
            this.btnServicesSnapshot.Text = "Make service list snapshot";
            this.btnServicesSnapshot.UseVisualStyleBackColor = true;
            this.btnServicesSnapshot.Click += new System.EventHandler(this.btnServicesSnapshot_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnCompareServicesSnapshots);
            this.groupBox3.Controls.Add(this.btnSelectSecondServicesSnapshotFile);
            this.groupBox3.Controls.Add(this.btnSelectFirstServicesSnapshotFile);
            this.groupBox3.Controls.Add(this.txtSecondServicesSnapshotFile);
            this.groupBox3.Controls.Add(this.txtFirstServicesSnapshotFile);
            this.groupBox3.Location = new System.Drawing.Point(10, 231);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(743, 78);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Compare service list snapshots";
            // 
            // btnCompareServicesSnapshots
            // 
            this.btnCompareServicesSnapshots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompareServicesSnapshots.Location = new System.Drawing.Point(582, 19);
            this.btnCompareServicesSnapshots.Name = "btnCompareServicesSnapshots";
            this.btnCompareServicesSnapshots.Size = new System.Drawing.Size(156, 45);
            this.btnCompareServicesSnapshots.TabIndex = 6;
            this.btnCompareServicesSnapshots.Text = "Compare service list snapshots";
            this.btnCompareServicesSnapshots.UseVisualStyleBackColor = true;
            this.btnCompareServicesSnapshots.Click += new System.EventHandler(this.btnCompareServicesSnapshots_Click);
            // 
            // btnSelectSecondServicesSnapshotFile
            // 
            this.btnSelectSecondServicesSnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSecondServicesSnapshotFile.Location = new System.Drawing.Point(477, 43);
            this.btnSelectSecondServicesSnapshotFile.Name = "btnSelectSecondServicesSnapshotFile";
            this.btnSelectSecondServicesSnapshotFile.Size = new System.Drawing.Size(100, 22);
            this.btnSelectSecondServicesSnapshotFile.TabIndex = 5;
            this.btnSelectSecondServicesSnapshotFile.Text = "Select second file";
            this.btnSelectSecondServicesSnapshotFile.UseVisualStyleBackColor = true;
            this.btnSelectSecondServicesSnapshotFile.Click += new System.EventHandler(this.btnSelectSecondServicesSnapshotFile_Click);
            // 
            // btnSelectFirstServicesSnapshotFile
            // 
            this.btnSelectFirstServicesSnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFirstServicesSnapshotFile.Location = new System.Drawing.Point(477, 18);
            this.btnSelectFirstServicesSnapshotFile.Name = "btnSelectFirstServicesSnapshotFile";
            this.btnSelectFirstServicesSnapshotFile.Size = new System.Drawing.Size(100, 22);
            this.btnSelectFirstServicesSnapshotFile.TabIndex = 4;
            this.btnSelectFirstServicesSnapshotFile.Text = "Select first file";
            this.btnSelectFirstServicesSnapshotFile.UseVisualStyleBackColor = true;
            this.btnSelectFirstServicesSnapshotFile.Click += new System.EventHandler(this.btnSelectFirstServicesSnapshotFile_Click);
            // 
            // txtSecondServicesSnapshotFile
            // 
            this.txtSecondServicesSnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecondServicesSnapshotFile.Location = new System.Drawing.Point(5, 44);
            this.txtSecondServicesSnapshotFile.Name = "txtSecondServicesSnapshotFile";
            this.txtSecondServicesSnapshotFile.Size = new System.Drawing.Size(467, 20);
            this.txtSecondServicesSnapshotFile.TabIndex = 3;
            // 
            // txtFirstServicesSnapshotFile
            // 
            this.txtFirstServicesSnapshotFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstServicesSnapshotFile.Location = new System.Drawing.Point(5, 19);
            this.txtFirstServicesSnapshotFile.Name = "txtFirstServicesSnapshotFile";
            this.txtFirstServicesSnapshotFile.Size = new System.Drawing.Size(467, 20);
            this.txtFirstServicesSnapshotFile.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 338);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnServicesSnapshot);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFileSystemSnapshot);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.txtDataFolder);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "FileSystem and Registry Snapshots";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.TextBox txtDataFolder;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnFileSystemSnapshot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCompareFileSystemSnapshots;
        private System.Windows.Forms.Button btnSelectFirstFileSystemSnapshotFile;
        private System.Windows.Forms.TextBox txtSecondFileSystemSnapshotFile;
        private System.Windows.Forms.TextBox txtFirstFileSystemSnapshotFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCompareRegistrySnapshots;
        private System.Windows.Forms.Button btnSelectSecondRegistrySnapshotFile;
        private System.Windows.Forms.Button btnSelectFirstRegistrySnapshotFile;
        private System.Windows.Forms.TextBox txtSecondRegistrySnapshotFile;
        private System.Windows.Forms.TextBox txtFirstRegistrySnapshotFile;
        private System.Windows.Forms.Button btnSelectSecondFileSystemSnapshotFile;
        private System.Windows.Forms.Button btnServicesSnapshot;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCompareServicesSnapshots;
        private System.Windows.Forms.Button btnSelectSecondServicesSnapshotFile;
        private System.Windows.Forms.Button btnSelectFirstServicesSnapshotFile;
        private System.Windows.Forms.TextBox txtSecondServicesSnapshotFile;
        private System.Windows.Forms.TextBox txtFirstServicesSnapshotFile;
    }
}

