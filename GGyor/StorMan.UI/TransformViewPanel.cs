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
    public partial class TransformViewPanel : ViewPanelBase
    {
        public TransformViewPanel()
        {
            InitializeComponent();
        }

        private TransformModel _transform;
        public TransformModel Transform
        {
            get
            {
                if (_transform != null && !String.IsNullOrWhiteSpace(txtTransformName.Text))
                    _transform.Name = txtTransformName.Text;
                return _transform; 
            }
            set 
            { 
                _transform = value;
            }
        }

        public DataTable DataTable { get; set; }

        private void TransformViewPanel_Load(object sender, EventArgs e)
        {
            if (this.Transform != null)
            {
                foreach (var operationModel in this.Transform.Operations)
                {
                    lbOperations.Items.Add(operationModel.Name);
                }

                txtTransformName.Text = this.Transform.Name;
                txtFilter.Text = this.Transform.Filters.Count == 0 ? "" : this.Transform.Filters.Select(x => x.ToString()).Aggregate((a, b) => a + " AND " + b);

            }

            if (this.DataTable != null)
            {
                var fieldList = new List<string>();
                foreach (DataColumn dc in this.DataTable.Columns)
                {
                    fieldList.Add(dc.ColumnName);
                }
                operationControl.FieldList = fieldList;

                if (this.DataTable.PrimaryKey.Length == 0)
                {
                    this.DataTable.PrimaryKey = new DataColumn[] { this.DataTable.Columns[0] };
                }
                grid.OriginalDataTable = this.DataTable;
                reloadGrid();
            }
        }

        private void reloadGrid()
        {
            var service = new XmlDataTableService();
            var dt = this.DataTable.Copy();
            service.ApplyFilters(dt, this.Transform.Filters);
            service.ApplyOperations(dt, this.Transform.Operations);

            grid.ModifiedDataTable = dt;
            grid.Reload();
        }

        private void lbOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbOperations.SelectedIndex >= 0 && this.Transform.Operations.Count > lbOperations.SelectedIndex)
            {
                var operation = this.Transform.Operations[lbOperations.SelectedIndex];
                operationControl.Operation = operation;

            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            var operation = operationControl.Operation.Copy(doNotCopyId: true);
            if (operation != null)
            {
                this.Transform.Operations.Add(operation);
                lbOperations.Items.Add(operation.Name);
                reloadGrid();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var index = lbOperations.SelectedIndex;
            if (index < 0)
                return;
            var operation = operationControl.Operation.Copy();
            this.Transform.Operations[index] = operation;
            lbOperations.Items[index] = operation.Name;

            reloadGrid();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var index = lbOperations.SelectedIndex;
            if (index < 0)
                return;
            this.Transform.Operations.RemoveAt(index);
            lbOperations.Items.RemoveAt(index);
            if (lbOperations.Items.Count > 0)
                lbOperations.SelectedIndex = Math.Min(index, lbOperations.Items.Count - 1);

            reloadGrid();
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            var currentValue = grid.ViewType;
            if (rbComparison.Checked && currentValue != ComparerDataGrid.ViewTypeEnum.Both)
            {
                grid.ViewType = ComparerDataGrid.ViewTypeEnum.Both;
            }
            else if (rbOriginal.Checked && currentValue != ComparerDataGrid.ViewTypeEnum.Original)
            {
                grid.ViewType = ComparerDataGrid.ViewTypeEnum.Original;
            }
            else if (rbModified.Checked && currentValue != ComparerDataGrid.ViewTypeEnum.Modified)
            {
                grid.ViewType = ComparerDataGrid.ViewTypeEnum.Modified;
            }
            else
            {
                return;
            }

            reloadGrid();

        }

        private void chkHideUnmodifiedColumns_CheckedChanged(object sender, EventArgs e)
        {
            if (grid.ShowTransformedColumnsOnly != chkHideUnmodifiedColumns.Checked)
            {
                grid.ShowTransformedColumnsOnly = chkHideUnmodifiedColumns.Checked;
                reloadGrid();
            }
        }
        
    }
}
