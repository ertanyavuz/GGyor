namespace N11Entegrator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetSource = new System.Windows.Forms.Button();
            this.btnGetDestination = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCompareLists = new System.Windows.Forms.Button();
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.grid2 = new System.Windows.Forms.DataGridView();
            this.grid3 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnRunUpdate = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.bgw1 = new System.ComponentModel.BackgroundWorker();
            this.btnGetCategories = new System.Windows.Forms.Button();
            this.tree = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveAttribute = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkDontCheckUpdates = new System.Windows.Forms.CheckBox();
            this.btnReloadCategories = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.bgw2 = new System.ComponentModel.BackgroundWorker();
            this.bgwReloadCategories = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid3)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetSource
            // 
            this.btnGetSource.Location = new System.Drawing.Point(3, 41);
            this.btnGetSource.Name = "btnGetSource";
            this.btnGetSource.Size = new System.Drawing.Size(104, 23);
            this.btnGetSource.TabIndex = 0;
            this.btnGetSource.Text = "Kaynak XML\'i Çek";
            this.btnGetSource.UseVisualStyleBackColor = true;
            this.btnGetSource.Click += new System.EventHandler(this.btnGetSource_Click);
            // 
            // btnGetDestination
            // 
            this.btnGetDestination.Location = new System.Drawing.Point(3, 70);
            this.btnGetDestination.Name = "btnGetDestination";
            this.btnGetDestination.Size = new System.Drawing.Size(104, 23);
            this.btnGetDestination.TabIndex = 1;
            this.btnGetDestination.Text = "N11 Ürünlerini Çek";
            this.btnGetDestination.UseVisualStyleBackColor = true;
            this.btnGetDestination.Click += new System.EventHandler(this.btnGetDestination_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(436, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(10, 33);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 3;
            // 
            // btnCompareLists
            // 
            this.btnCompareLists.Location = new System.Drawing.Point(3, 99);
            this.btnCompareLists.Name = "btnCompareLists";
            this.btnCompareLists.Size = new System.Drawing.Size(104, 23);
            this.btnCompareLists.TabIndex = 4;
            this.btnCompareLists.Text = "Karşılaştır";
            this.btnCompareLists.UseVisualStyleBackColor = true;
            this.btnCompareLists.Click += new System.EventHandler(this.btnCompareLists_Click);
            // 
            // grid1
            // 
            this.grid1.AllowUserToAddRows = false;
            this.grid1.AllowUserToOrderColumns = true;
            this.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Location = new System.Drawing.Point(3, 3);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(770, 367);
            this.grid1.TabIndex = 5;
            this.grid1.SelectionChanged += new System.EventHandler(this.grid1_SelectionChanged);
            this.grid1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grid1_MouseDown);
            // 
            // grid2
            // 
            this.grid2.AllowUserToAddRows = false;
            this.grid2.AllowUserToOrderColumns = true;
            this.grid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid2.Location = new System.Drawing.Point(3, 3);
            this.grid2.Name = "grid2";
            this.grid2.Size = new System.Drawing.Size(770, 367);
            this.grid2.TabIndex = 7;
            // 
            // grid3
            // 
            this.grid3.AllowUserToAddRows = false;
            this.grid3.AllowUserToOrderColumns = true;
            this.grid3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid3.Location = new System.Drawing.Point(0, 0);
            this.grid3.Name = "grid3";
            this.grid3.Size = new System.Drawing.Size(776, 373);
            this.grid3.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 399);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grid1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 373);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Yeniler";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grid2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 373);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Güncellemeler";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grid3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(776, 373);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Stoğu Sıfırlanacaklar";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnRunUpdate
            // 
            this.btnRunUpdate.Location = new System.Drawing.Point(3, 128);
            this.btnRunUpdate.Name = "btnRunUpdate";
            this.btnRunUpdate.Size = new System.Drawing.Size(104, 23);
            this.btnRunUpdate.TabIndex = 12;
            this.btnRunUpdate.Text = "Başlat";
            this.btnRunUpdate.UseVisualStyleBackColor = true;
            this.btnRunUpdate.Click += new System.EventHandler(this.btnRunUpdate_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(3, 157);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(104, 23);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Durdur";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // bgw1
            // 
            this.bgw1.WorkerReportsProgress = true;
            this.bgw1.WorkerSupportsCancellation = true;
            this.bgw1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            this.bgw1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw1_ProgressChanged);
            this.bgw1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw1_RunWorkerCompleted);
            // 
            // btnGetCategories
            // 
            this.btnGetCategories.Location = new System.Drawing.Point(3, 12);
            this.btnGetCategories.Name = "btnGetCategories";
            this.btnGetCategories.Size = new System.Drawing.Size(104, 23);
            this.btnGetCategories.TabIndex = 14;
            this.btnGetCategories.Text = "Kategorileri Çek";
            this.btnGetCategories.UseVisualStyleBackColor = true;
            this.btnGetCategories.Click += new System.EventHandler(this.btnGetCategories_Click);
            // 
            // tree
            // 
            this.tree.AllowDrop = true;
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(226, 384);
            this.tree.TabIndex = 15;
            this.tree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tree_BeforeExpand);
            this.tree.DragDrop += new System.Windows.Forms.DragEventHandler(this.tree_DragDrop);
            this.tree.DragOver += new System.Windows.Forms.DragEventHandler(this.tree_DragOver);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 596);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ürün Özellikleri";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(229, 577);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnSaveAttribute
            // 
            this.btnSaveAttribute.Location = new System.Drawing.Point(13, 4);
            this.btnSaveAttribute.Name = "btnSaveAttribute";
            this.btnSaveAttribute.Size = new System.Drawing.Size(118, 23);
            this.btnSaveAttribute.TabIndex = 17;
            this.btnSaveAttribute.Text = "Özellikleri Kaydet";
            this.btnSaveAttribute.UseVisualStyleBackColor = true;
            this.btnSaveAttribute.Click += new System.EventHandler(this.btnSaveAttribute_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkDontCheckUpdates);
            this.splitContainer1.Panel1.Controls.Add(this.btnReloadCategories);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetCategories);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetSource);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetDestination);
            this.splitContainer1.Panel1.Controls.Add(this.btnCompareLists);
            this.splitContainer1.Panel1.Controls.Add(this.btnRunUpdate);
            this.splitContainer1.Panel1.Controls.Add(this.btnStop);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tree);
            this.splitContainer1.Size = new System.Drawing.Size(226, 635);
            this.splitContainer1.SplitterDistance = 247;
            this.splitContainer1.TabIndex = 18;
            // 
            // chkDontCheckUpdates
            // 
            this.chkDontCheckUpdates.AutoSize = true;
            this.chkDontCheckUpdates.Location = new System.Drawing.Point(3, 197);
            this.chkDontCheckUpdates.Name = "chkDontCheckUpdates";
            this.chkDontCheckUpdates.Size = new System.Drawing.Size(137, 17);
            this.chkDontCheckUpdates.TabIndex = 15;
            this.chkDontCheckUpdates.Text = "Güncellemelere bakma.";
            this.chkDontCheckUpdates.UseVisualStyleBackColor = true;
            // 
            // btnReloadCategories
            // 
            this.btnReloadCategories.Location = new System.Drawing.Point(113, 12);
            this.btnReloadCategories.Name = "btnReloadCategories";
            this.btnReloadCategories.Size = new System.Drawing.Size(112, 23);
            this.btnReloadCategories.TabIndex = 14;
            this.btnReloadCategories.Text = "Kategorileri Güncelle";
            this.btnReloadCategories.UseVisualStyleBackColor = true;
            this.btnReloadCategories.Click += new System.EventHandler(this.btnReloadCategories_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnSaveAttribute);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(235, 635);
            this.splitContainer2.SplitterDistance = 35;
            this.splitContainer2.TabIndex = 19;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(1023, 635);
            this.splitContainer3.SplitterDistance = 784;
            this.splitContainer3.TabIndex = 20;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.lbLog);
            this.splitContainer6.Size = new System.Drawing.Size(784, 635);
            this.splitContainer6.SplitterDistance = 458;
            this.splitContainer6.TabIndex = 22;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lblStatus);
            this.splitContainer4.Panel1.Controls.Add(this.progressBar1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer4.Size = new System.Drawing.Size(784, 458);
            this.splitContainer4.SplitterDistance = 55;
            this.splitContainer4.TabIndex = 21;
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(0, 0);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(784, 173);
            this.lbLog.TabIndex = 0;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer5.Size = new System.Drawing.Size(1253, 635);
            this.splitContainer5.SplitterDistance = 226;
            this.splitContainer5.TabIndex = 21;
            // 
            // bgw2
            // 
            this.bgw2.WorkerReportsProgress = true;
            this.bgw2.WorkerSupportsCancellation = true;
            this.bgw2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw2_ProgressChanged);
            this.bgw2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw2_RunWorkerCompleted);
            // 
            // bgwReloadCategories
            // 
            this.bgwReloadCategories.WorkerReportsProgress = true;
            this.bgwReloadCategories.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwReloadCategories_DoWork);
            this.bgwReloadCategories.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwReloadCategories_ProgressChanged);
            this.bgwReloadCategories.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwReloadCategories_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 635);
            this.Controls.Add(this.splitContainer5);
            this.Name = "Form1";
            this.Text = "N11 Entegrator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid3)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetSource;
        private System.Windows.Forms.Button btnGetDestination;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCompareLists;
        private System.Windows.Forms.DataGridView grid1;
        private System.Windows.Forms.DataGridView grid2;
        private System.Windows.Forms.DataGridView grid3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnRunUpdate;
        private System.Windows.Forms.Button btnStop;
        private System.ComponentModel.BackgroundWorker bgw1;
        private System.Windows.Forms.Button btnGetCategories;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSaveAttribute;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.ListBox lbLog;
        private System.ComponentModel.BackgroundWorker bgw2;
        private System.Windows.Forms.CheckBox chkDontCheckUpdates;
        private System.Windows.Forms.Button btnReloadCategories;
        private System.ComponentModel.BackgroundWorker bgwReloadCategories;
    }
}

