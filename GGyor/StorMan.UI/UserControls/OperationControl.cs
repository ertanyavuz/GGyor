using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StorMan.Model;

namespace StorMan.UI.UserControls
{
    public partial class OperationControl : UserControl
    {
        public OperationControl()
        {
            _operation = new OperationModel();
            InitializeComponent();
        }

        private OperationModel _operation;
        public OperationModel Operation
        {
            get { return _operation; }
            set
            {
                _operation = value ?? new OperationModel();
                setFields();
            }
        }

        private bool running = false;
        private void OperationControl_Load(object sender, EventArgs e)
        {
            cmbOperationType.Items.AddRange(Enum.GetNames(typeof (OperationTypeEnum)).ToArray<object>());
            
            if (this._operation != null)
            {
                setFields();
            }
            else
            {
                _operation = new OperationModel();
            }
        }

        private void cmbFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (running)
                return;
            this._operation.FieldName = (cmbFieldName.SelectedItem ?? "").ToString();
        }

        private void cmbOperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (running)
                return;
            var opType = OperationTypeEnum.Toplama;
            Enum.TryParse((cmbFieldName.SelectedItem ?? OperationTypeEnum.Toplama).ToString(), true, out opType);

            this._operation.OperationType = opType;
        }

        private void setFields()
        {
            running = true;
            txtName.Text = _operation.Name;
            cmbFieldName.SelectedItem = _operation.FieldName;
            cmbOperationType.SelectedItem = _operation.OperationType.ToString();
            txtValue.Text = _operation.Value.ToString();
            running = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (running)
                return;
            this._operation.Name = txtName.Text;
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            if (running)
                return;
            this._operation.Value = txtValue.Text;
        }



    }
}
