using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StorMan.Business;
using StorMan.Model;

namespace ElektrostilXmlEditor
{
    public partial class frmViewXml : Form
    {
        public frmViewXml()
        {
            InitializeComponent();
        }

        public ConvertedDataSetModel ConvertedDataSet { get; set; }

        private ProductsXmlCollection products;

        private void frmViewXml_Load(object sender, EventArgs e)
        {
            if (this.ConvertedDataSet == null)
                return;

            this.Text = this.ConvertedDataSet.Name;

            try
            {
                products = new ProductsXmlCollection(this.ConvertedDataSet.SourceXmlPath);

                grid.DataSource = products.DataTable;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}

