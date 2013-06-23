using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ElektrostilXmlEditor
{
    public partial class frmTransform : Form
    {
        public frmTransform()
        {
            InitializeComponent();
            FieldList = new List<string>();
        }

        public XmlTransform Transform { get; set; }
        public List<string> FieldList { get; set; }

        private void frmTransform_Load(object sender, EventArgs e)
        {
            foreach (var field in FieldList)
            {
                cmbField.Items.Add(field);
            }

            var enumList = Enum.GetNames(typeof(XmlTransformOperation));
            foreach (var enumStr in enumList)
            {
                cmbOperation.Items.Add(enumStr);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Transform = new XmlTransform
                {
                    DataType = typeof(string),
                    FieldName = cmbField.Text,
                    //Operation = XmlTransformOperation.TryParse(cmbOperation.Text)
                    Value = txtValue.Text
                };
            var op = new XmlTransformOperation();
            Enum.TryParse(cmbOperation.Text, true, out op);
            this.Transform.Operation = op;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
