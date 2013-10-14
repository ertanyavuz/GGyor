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
    public partial class ConvertedDataSetControl : UserControl
    {
        public ConvertedDataSetControl()
        {
            InitializeComponent();
        }

        private ConvertedDataSetModel _convertedDataSet;
        public ConvertedDataSetModel ConvertedDataSet
        {
            get { return _convertedDataSet; }
            set
            {
                _convertedDataSet = value;
            }
        }

        private void ConvertedDataSetControl_Load(object sender, EventArgs e)
        {
            if (this.ConvertedDataSet != null)
                setFields();
        }

        private bool running = false;
        private void setFields()
        {
            running = true;
            txtName.Text = _convertedDataSet == null ? "" : _convertedDataSet.Name;
            txtSourceXml.Text = _convertedDataSet == null ? "" : _convertedDataSet.SourceXmlPath;
            running = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!running && this.ConvertedDataSet != null)
                this.ConvertedDataSet.Name = txtName.Text;
        }

        private void txtSourceXml_TextChanged(object sender, EventArgs e)
        {
            if (!running && this.ConvertedDataSet != null)
                this.ConvertedDataSet.SourceXmlPath = txtSourceXml.Text;
        }
    }
}
