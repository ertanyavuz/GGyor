namespace StorMan.UI
{
    partial class TransformViewPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformViewPanel));
            StorMan.Model.OperationModel operationModel2 = new StorMan.Model.OperationModel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTransformName = new System.Windows.Forms.TextBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnEditFilter = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnEkle = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbOperations = new System.Windows.Forms.ListBox();
            this.operationControl = new StorMan.UI.UserControls.OperationControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbModified = new System.Windows.Forms.RadioButton();
            this.rbOriginal = new System.Windows.Forms.RadioButton();
            this.rbComparison = new System.Windows.Forms.RadioButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.grid = new StorMan.UI.ComparerDataGrid();
            this.chkHideUnmodifiedColumns = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dönüşüm Adı";
            // 
            // txtTransformName
            // 
            this.txtTransformName.Location = new System.Drawing.Point(103, 9);
            this.txtTransformName.Name = "txtTransformName";
            this.txtTransformName.Size = new System.Drawing.Size(329, 20);
            this.txtTransformName.TabIndex = 1;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(103, 35);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.ReadOnly = true;
            this.txtFilter.Size = new System.Drawing.Size(329, 20);
            this.txtFilter.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filtre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Operasyonlar";
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
            this.splitContainer1.Panel1.Controls.Add(this.btnEditFilter);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtTransformName);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtFilter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1106, 216);
            this.splitContainer1.SplitterDistance = 71;
            this.splitContainer1.TabIndex = 8;
            // 
            // btnEditFilter
            // 
            this.btnEditFilter.Location = new System.Drawing.Point(438, 33);
            this.btnEditFilter.Name = "btnEditFilter";
            this.btnEditFilter.Size = new System.Drawing.Size(75, 23);
            this.btnEditFilter.TabIndex = 4;
            this.btnEditFilter.Text = "Düzenle";
            this.btnEditFilter.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 700F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1106, 141);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnKaydet);
            this.panel1.Controls.Add(this.btnSil);
            this.panel1.Controls.Add(this.btnEkle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(803, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(114, 135);
            this.panel1.TabIndex = 6;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(3, 32);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 23);
            this.btnKaydet.TabIndex = 11;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(3, 61);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 23);
            this.btnSil.TabIndex = 10;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(3, 3);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(75, 23);
            this.btnEkle.TabIndex = 9;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(103, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbOperations);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.operationControl);
            this.splitContainer3.Size = new System.Drawing.Size(694, 135);
            this.splitContainer3.SplitterDistance = 392;
            this.splitContainer3.TabIndex = 7;
            // 
            // lbOperations
            // 
            this.lbOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOperations.FormattingEnabled = true;
            this.lbOperations.Location = new System.Drawing.Point(0, 0);
            this.lbOperations.Name = "lbOperations";
            this.lbOperations.Size = new System.Drawing.Size(392, 135);
            this.lbOperations.TabIndex = 0;
            this.lbOperations.SelectedIndexChanged += new System.EventHandler(this.lbOperations_SelectedIndexChanged);
            // 
            // operationControl
            // 
            this.operationControl.FieldList = ((System.Collections.Generic.List<string>)(resources.GetObject("operationControl.FieldList")));
            this.operationControl.Location = new System.Drawing.Point(3, 5);
            this.operationControl.Name = "operationControl";
            operationModel2.DataType = null;
            operationModel2.FieldName = null;
            operationModel2.ID = 0;
            operationModel2.Name = null;
            operationModel2.OperationType = StorMan.Model.OperationTypeEnum.Carpma;
            operationModel2.Order = null;
            operationModel2.Value = "";
            this.operationControl.Operation = operationModel2;
            this.operationControl.Size = new System.Drawing.Size(283, 111);
            this.operationControl.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkHideUnmodifiedColumns);
            this.panel2.Controls.Add(this.rbModified);
            this.panel2.Controls.Add(this.rbOriginal);
            this.panel2.Controls.Add(this.rbComparison);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(923, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 135);
            this.panel2.TabIndex = 8;
            // 
            // rbModified
            // 
            this.rbModified.AutoSize = true;
            this.rbModified.Location = new System.Drawing.Point(12, 35);
            this.rbModified.Name = "rbModified";
            this.rbModified.Size = new System.Drawing.Size(85, 17);
            this.rbModified.TabIndex = 2;
            this.rbModified.TabStop = true;
            this.rbModified.Text = "Değişen Veri";
            this.rbModified.UseVisualStyleBackColor = true;
            this.rbModified.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbOriginal
            // 
            this.rbOriginal.AutoSize = true;
            this.rbOriginal.Location = new System.Drawing.Point(12, 6);
            this.rbOriginal.Name = "rbOriginal";
            this.rbOriginal.Size = new System.Drawing.Size(75, 17);
            this.rbOriginal.TabIndex = 1;
            this.rbOriginal.TabStop = true;
            this.rbOriginal.Text = "Orjinal Veri";
            this.rbOriginal.UseVisualStyleBackColor = true;
            this.rbOriginal.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbComparison
            // 
            this.rbComparison.AutoSize = true;
            this.rbComparison.Checked = true;
            this.rbComparison.Location = new System.Drawing.Point(12, 62);
            this.rbComparison.Name = "rbComparison";
            this.rbComparison.Size = new System.Drawing.Size(117, 17);
            this.rbComparison.TabIndex = 0;
            this.rbComparison.TabStop = true;
            this.rbComparison.Text = "Karşılaştırmalı Tablo";
            this.rbComparison.UseVisualStyleBackColor = true;
            this.rbComparison.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grid);
            this.splitContainer2.Size = new System.Drawing.Size(1106, 773);
            this.splitContainer2.SplitterDistance = 216;
            this.splitContainer2.TabIndex = 9;
            // 
            // grid
            // 
            this.grid.ColumnsToCompare = null;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.ModifiedColumnSuffix = "_1";
            this.grid.ModifiedDataTable = null;
            this.grid.Name = "grid";
            this.grid.OriginalDataTable = null;
            this.grid.ShowTransformedColumnsOnly = true;
            this.grid.Size = new System.Drawing.Size(1106, 553);
            this.grid.TabIndex = 0;
            this.grid.ViewType = StorMan.UI.ComparerDataGrid.ViewTypeEnum.Both;
            // 
            // chkHideUnmodifiedColumns
            // 
            this.chkHideUnmodifiedColumns.AutoSize = true;
            this.chkHideUnmodifiedColumns.Checked = true;
            this.chkHideUnmodifiedColumns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHideUnmodifiedColumns.Location = new System.Drawing.Point(12, 93);
            this.chkHideUnmodifiedColumns.Name = "chkHideUnmodifiedColumns";
            this.chkHideUnmodifiedColumns.Size = new System.Drawing.Size(150, 17);
            this.chkHideUnmodifiedColumns.TabIndex = 3;
            this.chkHideUnmodifiedColumns.Text = "Değişmeyen sütunları gizle";
            this.chkHideUnmodifiedColumns.UseVisualStyleBackColor = true;
            this.chkHideUnmodifiedColumns.CheckedChanged += new System.EventHandler(this.chkHideUnmodifiedColumns_CheckedChanged);
            // 
            // TransformViewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "TransformViewPanel";
            this.Size = new System.Drawing.Size(1106, 773);
            this.Load += new System.EventHandler(this.TransformViewPanel_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTransformName;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ComparerDataGrid grid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditFilter;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox lbOperations;
        private UserControls.OperationControl operationControl;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbModified;
        private System.Windows.Forms.RadioButton rbOriginal;
        private System.Windows.Forms.RadioButton rbComparison;
        private System.Windows.Forms.CheckBox chkHideUnmodifiedColumns;
    }
}
