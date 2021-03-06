﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using StorMan.Business;
using StorMan.Model;

namespace StomMan.UI.Lib
{
    public partial class frmTransform : Form
    {
        public frmTransform()
        {
            InitializeComponent();
            OperationList = new List<OperationModel>();
            Filters = new List<FilterModel>();
        }

        private ProductsXmlCollection products;
        private List<OperationModel> OperationList;
        private List<FilterModel> Filters;
        public string SourceXML { get; set; }

        public TransformModel Transform { get; set; }
        
        private void frmTransform_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.SourceXML))
                txtSourceXml.Text = this.SourceXML;

            if (!String.IsNullOrWhiteSpace(txtSourceXml.Text))
            {
                btnLoad_Click(null, null);
            }

            if (this.Transform == null)
            {
                this.Transform = new TransformModel();
            }
            else
            {
                var filterStr = "";
                foreach (var filterModel in this.Transform.Filters)
                {
                    var col = gView.Columns[filterModel.FieldName];
                    if (col == null) continue;

                    var filter = new ColumnFilterInfo(ColumnFilterType.Value, filterModel.Value.ToString(), "");
                    //filter.Type = ColumnFilterType.Value;
                    //filter.Value = String.Format("[{0}] = '{1}'", filterModel.FieldName, filterModel.Value);

                    gView.ActiveFilter.Add(col, filter);

                    //filterStr += String.Format(" AND [{0}] = '{1}'", filterModel.FieldName, filterModel.Value);
                }
                //if (filterStr.Length >= 5)
                //{
                //    gView.ActiveFilterString = filterStr.Substring(5);
                //}
                foreach (var operationModel in this.Transform.Operations)
                {
                    this.OperationList.Add(operationModel);
                    listBox1.Items.Add(operationModel);
                }
                txtTransformName.Text = this.Transform.Name;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                products = new ProductsXmlCollection(txtSourceXml.Text);

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
            var f = new frmOperation { FieldList = products.DataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList() };
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.OperationList.Add(f.Operation);
                listBox1.Items.Add(f.Operation);
            }
        }
        private void btnCikar_Click(object sender, EventArgs e)
        {
            var item = listBox1.SelectedItem;
            var index = listBox1.SelectedIndex;
            if (item == null || index < 0)
                return;
            listBox1.Items.Remove(item);
            this.OperationList.RemoveAt(index);
        }
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (products.DataTable == null || products.DataTable.Columns.Count == 0)
                return;
            var item = listBox1.SelectedItem;
            var index = listBox1.SelectedIndex;
            if (item == null || index < 0)
                return;

            var f = new frmOperation
                {
                    FieldList = products.DataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList(),
                    Operation = this.OperationList[index].Copy()
                };
            if (f.ShowDialog() == DialogResult.OK)
            {
                //this.OperationList.Add(f.Operation);
                this.OperationList[index] = f.Operation;
                listBox1.Items[index] = f.Operation;
            }
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
            gView.ActiveFilterString = "";
            grid.RefreshDataSource();

            products.OperationList = this.OperationList.ToArray().ToList();
            
            //var filterList = XmlFilter.Parse(gView.ActiveFilterString);
            //products.FilterList = filterList;

            //products.ApplyFilters();
            products.ApplyTransforms();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtTransformName.Text))
            {
                MessageBox.Show("Transform ismi giriniz", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Transform.Operations = this.OperationList.ToArray().ToList();

            this.Transform.Filters.Clear();
            foreach (ViewColumnFilterInfo filterInfo in gView.ActiveFilter)
            {
                filterInfo.GetType();
                var filter = new FilterModel
                    {
                        FieldName = filterInfo.Column.FieldName,
                        FilterType = FilterTypeEnum.Equals,
                        Value = filterInfo.Filter.Value
                    };
                this.Transform.Filters.Add(filter);
            }
            //this.Filters = FilterModel.Parse(gView.ActiveFilter);

            this.Transform.Name = txtTransformName.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }


        
    }
}
