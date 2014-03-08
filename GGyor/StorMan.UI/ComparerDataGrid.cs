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
    /// <summary>
    /// Displays two given DataTables in DataGrid, showing its columns.
    /// </summary>
    public partial class ComparerDataGrid : UserControl
    {
        public ComparerDataGrid()
        {
            this.DoubleBuffered = true;
            this.ModifiedColumnSuffix = "_2";
            InitializeComponent();
            if (String.IsNullOrEmpty(LabelColumn))
                LabelColumn = "label";
        }

        public DataTable OriginalDataTable { get; set; }
        private DataTable _modifiedDataTable;
        public DataTable ModifiedDataTable
        {
            get { return _modifiedDataTable; }
            set
            { 
                _modifiedDataTable = value;
                modifiedRowsDictionary = null;
            }
        }

        public List<string> ColumnsToCompare { get; set; }
        public static string LabelColumn { get; set; }

        private ViewTypeEnum _viewType = ViewTypeEnum.Both;
        public ViewTypeEnum ViewType
        {
            get { return _viewType; }
            set { _viewType = value; }
        }

        public string ModifiedColumnSuffix { get; set; }
        public bool ShowTransformedColumnsOnly { get; set; }

        private void ComparerDataGrid_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            Reload();
        }

        /// <summary>
        /// Combine Original and Modified dataTables in a new dataTable, add style to display differences.
        /// </summary>
        public void Reload()
        {
            this.grid.DataSource = null;

            if (this.OriginalDataTable == null)
                return;

            if (this.OriginalDataTable != null && this.ModifiedDataTable == null)
            {
                grid.DataSource = this.OriginalDataTable.Copy();
            }
            else if (this.ViewType == ViewTypeEnum.Original)
            {
                grid.DataSource = this.OriginalDataTable;
            }
            else if (this.ModifiedDataTable != null && this.ViewType == ViewTypeEnum.Modified)
            {
                grid.DataSource = this.ModifiedDataTable;
            }
            else
            {
                // ViewType = Both
                try
                {
                    var dt = new DataTable("Comparer");
                    foreach (DataColumn col in this.OriginalDataTable.Columns)
                    {
                        dt.Columns.Add(col.ColumnName, col.DataType);
                        if (col.ColumnName == this.OriginalDataTable.PrimaryKey[0].ColumnName)
                            dt.PrimaryKey = new[] { dt.Columns[dt.Columns.Count - 1] };
                        if (this.ModifiedDataTable != null && this.ModifiedDataTable.Columns.Contains(col.ColumnName))
                            dt.Columns.Add(col.ColumnName + this.ModifiedColumnSuffix, this.ModifiedDataTable.Columns[col.ColumnName].DataType);
                    }

                    foreach (DataRow dr in this.OriginalDataTable.Rows)
                    {
                        var modifiedRow = getModifiedRow(dr);
                        if (this.ModifiedDataTable != null && modifiedRow == null)
                            continue;   // This row is filtered out.
                        var newRow = dt.NewRow();
                        foreach (DataColumn col in this.OriginalDataTable.Columns)
                        {
                            newRow[col.ColumnName] = dr[col.ColumnName];
                            if (this.ModifiedDataTable != null && this.ModifiedDataTable.Columns.Contains(col.ColumnName))
                            {
                                if (modifiedRow != null)
                                    newRow[col.ColumnName + this.ModifiedColumnSuffix] = modifiedRow[col.ColumnName];
                            }
                        }
                        dt.Rows.Add(newRow);
                    }

                    grid.DataSource = dt;

                    // Modified cell styles...
                    if (this.ModifiedDataTable != null)
                    {
                        // Boldness for Modified columns...
                        foreach (DataGridViewColumn col in grid.Columns)
                        {
                            if (col.Name.EndsWith(this.ModifiedColumnSuffix))
                                col.DefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
                        }

                        if (this.ShowTransformedColumnsOnly && this.ViewType == ViewTypeEnum.Both)
                        {
                            // Hide unmodified columns
                            //

                            // First, hide all columns except the key.
                            var columnList = new List<string>();
                            foreach (DataGridViewColumn col in grid.Columns)
                            {
                                // Primary key is always shown
                                if (dt.PrimaryKey[0].ColumnName == col.Name)
                                    continue;

                                col.Visible = false;

                                // Primary key of the modified table is always hidden.
                                if (dt.PrimaryKey[0].ColumnName + this.ModifiedColumnSuffix == col.Name)
                                    continue;

                                // Collect original table columns except the primary key, for the next step
                                if (!col.Name.EndsWith(this.ModifiedColumnSuffix))
                                    columnList.Add(col.Name);
                            }

                            // Next, loop through all rows and columns and show any column that has modified values, until all rows or hidden columns processed.
                            foreach (DataGridViewRow row in grid.Rows)
                            {
                                foreach (var colName in columnList)
                                {
                                    var orgCol = colName;
                                    var modCol = colName + this.ModifiedColumnSuffix;
                                    if (row.Cells[orgCol].Value != row.Cells[modCol].Value)
                                    {
                                        // ReSharper disable PossibleNullReferenceException
                                        grid.Columns[orgCol].Visible = true;
                                        grid.Columns[modCol].Visible = true;
                                        // ReSharper restore PossibleNullReferenceException

                                        columnList.Remove(colName);
                                        break;
                                    }
                                }
                            }

                            // Last, always show the label column. We do not show it in the beginning because label_modified might be shown or not, depending on the data.
                            if (grid.Columns.Contains(LabelColumn))
                                grid.Columns[LabelColumn].Visible = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.ToString());
                }
            }

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.Cornsilk;
        }

        private Dictionary<string, DataRow> modifiedRowsDictionary;
        private DataRow getModifiedRow(DataRow row)
        {
            if (this.OriginalDataTable.PrimaryKey == null || this.OriginalDataTable.PrimaryKey.Length == 0)
                throw new Exception("Table primary key is not set!");
            var keyName = this.ModifiedDataTable.PrimaryKey[0].ColumnName;
            if (modifiedRowsDictionary == null)
            {
                modifiedRowsDictionary = new Dictionary<string, DataRow>();
                foreach (DataRow dr in this.ModifiedDataTable.Rows)
                {
                    modifiedRowsDictionary.Add(dr[keyName].ToString(), dr);
                }
            }

            var keyValue = row[keyName].ToString();
            return modifiedRowsDictionary.ContainsKey(keyValue) ? modifiedRowsDictionary[keyValue] : null;
        }

        public enum ViewTypeEnum
        {
            Original,
            Modified,
            Both
        }

        private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var col = grid.Columns[e.ColumnIndex];
            if (col.Name.EndsWith(this.ModifiedColumnSuffix))
            {
                var row = grid.Rows[e.RowIndex];
                var orgCol = col.Name.Substring(0, col.Name.Length - this.ModifiedColumnSuffix.Length);
                //var modCol = col.Name;
                if (row.Cells[orgCol].Value != e.Value)
                {
                    e.CellStyle.ForeColor = Color.Red;

                }
            }
        }
    }


    public class DoubleBufferedDataGrid : DataGridView
    {
        public DoubleBufferedDataGrid()
        {
            DoubleBuffered = true;
        }
    }
    
}
