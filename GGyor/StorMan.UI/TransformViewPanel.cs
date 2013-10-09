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

        public TransformModel Transform { get; set; }
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
                txtFilter.Text = this.Transform.Filters.Select(x => x.ToString()).Aggregate((a, b) => a + " AND " + b);

            }

            if (this.DataTable != null)
            {
                if (this.DataTable.PrimaryKey.Length == 0)
                {
                    this.DataTable.PrimaryKey = new DataColumn[] { this.DataTable.Columns[0] };
                }
                comparerDataGrid1.OriginalDataTable = this.DataTable;
                reloadGrid();
            }
        }

        private void reloadGrid()
        {
            var service = new XmlDataTableService();
            var dt = this.DataTable.Copy();
            service.ApplyFilters(dt, this.Transform.Filters);
            service.ApplyTransforms(dt, this.Transform.Operations);

            comparerDataGrid1.ModifiedDataTable = dt;
            comparerDataGrid1.Reload();
        }
        
    }
}
