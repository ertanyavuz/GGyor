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
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.btnGetCategories = new System.Windows.Forms.Button();
            this.tree = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveAttribute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid3)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetSource
            // 
            this.btnGetSource.Location = new System.Drawing.Point(39, 53);
            this.btnGetSource.Name = "btnGetSource";
            this.btnGetSource.Size = new System.Drawing.Size(118, 23);
            this.btnGetSource.TabIndex = 0;
            this.btnGetSource.Text = "Kaynak XML\'i Çek";
            this.btnGetSource.UseVisualStyleBackColor = true;
            this.btnGetSource.Click += new System.EventHandler(this.btnGetSource_Click);
            // 
            // btnGetDestination
            // 
            this.btnGetDestination.Location = new System.Drawing.Point(39, 82);
            this.btnGetDestination.Name = "btnGetDestination";
            this.btnGetDestination.Size = new System.Drawing.Size(118, 23);
            this.btnGetDestination.TabIndex = 1;
            this.btnGetDestination.Text = "N11 Ürünlerini Çek";
            this.btnGetDestination.UseVisualStyleBackColor = true;
            this.btnGetDestination.Click += new System.EventHandler(this.btnGetDestination_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(255, 23);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(279, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(252, 58);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 3;
            // 
            // btnCompareLists
            // 
            this.btnCompareLists.Location = new System.Drawing.Point(39, 111);
            this.btnCompareLists.Name = "btnCompareLists";
            this.btnCompareLists.Size = new System.Drawing.Size(118, 23);
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
            this.grid1.Size = new System.Drawing.Size(689, 546);
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
            this.grid2.Size = new System.Drawing.Size(689, 546);
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
            this.grid3.Size = new System.Drawing.Size(695, 552);
            this.grid3.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(255, 82);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(703, 578);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grid1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(695, 552);
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
            this.tabPage2.Size = new System.Drawing.Size(695, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Güncellemeler";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grid3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(695, 552);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Stoğu Sıfırlanacaklar";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnRunUpdate
            // 
            this.btnRunUpdate.Location = new System.Drawing.Point(39, 140);
            this.btnRunUpdate.Name = "btnRunUpdate";
            this.btnRunUpdate.Size = new System.Drawing.Size(118, 23);
            this.btnRunUpdate.TabIndex = 12;
            this.btnRunUpdate.Text = "Başlat";
            this.btnRunUpdate.UseVisualStyleBackColor = true;
            this.btnRunUpdate.Click += new System.EventHandler(this.btnRunUpdate_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(39, 169);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(118, 23);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Durdur";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // bgw
            // 
            this.bgw.WorkerReportsProgress = true;
            this.bgw.WorkerSupportsCancellation = true;
            this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            this.bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ProgressChanged);
            this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
            // 
            // btnGetCategories
            // 
            this.btnGetCategories.Location = new System.Drawing.Point(39, 24);
            this.btnGetCategories.Name = "btnGetCategories";
            this.btnGetCategories.Size = new System.Drawing.Size(118, 23);
            this.btnGetCategories.TabIndex = 14;
            this.btnGetCategories.Text = "Kategorileri Çek";
            this.btnGetCategories.UseVisualStyleBackColor = true;
            this.btnGetCategories.Click += new System.EventHandler(this.btnGetCategories_Click);
            // 
            // tree
            // 
            this.tree.AllowDrop = true;
            this.tree.Location = new System.Drawing.Point(12, 198);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(237, 459);
            this.tree.TabIndex = 15;
            this.tree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tree_BeforeExpand);
            this.tree.DragDrop += new System.Windows.Forms.DragEventHandler(this.tree_DragDrop);
            this.tree.DragOver += new System.Windows.Forms.DragEventHandler(this.tree_DragOver);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(964, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 564);
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(294, 545);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnSaveAttribute
            // 
            this.btnSaveAttribute.Location = new System.Drawing.Point(964, 67);
            this.btnSaveAttribute.Name = "btnSaveAttribute";
            this.btnSaveAttribute.Size = new System.Drawing.Size(118, 23);
            this.btnSaveAttribute.TabIndex = 17;
            this.btnSaveAttribute.Text = "Özellikleri Kaydet";
            this.btnSaveAttribute.UseVisualStyleBackColor = true;
            this.btnSaveAttribute.Click += new System.EventHandler(this.btnSaveAttribute_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 672);
            this.Controls.Add(this.btnSaveAttribute);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tree);
            this.Controls.Add(this.btnGetCategories);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRunUpdate);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCompareLists);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnGetDestination);
            this.Controls.Add(this.btnGetSource);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid3)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.ComponentModel.BackgroundWorker bgw;
        private System.Windows.Forms.Button btnGetCategories;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSaveAttribute;
    }
}

