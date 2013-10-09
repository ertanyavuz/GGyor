﻿using System;
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
            this.ModifiedColumnSuffix = "_1";
            InitializeComponent();
        }

        public DataTable OriginalDataTable { get; set; }
        public DataTable ModifiedDataTable { get; set; }
        public List<string> ColumnsToCompare { get; set; }
        
        public ViewTypeEnum ViewType { get; set; }

        public string ModifiedColumnSuffix { get; set; }

        //public ProductsXmlCollection ProductsXmlCollection { get; set; }

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
                return;
            }

            try
            {
                var dt = new DataTable("Comparer");
                foreach (DataColumn col in this.OriginalDataTable.Columns)
                {
                    dt.Columns.Add(col.ColumnName, col.DataType);
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
                grid.AlternatingRowsDefaultCellStyle.BackColor = Color.Cornsilk;

                // Modified cell styles...
                if (this.ModifiedDataTable != null)
                {
                    // Boldness for Modified columns...
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        if (col.Name.EndsWith(this.ModifiedColumnSuffix))
                            col.DefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
                    }
                    //// Backcolor for differing cells
                    //foreach (DataGridViewRow row in grid.Rows)
                    //{
                    //    foreach (DataGridViewColumn col in grid.Columns)
                    //    {
                    //        if (col.Name.EndsWith(this.ModifiedColumnSuffix))
                    //        {
                    //            var orgCol = col.Name.Substring(0, col.Name.Length - this.ModifiedColumnSuffix.Length);
                    //            var modCol = col.Name;
                    //            if (row.Cells[orgCol].Value != row.Cells[modCol].Value)
                    //            {
                    //                row.Cells[modCol].Style.ForeColor = Color.Red;
                    //                row.Cells[modCol].Style.BackColor = Color.Chartreuse;

                    //            }
                    //        }

                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.ToString());
            }

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
                    //row.Cells[modCol].Style.ForeColor = Color.Red;
                    //row.Cells[modCol].Style.BackColor = Color.Chartreuse;

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
