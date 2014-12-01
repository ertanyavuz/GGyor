using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StorMan.Model;

namespace StomMan.UI.Lib
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

            lbl2ndAlan.Visible = false;
            cmbSecondField.Visible = false;

            if (this.Operation != null)
            {
                cmbField.SelectedItem = this.Operation.FieldName;
                cmbOperation.SelectedItem = this.Operation.OperationType.ToString();
                txtValue.Text = this.Operation.Value.ToString();
            }
        }

        private void cmbSecondField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecondField.SelectedItem != null && cmbSecondField.SelectedItem.ToString() == OperationTypeEnum.KurDönüşümü.ToString())
            {
                lbl2ndAlan.Visible = true;
                cmbSecondField.Visible = true;
            }
            else
            {
                lbl2ndAlan.Visible = false;
                cmbSecondField.Visible = false;
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
