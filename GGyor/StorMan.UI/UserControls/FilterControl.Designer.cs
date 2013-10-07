namespace StorMan.UI.UserControls
{
    partial class FilterControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFieldName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFilterType = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alan";
            // 
            // cmbFieldName
            // 
            this.cmbFieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFieldName.FormattingEnabled = true;
            this.cmbFieldName.Location = new System.Drawing.Point(65, 3);
            this.cmbFieldName.Name = "cmbFieldName";
            this.cmbFieldName.Size = new System.Drawing.Size(190, 21);
            this.cmbFieldName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filtre Tipi";
            // 
            // cmbFilterType
            // 
            this.cmbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterType.FormattingEnabled = true;
            this.cmbFilterType.Location = new System.Drawing.Point(65, 30);
            this.cmbFilterType.Name = "cmbFilterType";
            this.cmbFilterType.Size = new System.Drawing.Size(190, 21);
            this.cmbFilterType.TabIndex = 3;
            this.cmbFilterType.SelectedIndexChanged += new System.EventHandler(this.cmbFilterType_SelectedIndexChanged);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(65, 57);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(190, 20);
            this.txtValue.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Değer";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(65, 57);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(91, 20);
            this.txtFrom.TabIndex = 6;
            this.txtFrom.Visible = false;
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(164, 57);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(91, 20);
            this.txtTo.TabIndex = 7;
            this.txtTo.Visible = false;
            // 
            // FilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.cmbFilterType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFieldName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Name = "FilterControl";
            this.Size = new System.Drawing.Size(261, 84);
            this.Load += new System.EventHandler(this.FilterControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFieldName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFilterType;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtTo;
    }
}
