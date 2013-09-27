using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorMan.UI
{
    public partial class FilterViewPanel : ViewPanelBase
    {
        public FilterViewPanel()
        {
            InitializeComponent();
        }

        public List<Model.FilterModel> FilterList { get; set; }
        public DataTable DataTable { get; set; }

        private void FilterViewPanel_Load(object sender, EventArgs e)
        {
            if (this.DataTable != null)
            {
                if (this.DataTable.PrimaryKey.Length == 0)
                {
                    this.DataTable.PrimaryKey = new DataColumn[] {this.DataTable.Columns[0]};
                }

                this.comparerGrid.OriginalDataTable = this.DataTable;
                this.comparerGrid.ModifiedDataTable = this.DataTable.Copy();

                this.comparerGrid.Reload();

            }
        }


    }
}
