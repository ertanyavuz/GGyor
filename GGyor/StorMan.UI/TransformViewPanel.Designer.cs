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
            StorMan.Model.OperationModel operationModel1 = new StorMan.Model.OperationModel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTransformName = new System.Windows.Forms.TextBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEditOperation = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnEditFilter = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbOperations = new System.Windows.Forms.ListBox();
            this.operationControl1 = new StorMan.UI.UserControls.OperationControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.comparerDataGrid1 = new StorMan.UI.ComparerDataGrid();
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
            // btnEditOperation
            // 
            this.btnEditOperation.Location = new System.Drawing.Point(3, 5);
            this.btnEditOperation.Name = "btnEditOperation";
            this.btnEditOperation.Size = new System.Drawing.Size(75, 23);
            this.btnEditOperation.TabIndex = 7;
            this.btnEditOperation.Text = "Düzenle";
            this.btnEditOperation.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Size = new System.Drawing.Size(991, 216);
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
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(991, 141);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnEditOperation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(894, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(94, 135);
            this.panel1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
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
            this.splitContainer3.Panel2.Controls.Add(this.operationControl1);
            this.splitContainer3.Size = new System.Drawing.Size(785, 135);
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
            // 
            // operationControl1
            // 
            this.operationControl1.Location = new System.Drawing.Point(3, 5);
            this.operationControl1.Name = "operationControl1";
            operationModel1.DataType = null;
            operationModel1.FieldName = null;
            operationModel1.ID = 0;
            operationModel1.Name = null;
            operationModel1.OperationType = StorMan.Model.OperationTypeEnum.Carpma;
            operationModel1.Order = null;
            operationModel1.Value = "";
            this.operationControl1.Operation = operationModel1;
            this.operationControl1.Size = new System.Drawing.Size(283, 111);
            this.operationControl1.TabIndex = 0;
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
            this.splitContainer2.Panel2.Controls.Add(this.comparerDataGrid1);
            this.splitContainer2.Size = new System.Drawing.Size(991, 773);
            this.splitContainer2.SplitterDistance = 216;
            this.splitContainer2.TabIndex = 9;
            // 
            // comparerDataGrid1
            // 
            this.comparerDataGrid1.ColumnsToCompare = null;
            this.comparerDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comparerDataGrid1.Location = new System.Drawing.Point(0, 0);
            this.comparerDataGrid1.ModifiedColumnSuffix = "_1";
            this.comparerDataGrid1.ModifiedDataTable = null;
            this.comparerDataGrid1.Name = "comparerDataGrid1";
            this.comparerDataGrid1.OriginalDataTable = null;
            this.comparerDataGrid1.Size = new System.Drawing.Size(991, 553);
            this.comparerDataGrid1.TabIndex = 0;
            this.comparerDataGrid1.ViewType = StorMan.UI.ComparerDataGrid.ViewTypeEnum.Original;
            // 
            // TransformViewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "TransformViewPanel";
            this.Size = new System.Drawing.Size(991, 773);
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
        private System.Windows.Forms.Button btnEditOperation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ComparerDataGrid comparerDataGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnEditFilter;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox lbOperations;
        private UserControls.OperationControl operationControl1;
    }
}
