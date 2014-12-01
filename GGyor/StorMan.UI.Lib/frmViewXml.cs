using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using StorMan.Model;

namespace StomMan.UI.Lib
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
                var sw = new Stopwatch();
                sw.Start();
                //products = new ProductsXmlCollection(this.ConvertedDataSet.SourceXmlPath);
                this.ConvertedDataSet.LoadXml();
                sw.Stop();
                MessageBox.Show("LoadXml in " + sw.Elapsed.ToString());

                sw.Start();
                this.ConvertedDataSet.ApplyTransforms();
                sw.Stop();
                MessageBox.Show("ApplyTransforms in " + sw.Elapsed.ToString());

                this.dataTable = this.ConvertedDataSet.DataTable.Copy();
                if (this.dataTable.PrimaryKey.Length == 0)
                {
                    this.dataTable.PrimaryKey = new DataColumn[] {this.dataTable.Columns[0]};
                }

                //grid.DataSource = products.DataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            lbMenu.Items.Add("Ham Veri");
            lbMenu.SelectedIndex = 0;
            //foreach (var transformModel in this.ConvertedDataSet.Transforms)
            //{
            //    lbMenu.Items.Add(transformModel.Name);
            //}
            lbMenu.Items.Add("Karşılaştırma");
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
            else if (lbMenu.SelectedIndex == lbMenu.Items.Count - 2)
            {
                // Karşılaştırmalı Sonuç
                var dt = this.ConvertedDataSet.ResultTable.Copy();
                
                var priceColumn = dt.Columns["buyingPrice"];
                var oldPriceColumn = dt.Columns.Add("oldBuyingPrice");
                oldPriceColumn.SetOrdinal(priceColumn.Ordinal);

                var currColumn = dt.Columns["currencyAbbr"];
                var oldCurrColumn = dt.Columns.Add("oldCurrencyAbbr");
                oldCurrColumn.SetOrdinal(currColumn.Ordinal);

                foreach (DataRow row in dt.Rows)
                {
                    row["oldBuyingPrice"] = dt.Rows.Find(row[0])["buyingPrice"];
                    row["oldCurrencyAbbr"] = dt.Rows.Find(row[0])["currencyAbbr"];
                }

                grid.DataSource = dt;

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

