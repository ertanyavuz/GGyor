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
        }

        private ProductsXmlCollection products;

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


    }
}
