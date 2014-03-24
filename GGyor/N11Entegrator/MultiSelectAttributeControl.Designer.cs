namespace N11Entegrator
{
    partial class MultiSelectAttributeControl
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
            this.lblName = new System.Windows.Forms.Label();
            this.chkValue = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoEllipsis = true;
            this.lblName.Location = new System.Drawing.Point(1, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(108, 17);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "label1";
            // 
            // chkValue
            // 
            this.chkValue.FormattingEnabled = true;
            this.chkValue.Location = new System.Drawing.Point(110, 3);
            this.chkValue.Name = "chkValue";
            this.chkValue.ScrollAlwaysVisible = true;
            this.chkValue.Size = new System.Drawing.Size(197, 94);
            this.chkValue.TabIndex = 4;
            // 
            // MultiSelectAttributeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkValue);
            this.Controls.Add(this.lblName);
            this.Name = "MultiSelectAttributeControl";
            this.Size = new System.Drawing.Size(314, 104);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckedListBox chkValue;
    }
}
