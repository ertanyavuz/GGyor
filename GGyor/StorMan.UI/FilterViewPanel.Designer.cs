﻿namespace StorMan.UI
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbFilters = new System.Windows.Forms.ListBox();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnEkle = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.splitContainer1.Panel1.Controls.Add(this.lbFilters);
            this.splitContainer1.Panel1.Controls.Add(this.btnSil);
            this.splitContainer1.Panel1.Controls.Add(this.btnEkle);
            this.splitContainer1.Panel1.Controls.Add(this.txtFilter);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comparerGrid);
            this.splitContainer1.Size = new System.Drawing.Size(861, 659);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbFilters
            // 
            this.lbFilters.FormattingEnabled = true;
            this.lbFilters.Location = new System.Drawing.Point(67, 40);
            this.lbFilters.Name = "lbFilters";
            this.lbFilters.Size = new System.Drawing.Size(407, 69);
            this.lbFilters.TabIndex = 6;
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(491, 41);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 23);
            this.btnSil.TabIndex = 5;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(491, 12);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(75, 23);
            this.btnEkle.TabIndex = 4;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(67, 14);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(407, 20);
            this.txtFilter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtre";
            // 
            // comparerDataGrid1
            // 
            this.comparerGrid.ColumnsToCompare = null;
            this.comparerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comparerGrid.KeyColumn = null;
            this.comparerGrid.Location = new System.Drawing.Point(0, 0);
            this.comparerGrid.ModifiedColumnSuffix = "_1";
            this.comparerGrid.ModifiedDataTable = null;
            this.comparerGrid.Name = "comparerDataGrid1";
            this.comparerGrid.OriginalDataTable = null;
            this.comparerGrid.Size = new System.Drawing.Size(861, 528);
            this.comparerGrid.TabIndex = 0;
            this.comparerGrid.ViewType = StorMan.UI.ComparerDataGrid.ViewTypeEnum.Original;
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
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbFilters;
        private ComparerDataGrid comparerGrid;
    }
}
