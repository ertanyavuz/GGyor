namespace StorMan.UI
{
    partial class FilterViewPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterViewPanel));
            StorMan.Model.FilterModel filterModel1 = new StorMan.Model.FilterModel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rbModified = new System.Windows.Forms.RadioButton();
            this.rbOriginal = new System.Windows.Forms.RadioButton();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.filterControl = new StorMan.UI.UserControls.FilterControl();
            this.lbFilters = new System.Windows.Forms.ListBox();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnEkle = new System.Windows.Forms.Button();
            this.comparerGrid = new StorMan.UI.ComparerDataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.rbModified);
            this.splitContainer1.Panel1.Controls.Add(this.rbOriginal);
            this.splitContainer1.Panel1.Controls.Add(this.btnKaydet);
            this.splitContainer1.Panel1.Controls.Add(this.filterControl);
            this.splitContainer1.Panel1.Controls.Add(this.lbFilters);
            this.splitContainer1.Panel1.Controls.Add(this.btnSil);
            this.splitContainer1.Panel1.Controls.Add(this.btnEkle);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comparerGrid);
            this.splitContainer1.Size = new System.Drawing.Size(861, 659);
            this.splitContainer1.SplitterDistance = 106;
            this.splitContainer1.TabIndex = 0;
            // 
            // rbModified
            // 
            this.rbModified.AutoSize = true;
            this.rbModified.Checked = true;
            this.rbModified.Location = new System.Drawing.Point(679, 49);
            this.rbModified.Name = "rbModified";
            this.rbModified.Size = new System.Drawing.Size(85, 17);
            this.rbModified.TabIndex = 10;
            this.rbModified.TabStop = true;
            this.rbModified.Text = "Değişen Veri";
            this.rbModified.UseVisualStyleBackColor = true;
            this.rbModified.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbOriginal
            // 
            this.rbOriginal.AutoSize = true;
            this.rbOriginal.Location = new System.Drawing.Point(679, 20);
            this.rbOriginal.Name = "rbOriginal";
            this.rbOriginal.Size = new System.Drawing.Size(75, 17);
            this.rbOriginal.TabIndex = 9;
            this.rbOriginal.Text = "Orjinal Veri";
            this.rbOriginal.UseVisualStyleBackColor = true;
            this.rbOriginal.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(576, 46);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 23);
            this.btnKaydet.TabIndex = 8;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // filterControl
            // 
            this.filterControl.FieldList = ((System.Collections.Generic.List<string>)(resources.GetObject("filterControl.FieldList")));
            filterModel1.FieldName = null;
            filterModel1.FilterType = StorMan.Model.FilterTypeEnum.Equals;
            filterModel1.ID = 0;
            filterModel1.Value = null;
            this.filterControl.Filter = filterModel1;
            this.filterControl.Location = new System.Drawing.Point(8, 17);
            this.filterControl.Name = "filterControl";
            this.filterControl.Size = new System.Drawing.Size(261, 84);
            this.filterControl.TabIndex = 7;
            // 
            // lbFilters
            // 
            this.lbFilters.FormattingEnabled = true;
            this.lbFilters.Location = new System.Drawing.Point(275, 17);
            this.lbFilters.Name = "lbFilters";
            this.lbFilters.Size = new System.Drawing.Size(295, 82);
            this.lbFilters.TabIndex = 6;
            this.lbFilters.SelectedIndexChanged += new System.EventHandler(this.lbFilters_SelectedIndexChanged);
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(576, 75);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 23);
            this.btnSil.TabIndex = 5;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(576, 17);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(75, 23);
            this.btnEkle.TabIndex = 4;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // comparerGrid
            // 
            this.comparerGrid.ColumnsToCompare = null;
            this.comparerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comparerGrid.Location = new System.Drawing.Point(0, 0);
            this.comparerGrid.ModifiedColumnSuffix = "_1";
            this.comparerGrid.ModifiedDataTable = null;
            this.comparerGrid.Name = "comparerGrid";
            this.comparerGrid.OriginalDataTable = null;
            this.comparerGrid.ShowTransformedColumnsOnly = false;
            this.comparerGrid.Size = new System.Drawing.Size(861, 549);
            this.comparerGrid.TabIndex = 0;
            this.comparerGrid.ViewType = StorMan.UI.ComparerDataGrid.ViewTypeEnum.Modified;
            // 
            // FilterViewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FilterViewPanel";
            this.Size = new System.Drawing.Size(861, 659);
            this.Load += new System.EventHandler(this.FilterViewPanel_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.ListBox lbFilters;
        private UserControls.FilterControl filterControl;
        private System.Windows.Forms.Button btnKaydet;
        private ComparerDataGrid comparerGrid;
        private System.Windows.Forms.RadioButton rbModified;
        private System.Windows.Forms.RadioButton rbOriginal;
    }
}
