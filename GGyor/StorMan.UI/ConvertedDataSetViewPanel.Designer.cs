namespace StorMan.UI
{
    partial class ConvertedDataSetViewPanel
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
            this.convertedDataSetControl1 = new StorMan.UI.UserControls.ConvertedDataSetControl();
            this.btnSil = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // convertedDataSetControl1
            // 
            this.convertedDataSetControl1.ConvertedDataSet = null;
            this.convertedDataSetControl1.Location = new System.Drawing.Point(3, 3);
            this.convertedDataSetControl1.Name = "convertedDataSetControl1";
            this.convertedDataSetControl1.Size = new System.Drawing.Size(297, 60);
            this.convertedDataSetControl1.TabIndex = 0;
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(101, 69);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 23);
            this.btnSil.TabIndex = 1;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // ConvertedDataSetViewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.convertedDataSetControl1);
            this.Name = "ConvertedDataSetViewPanel";
            this.Size = new System.Drawing.Size(486, 276);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ConvertedDataSetControl convertedDataSetControl1;
        private System.Windows.Forms.Button btnSil;
    }
}
