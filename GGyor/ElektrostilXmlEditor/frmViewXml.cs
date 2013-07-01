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

        //private ProductsXmlCollection products;
        private DataTable dataTable;

        private void frmViewXml_Load(object sender, EventArgs e)
        {
            if (this.ConvertedDataSet == null)
                return;

            this.Text = this.ConvertedDataSet.Name;
            
            try
            {
                //products = new ProductsXmlCollection(this.ConvertedDataSet.SourceXmlPath);
                this.ConvertedDataSet.LoadXml();
                this.ConvertedDataSet.ApplyTransforms();

                this.dataTable = this.ConvertedDataSet.DataTable.Copy();

                //grid.DataSource = products.DataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            lbMenu.Items.Add("Ham Veri");
            lbMenu.SelectedIndex = 0;
            foreach (var transformModel in this.ConvertedDataSet.Transforms)
            {
                lbMenu.Items.Add(transformModel.Name);
            }
            lbMenu.Items.Add("Son Hali");

        }

        private void lbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMenu.SelectedIndex == 0)
            {
                // Ham Veri
                grid.DataSource = this.dataTable;
            }
            else if (lbMenu.SelectedIndex == lbMenu.Items.Count - 1)
            {
                // Sonuç
                grid.DataSource = this.ConvertedDataSet.ResultTable.Copy();
            }
            else
            {
                // Transformlar
                grid.DataSource = null;
            }
        }

        private void btnXmlKaydet_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ConvertedDataSet.SaveAsXml(saveFileDialog1.FileName);
            }
        }

        private void btnXmlAc_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var cds = new ConvertedDataSetModel();
                cds.SourceXmlPath = openFileDialog1.FileName;
                cds.Transforms = new List<TransformModel>();
                cds.Name = "";
                cds.LoadXml();

                grid.DataSource = cds.DataTable;
            }
        }

    }
}

