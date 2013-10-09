namespace StorMan.UI.UserControls
{
    partial class OperationControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cmbOperationType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFieldName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Değer";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(86, 83);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(190, 20);
            this.txtValue.TabIndex = 11;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // cmbOperationType
            // 
            this.cmbOperationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperationType.FormattingEnabled = true;
            this.cmbOperationType.Location = new System.Drawing.Point(86, 56);
            this.cmbOperationType.Name = "cmbOperationType";
            this.cmbOperationType.Size = new System.Drawing.Size(190, 21);
            this.cmbOperationType.TabIndex = 10;
            this.cmbOperationType.SelectedIndexChanged += new System.EventHandler(this.cmbOperationType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Operasyon Tipi";
            // 
            // cmbFieldName
            // 
            this.cmbFieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFieldName.FormattingEnabled = true;
            this.cmbFieldName.Location = new System.Drawing.Point(86, 29);
            this.cmbFieldName.Name = "cmbFieldName";
            this.cmbFieldName.Size = new System.Drawing.Size(190, 21);
            this.cmbFieldName.TabIndex = 8;
            this.cmbFieldName.SelectedIndexChanged += new System.EventHandler(this.cmbFieldName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Alan";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(86, 83);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(91, 20);
            this.txtFrom.TabIndex = 13;
            this.txtFrom.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Operasyon Adı";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(86, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(190, 20);
            this.txtName.TabIndex = 14;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(86, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(91, 20);
            this.textBox2.TabIndex = 16;
            this.textBox2.Visible = false;
            // 
            // OperationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.cmbOperationType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFieldName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFrom);
            this.Name = "OperationControl";
            this.Size = new System.Drawing.Size(283, 111);
            this.Load += new System.EventHandler(this.OperationControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cmbOperationType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFieldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox textBox2;
    }
}
