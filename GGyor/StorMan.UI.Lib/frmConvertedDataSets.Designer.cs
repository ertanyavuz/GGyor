﻿namespace StomMan.UI.Lib
{
    partial class frmConvertedDataSets
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
            this.convertedDataSetView1 = new StomMan.UI.Lib.ConvertedDataSetView();
            this.SuspendLayout();
            // 
            // convertedDataSetView1
            // 
            this.convertedDataSetView1.Location = new System.Drawing.Point(4, 12);
            this.convertedDataSetView1.Name = "convertedDataSetView1";
            this.convertedDataSetView1.Size = new System.Drawing.Size(663, 286);
            this.convertedDataSetView1.TabIndex = 0;
            // 
            // frmConvertedDataSets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 358);
            this.Controls.Add(this.convertedDataSetView1);
            this.Name = "frmConvertedDataSets";
            this.Text = "Ürün Listelerim";
            this.Load += new System.EventHandler(this.frmConvertedDataSets_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private StomMan.UI.Lib.ConvertedDataSetView convertedDataSetView1;


    }
}