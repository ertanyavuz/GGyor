using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N11Entegrator
{
    public partial class ComboAttributeControl : AttributeControlBase
    {
        public ComboAttributeControl()
        {
            InitializeComponent();
        }

        protected string _attributeValue;

        public override string AttributeValue {
            get
            {
                var value = cmbValue.SelectedItem;
                return (value ?? "").ToString();
            }
            set
            {
                _attributeValue = value;
                if (cmbValue != null && cmbValue.Items.Count > 0)
                    cmbValue.SelectedItem = value;
            }
        }


        private void ComboAttributeControl_Load(object sender, EventArgs e)
        {
            if (this.AttributeModel != null)
            {
                lblName.Text = this.AttributeModel.name;
                if (this.AttributeModel.mandatory)
                    lblName.Font = new Font(lblName.Font, FontStyle.Bold);
                else
                    cmbValue.Items.Add("<Seçiniz>");

                foreach (var keyValuePair in this.AttributeModel.values)
                {
                    cmbValue.Items.Add(keyValuePair.Value);
                }

                if (!String.IsNullOrWhiteSpace(_attributeValue)/* && this.AttributeModel.values.Any(x => x.Value == this.AttributeValue)*/)
                    cmbValue.SelectedItem = _attributeValue;
            }
        }
    }


}
