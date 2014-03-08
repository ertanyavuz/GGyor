using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StorMan.Business;
using StorMan.Model;

namespace StorMan.UI
{
    public partial class FilterViewPanel : ViewPanelBase
    {
        public FilterViewPanel()
        {
            InitializeComponent();
        }

        public List<FilterModel> FilterList { get; set; }
        public DataTable DataTable { get; set; }

        private void FilterViewPanel_Load(object sender, EventArgs e)
        {
            if (this.DataTable != null)
            {
                if (this.DataTable.PrimaryKey.Length == 0)
                {
                    this.DataTable.PrimaryKey = new DataColumn[] {this.DataTable.Columns[0]};
                }

                // Set original and modified tables for ComparerDataGrid
                this.comparerGrid.OriginalDataTable = this.DataTable;
                //this.comparerGrid.ModifiedDataTable = this.DataTable.Copy();

                var fieldList = new List<string>();
                foreach (DataColumn dc in this.DataTable.Columns)
                {
                    fieldList.Add(dc.ColumnName);
                }
                filterControl.FieldList = fieldList;

                UpdateFilterList();
                UpdateGrid();
                
            }
        }

        public void UpdateGrid()
        {
            var service = new XmlDataTableService();

            var dt = this.DataTable.Copy();
            /*dt = */service.ApplyFilters(dt, this.FilterList);
            //this.comparerGrid.ModifiedDataTable = dt;
            comparerGrid.OriginalDataTable = this.DataTable;
            comparerGrid.ModifiedDataTable = dt;
            comparerGrid.Reload();
        }
        public void UpdateFilterList()
        {
            lbFilters.Items.Clear();
            lbFilters.Items.AddRange(this.FilterList.Select(x => x.ToString()).ToArray<object>());
            if (lbFilters.Items.Count > 0)
                lbFilters.SelectedIndex = lbFilters.Items.Count - 1;
        }

        private FilterModel SelectedFilter
        {
            get
            {
                if (lbFilters.SelectedIndex < 0)
                    return null;
                return FilterList[lbFilters.SelectedIndex];
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            var filter = filterControl.Filter;
            if (filter != null)
            {
                this.FilterList.Add(filter);
                lbFilters.Items.Add(filter.ToString());
                UpdateGrid();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var index = lbFilters.SelectedIndex;
            if (index < 0)
                return;
            var filter = filterControl.Filter;
            this.FilterList[index] = filter;
            lbFilters.Items[index] = filter.ToString();

            UpdateGrid();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var index = lbFilters.SelectedIndex;
            if (index < 0)
                return;
            this.FilterList.RemoveAt(index);
            lbFilters.Items.RemoveAt(index);
            if (lbFilters.Items.Count > 0)
                lbFilters.SelectedIndex = Math.Min(index, lbFilters.Items.Count - 1);

            UpdateGrid();
        }

        private void lbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filter = this.SelectedFilter;
            if (filter != null)
            {
                filterControl.Filter = filter;
            }
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            var currentValue = comparerGrid.ViewType;
            if (rbOriginal.Checked && currentValue != ComparerDataGrid.ViewTypeEnum.Original)
            {
                comparerGrid.ViewType = ComparerDataGrid.ViewTypeEnum.Original;
            }
            else if (rbModified.Checked && currentValue != ComparerDataGrid.ViewTypeEnum.Modified)
            {
                comparerGrid.ViewType = ComparerDataGrid.ViewTypeEnum.Modified;
            }
            else
            {
                return;
            }

            UpdateGrid();
        }



    }
}
