using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StorMan.Business;
using StorMan.Model;

namespace ElektrostilXmlEditor
{
    public partial class frmOperation : Form
    {
        public frmOperation()
        {
            InitializeComponent();
            FieldList = new List<string>();
        }

        public OperationModel Operation { get; set; }
        public List<string> FieldList { get; set; }

        private void frmOperation_Load(object sender, EventArgs e)
        {
            foreach (var field in FieldList)
            {
                cmbField.Items.Add(field);
            }

            var enumList = Enum.GetNames(typeof(OperationTypeEnum));
            foreach (var enumStr in enumList)
            {
                cmbOperation.Items.Add(enumStr);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Operation = new OperationModel
                {
                    DataType = typeof(string),
                    FieldName = cmbField.Text,
                    //Operation = XmlTransformOperation.TryParse(cmbOperation.Text)
                    Value =  txtValue.Text
                };
            OperationTypeEnum op;
            Enum.TryParse(cmbOperation.Text, true, out op);
            this.Operation.OperationType = op;
            if (op == OperationTypeEnum.Toplama || op == OperationTypeEnum.Carpma)
            {
                this.Operation.Value = OperationModel.floatToStr(OperationModel.stringToFloat(txtValue.Text));
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
