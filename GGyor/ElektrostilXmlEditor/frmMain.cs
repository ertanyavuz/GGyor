using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ElektrostilXmlEditor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            TransformList = new List<XmlTransform>();
            Filters = new List<XmlFilter>();
        }

        private ProductsXmlCollection products;
        private List<XmlTransform> TransformList;
        private List<XmlFilter> Filters;

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                products = new ProductsXmlCollection(textBox1.Text);

                grid.DataSource = products.DataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            var fileName = "elektrostil_" + DateTime.Now.ToShortDateString().Replace(".", "");
            saveFileDialog1.FileName = fileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                products.SaveAsXml(saveFileDialog1.FileName);

                if (MessageBox.Show("Kayıt tamamlandı. Kaydedilen dokümanı görüntülemek istiyor musunuz?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start("C:\\Windows\\notepad.exe", saveFileDialog1.FileName);
                }
            }
        }

        private void btnConvertCurrencies_Click(object sender, EventArgs e)
        {
            var dollarRate = double.Parse(txtDolarKuru.Text, System.Globalization.CultureInfo.InvariantCulture);
            var euroRate = double.Parse(textBox2.Text, System.Globalization.CultureInfo.InvariantCulture);

            foreach (DataRow dr in products.DataTable.Rows)
            {
                var currency = dr["currencyAbbr"].ToString();
                var price = double.Parse(dr["price5"].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                if (currency == "USD")
                {
                    dr["currencyAbbr"] = "TL";
                    dr["price5"] = price * dollarRate;
                }
                else if (currency == "EUR")
                {
                    dr["currencyAbbr"] = "TL";
                    dr["price5"] = price * euroRate;
                }
            }

            MessageBox.Show("Kurlar dönüştürüldü.");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (products.DataTable == null || products.DataTable.Columns.Count == 0)
                return;
            var f = new frmTransform { FieldList = products.DataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList() };
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.TransformList.Add(f.Transform);
                listBox1.Items.Add(f.Transform);
            }
        }
        private void btnCikar_Click(object sender, EventArgs e)
        {
            var item = listBox1.SelectedItem;
            var index = listBox1.SelectedIndex;
            if (item == null || index < 0)
                return;
            listBox1.Items.Remove(item);
            this.TransformList.RemoveAt(index);
        }

        private void btnApplyTransforms_Click(object sender, EventArgs e)
        {
            grid.RefreshDataSource();

            // Apply Filters
            var rowsToBeSaved = new List<DataRow>();
            var rowsToBeDeleted = new List<DataRow>();
            for (int i = 0; i < gView.DataRowCount; i++)
            {
                rowsToBeSaved.Add(gView.GetDataRow(i));
            }
            var dt = grid.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (!rowsToBeSaved.Contains(row))
                    rowsToBeDeleted.Add(row);
            }
            foreach (var dataRow in rowsToBeDeleted)
            {
                dt.Rows.Remove(dataRow);
            }
            dt.AcceptChanges();
            grid.RefreshDataSource();

            products.TransformList = this.TransformList.ToArray().ToList();

            //var filterList = XmlFilter.Parse(gView.ActiveFilterString);
            //products.FilterList = filterList;

            //products.ApplyFilters();
            products.ApplyTransforms();
        }
        
    }
}
