﻿namespace N11Entegrator
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeCds = new System.Windows.Forms.TreeView();
            this.bodyPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.categoryTreeView1 = new StorMan.UI.CategoryTreeView();
            this.treeN11Categories = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnReloadCategories = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveCategoryMaps = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeProductCategories = new System.Windows.Forms.TreeView();
            this.tabProductComparison = new System.Windows.Forms.TabControl();
            this.tpYeniler = new System.Windows.Forms.TabPage();
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.tpGuncellemeler = new System.Windows.Forms.TabPage();
            this.grid2 = new System.Windows.Forms.DataGridView();
            this.tpSifirlanacaklar = new System.Windows.Forms.TabPage();
            this.grid3 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnDownloadProducts = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveAttributes = new System.Windows.Forms.ToolStripButton();
            this.btnSetCategory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStartUpdate = new System.Windows.Forms.ToolStripButton();
            this.btnStopUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcessNew = new System.Windows.Forms.ToolStripButton();
            this.btnProcessUpdates = new System.Windows.Forms.ToolStripButton();
            this.btnProcessDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStokGuncelle = new System.Windows.Forms.ToolStripButton();
            this.btnFiyatGuncelle = new System.Windows.Forms.ToolStripButton();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.btnTransformKaydet = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabProductComparison.SuspendLayout();
            this.tpYeniler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.tpGuncellemeler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            this.tpSifirlanacaklar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(1145, 712);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1137, 686);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dönüşümler";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeCds);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bodyPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1131, 641);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeCds
            // 
            this.treeCds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCds.FullRowSelect = true;
            this.treeCds.HideSelection = false;
            this.treeCds.Location = new System.Drawing.Point(0, 0);
            this.treeCds.Name = "treeCds";
            this.treeCds.Size = new System.Drawing.Size(199, 641);
            this.treeCds.TabIndex = 0;
            this.treeCds.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCds_AfterSelect);
            // 
            // bodyPanel
            // 
            this.bodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bodyPanel.Location = new System.Drawing.Point(0, 0);
            this.bodyPanel.Name = "bodyPanel";
            this.bodyPanel.Size = new System.Drawing.Size(928, 641);
            this.bodyPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTransformKaydet});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1131, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1137, 686);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Kategori Eşleştirme";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(3, 42);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.categoryTreeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.treeN11Categories);
            this.splitContainer2.Size = new System.Drawing.Size(1131, 641);
            this.splitContainer2.SplitterDistance = 370;
            this.splitContainer2.TabIndex = 3;
            // 
            // categoryTreeView1
            // 
            this.categoryTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryTreeView1.Location = new System.Drawing.Point(0, 0);
            this.categoryTreeView1.Name = "categoryTreeView1";
            this.categoryTreeView1.Size = new System.Drawing.Size(370, 641);
            this.categoryTreeView1.TabIndex = 2;
            // 
            // treeN11Categories
            // 
            this.treeN11Categories.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeN11Categories.Location = new System.Drawing.Point(0, 0);
            this.treeN11Categories.Name = "treeN11Categories";
            this.treeN11Categories.Size = new System.Drawing.Size(401, 641);
            this.treeN11Categories.TabIndex = 1;
            this.treeN11Categories.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeN11Categories_BeforeExpand);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnReloadCategories,
            this.toolStripSeparator5,
            this.btnSaveCategoryMaps});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1131, 39);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnReloadCategories
            // 
            this.btnReloadCategories.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReloadCategories.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadCategories.Image")));
            this.btnReloadCategories.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReloadCategories.Name = "btnReloadCategories";
            this.btnReloadCategories.Size = new System.Drawing.Size(36, 36);
            this.btnReloadCategories.Text = "Yenile";
            this.btnReloadCategories.Click += new System.EventHandler(this.btnReloadCategories_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // btnSaveCategoryMaps
            // 
            this.btnSaveCategoryMaps.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveCategoryMaps.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveCategoryMaps.Image")));
            this.btnSaveCategoryMaps.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCategoryMaps.Name = "btnSaveCategoryMaps";
            this.btnSaveCategoryMaps.Size = new System.Drawing.Size(36, 36);
            this.btnSaveCategoryMaps.Text = "Eşleşmeleri Kaydet";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer4);
            this.tabPage3.Controls.Add(this.toolStrip3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1137, 686);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Ürün Güncelleme";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.Location = new System.Drawing.Point(3, 57);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lbLog);
            this.splitContainer4.Size = new System.Drawing.Size(1131, 626);
            this.splitContainer4.SplitterDistance = 453;
            this.splitContainer4.TabIndex = 3;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer5.Size = new System.Drawing.Size(1131, 453);
            this.splitContainer5.SplitterDistance = 890;
            this.splitContainer5.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeProductCategories);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabProductComparison);
            this.splitContainer3.Size = new System.Drawing.Size(890, 453);
            this.splitContainer3.SplitterDistance = 194;
            this.splitContainer3.TabIndex = 2;
            // 
            // treeProductCategories
            // 
            this.treeProductCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProductCategories.HideSelection = false;
            this.treeProductCategories.Location = new System.Drawing.Point(0, 0);
            this.treeProductCategories.Name = "treeProductCategories";
            this.treeProductCategories.Size = new System.Drawing.Size(194, 453);
            this.treeProductCategories.TabIndex = 1;
            this.treeProductCategories.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeN11Categories_BeforeExpand);
            // 
            // tabProductComparison
            // 
            this.tabProductComparison.Controls.Add(this.tpYeniler);
            this.tabProductComparison.Controls.Add(this.tpGuncellemeler);
            this.tabProductComparison.Controls.Add(this.tpSifirlanacaklar);
            this.tabProductComparison.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProductComparison.Location = new System.Drawing.Point(0, 0);
            this.tabProductComparison.Name = "tabProductComparison";
            this.tabProductComparison.SelectedIndex = 0;
            this.tabProductComparison.Size = new System.Drawing.Size(692, 453);
            this.tabProductComparison.TabIndex = 12;
            // 
            // tpYeniler
            // 
            this.tpYeniler.Controls.Add(this.grid1);
            this.tpYeniler.Location = new System.Drawing.Point(4, 22);
            this.tpYeniler.Name = "tpYeniler";
            this.tpYeniler.Padding = new System.Windows.Forms.Padding(3);
            this.tpYeniler.Size = new System.Drawing.Size(684, 427);
            this.tpYeniler.TabIndex = 0;
            this.tpYeniler.Text = "Yeniler";
            this.tpYeniler.UseVisualStyleBackColor = true;
            // 
            // grid1
            // 
            this.grid1.AllowUserToAddRows = false;
            this.grid1.AllowUserToOrderColumns = true;
            this.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Location = new System.Drawing.Point(3, 3);
            this.grid1.Name = "grid1";
            this.grid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid1.Size = new System.Drawing.Size(678, 421);
            this.grid1.TabIndex = 5;
            this.grid1.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            // 
            // tpGuncellemeler
            // 
            this.tpGuncellemeler.Controls.Add(this.grid2);
            this.tpGuncellemeler.Location = new System.Drawing.Point(4, 22);
            this.tpGuncellemeler.Name = "tpGuncellemeler";
            this.tpGuncellemeler.Padding = new System.Windows.Forms.Padding(3);
            this.tpGuncellemeler.Size = new System.Drawing.Size(684, 427);
            this.tpGuncellemeler.TabIndex = 1;
            this.tpGuncellemeler.Text = "Güncellemeler";
            this.tpGuncellemeler.UseVisualStyleBackColor = true;
            // 
            // grid2
            // 
            this.grid2.AllowUserToAddRows = false;
            this.grid2.AllowUserToOrderColumns = true;
            this.grid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid2.Location = new System.Drawing.Point(3, 3);
            this.grid2.Name = "grid2";
            this.grid2.Size = new System.Drawing.Size(678, 421);
            this.grid2.TabIndex = 7;
            this.grid2.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            // 
            // tpSifirlanacaklar
            // 
            this.tpSifirlanacaklar.Controls.Add(this.grid3);
            this.tpSifirlanacaklar.Location = new System.Drawing.Point(4, 22);
            this.tpSifirlanacaklar.Name = "tpSifirlanacaklar";
            this.tpSifirlanacaklar.Size = new System.Drawing.Size(684, 427);
            this.tpSifirlanacaklar.TabIndex = 2;
            this.tpSifirlanacaklar.Text = "Stoğu Sıfırlanacaklar";
            this.tpSifirlanacaklar.UseVisualStyleBackColor = true;
            // 
            // grid3
            // 
            this.grid3.AllowUserToAddRows = false;
            this.grid3.AllowUserToOrderColumns = true;
            this.grid3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid3.Location = new System.Drawing.Point(0, 0);
            this.grid3.Name = "grid3";
            this.grid3.Size = new System.Drawing.Size(684, 427);
            this.grid3.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 453);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ürün Özellikleri";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(231, 434);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(0, 0);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(1131, 169);
            this.lbLog.TabIndex = 0;
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDownloadProducts,
            this.toolStripSeparator1,
            this.btnSaveAttributes,
            this.btnSetCategory,
            this.toolStripSeparator2,
            this.btnStartUpdate,
            this.btnStopUpdate,
            this.toolStripSeparator3,
            this.btnProcessNew,
            this.btnProcessUpdates,
            this.btnProcessDelete,
            this.toolStripSeparator4,
            this.btnStokGuncelle,
            this.btnFiyatGuncelle});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1131, 54);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // btnDownloadProducts
            // 
            this.btnDownloadProducts.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadProducts.Image")));
            this.btnDownloadProducts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownloadProducts.Name = "btnDownloadProducts";
            this.btnDownloadProducts.Size = new System.Drawing.Size(76, 51);
            this.btnDownloadProducts.Text = "Ürünleri Çek";
            this.btnDownloadProducts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDownloadProducts.Click += new System.EventHandler(this.btnDownloadProducts_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // btnSaveAttributes
            // 
            this.btnSaveAttributes.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAttributes.Image")));
            this.btnSaveAttributes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAttributes.Name = "btnSaveAttributes";
            this.btnSaveAttributes.Size = new System.Drawing.Size(101, 51);
            this.btnSaveAttributes.Text = "Özellikleri Kaydet";
            this.btnSaveAttributes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAttributes.Click += new System.EventHandler(this.btnSaveAttributes_Click);
            // 
            // btnSetCategory
            // 
            this.btnSetCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnSetCategory.Image")));
            this.btnSetCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetCategory.Name = "btnSetCategory";
            this.btnSetCategory.Size = new System.Drawing.Size(90, 51);
            this.btnSetCategory.Text = "Kategori Belirle";
            this.btnSetCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSetCategory.Click += new System.EventHandler(this.btnSetCategory_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // btnStartUpdate
            // 
            this.btnStartUpdate.Enabled = false;
            this.btnStartUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnStartUpdate.Image")));
            this.btnStartUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartUpdate.Name = "btnStartUpdate";
            this.btnStartUpdate.Size = new System.Drawing.Size(42, 51);
            this.btnStartUpdate.Text = "Başlat";
            this.btnStartUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStartUpdate.Click += new System.EventHandler(this.btnStartUpdate_Click);
            // 
            // btnStopUpdate
            // 
            this.btnStopUpdate.Enabled = false;
            this.btnStopUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnStopUpdate.Image")));
            this.btnStopUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopUpdate.Name = "btnStopUpdate";
            this.btnStopUpdate.Size = new System.Drawing.Size(48, 51);
            this.btnStopUpdate.Text = "Durdur";
            this.btnStopUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStopUpdate.Click += new System.EventHandler(this.btnStopUpdate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // btnProcessNew
            // 
            this.btnProcessNew.Checked = true;
            this.btnProcessNew.CheckOnClick = true;
            this.btnProcessNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnProcessNew.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessNew.Image")));
            this.btnProcessNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcessNew.Name = "btnProcessNew";
            this.btnProcessNew.Size = new System.Drawing.Size(76, 51);
            this.btnProcessNew.Text = "Yeni Ürünler";
            this.btnProcessNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnProcessUpdates
            // 
            this.btnProcessUpdates.Checked = true;
            this.btnProcessUpdates.CheckOnClick = true;
            this.btnProcessUpdates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnProcessUpdates.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessUpdates.Image")));
            this.btnProcessUpdates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcessUpdates.Name = "btnProcessUpdates";
            this.btnProcessUpdates.Size = new System.Drawing.Size(87, 51);
            this.btnProcessUpdates.Text = "Güncellemeler";
            this.btnProcessUpdates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnProcessDelete
            // 
            this.btnProcessDelete.Checked = true;
            this.btnProcessDelete.CheckOnClick = true;
            this.btnProcessDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnProcessDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessDelete.Image")));
            this.btnProcessDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcessDelete.Name = "btnProcessDelete";
            this.btnProcessDelete.Size = new System.Drawing.Size(83, 51);
            this.btnProcessDelete.Text = "Stok Sıfırlama";
            this.btnProcessDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // btnStokGuncelle
            // 
            this.btnStokGuncelle.Checked = true;
            this.btnStokGuncelle.CheckOnClick = true;
            this.btnStokGuncelle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnStokGuncelle.Image = ((System.Drawing.Image)(resources.GetObject("btnStokGuncelle.Image")));
            this.btnStokGuncelle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStokGuncelle.Name = "btnStokGuncelle";
            this.btnStokGuncelle.Size = new System.Drawing.Size(83, 51);
            this.btnStokGuncelle.Text = "Stok Güncelle";
            this.btnStokGuncelle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnFiyatGuncelle
            // 
            this.btnFiyatGuncelle.Checked = true;
            this.btnFiyatGuncelle.CheckOnClick = true;
            this.btnFiyatGuncelle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnFiyatGuncelle.Image = ((System.Drawing.Image)(resources.GetObject("btnFiyatGuncelle.Image")));
            this.btnFiyatGuncelle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiyatGuncelle.Name = "btnFiyatGuncelle";
            this.btnFiyatGuncelle.Size = new System.Drawing.Size(85, 51);
            this.btnFiyatGuncelle.Text = "Fiyat Güncelle";
            this.btnFiyatGuncelle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // bgw
            // 
            this.bgw.WorkerReportsProgress = true;
            this.bgw.WorkerSupportsCancellation = true;
            this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            this.bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ProgressChanged);
            this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
            // 
            // btnTransformKaydet
            // 
            this.btnTransformKaydet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTransformKaydet.Image = ((System.Drawing.Image)(resources.GetObject("btnTransformKaydet.Image")));
            this.btnTransformKaydet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTransformKaydet.Name = "btnTransformKaydet";
            this.btnTransformKaydet.Size = new System.Drawing.Size(36, 36);
            this.btnTransformKaydet.Text = "Değişiklikleri Kaydet";
            this.btnTransformKaydet.Click += new System.EventHandler(this.btnTransformKaydet_Click);
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(1145, 712);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "N11 Entegrator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabProductComparison.ResumeLayout(false);
            this.tpYeniler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.tpGuncellemeler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            this.tpSifirlanacaklar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeCds;
        private System.Windows.Forms.Panel bodyPanel;
        private StorMan.UI.CategoryTreeView categoryTreeView1;
        private System.Windows.Forms.ToolStripButton btnReloadCategories;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeN11Categories;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView treeProductCategories;
        private System.Windows.Forms.TabControl tabProductComparison;
        private System.Windows.Forms.TabPage tpYeniler;
        private System.Windows.Forms.DataGridView grid1;
        private System.Windows.Forms.TabPage tpGuncellemeler;
        private System.Windows.Forms.DataGridView grid2;
        private System.Windows.Forms.TabPage tpSifirlanacaklar;
        private System.Windows.Forms.DataGridView grid3;
        private System.Windows.Forms.ToolStripButton btnDownloadProducts;
        private System.Windows.Forms.ToolStripButton btnStartUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnStopUpdate;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripButton btnSaveAttributes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.ComponentModel.BackgroundWorker bgw;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnProcessNew;
        private System.Windows.Forms.ToolStripButton btnProcessUpdates;
        private System.Windows.Forms.ToolStripButton btnProcessDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnStokGuncelle;
        private System.Windows.Forms.ToolStripButton btnFiyatGuncelle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnSaveCategoryMaps;
        private System.Windows.Forms.ToolStripButton btnSetCategory;
        private System.Windows.Forms.ToolStripButton btnTransformKaydet;

    }
}