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
            this.btnSaveXml = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnYeniTransform = new System.Windows.Forms.Button();
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
            // btnSaveXml
            // 
            this.btnSaveXml.Location = new System.Drawing.Point(102, 69);
            this.btnSaveXml.Name = "btnSaveXml";
            this.btnSaveXml.Size = new System.Drawing.Size(75, 23);
            this.btnSaveXml.TabIndex = 1;
            this.btnSaveXml.Text = "XML Üret";
            this.btnSaveXml.UseVisualStyleBackColor = true;
            this.btnSaveXml.Click += new System.EventHandler(this.btnSaveXml_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.Filter = "*.xml|XML Dosyaları (*.xml)|*.*|Tüm Dosyalar (*.*)";
            // 
            // btnYeniTransform
            // 
            this.btnYeniTransform.Location = new System.Drawing.Point(183, 69);
            this.btnYeniTransform.Name = "btnYeniTransform";
            this.btnYeniTransform.Size = new System.Drawing.Size(88, 23);
            this.btnYeniTransform.TabIndex = 2;
            this.btnYeniTransform.Text = "Dönüşüm Ekle";
            this.btnYeniTransform.UseVisualStyleBackColor = true;
            this.btnYeniTransform.Click += new System.EventHandler(this.btnYeniTransform_Click);
            // 
            // ConvertedDataSetViewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnYeniTransform);
            this.Controls.Add(this.btnSaveXml);
            this.Controls.Add(this.convertedDataSetControl1);
            this.Name = "ConvertedDataSetViewPanel";
            this.Size = new System.Drawing.Size(486, 276);
            this.Load += new System.EventHandler(this.ConvertedDataSetViewPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ConvertedDataSetControl convertedDataSetControl1;
        private System.Windows.Forms.Button btnSaveXml;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnYeniTransform;
    }
}
