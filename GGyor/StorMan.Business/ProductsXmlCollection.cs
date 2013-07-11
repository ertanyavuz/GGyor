using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using StorMan.Model;

namespace StorMan.Business
{
    public class ProductsXmlCollection
    {
        private DataTable _dataTable;
        public DataTable DataTable
        {
            get { return _dataTable; }
        }

        public List<FilterModel> FilterList { get; set; }
        public List<OperationModel> OperationList { get; set; }

        public ProductsXmlCollection(string xmlPath)
        {
            OperationList = new List<OperationModel>();
            FilterList = new List<FilterModel>();

            _dataTable = new DataTable("ProductsXml");

            var xdoc = XDocument.Load(xmlPath);

            var q = from d in xdoc.Root.Descendants("item")
                    select d;
            var list = q.ToList();

            foreach (var xElement in list)
            {
                var valueList = new List<object>();
                foreach (var attr in xElement.Elements())
                {
                    if (this.DataTable.Columns[attr.Name.LocalName] == null)
                        this.DataTable.Columns.Add(attr.Name.LocalName);
                }
                var dr = this.DataTable.NewRow();
                foreach (var attr in xElement.Elements())
                {
                    dr[attr.Name.LocalName] = attr.Value;
                }

                this.DataTable.Rows.Add(dr);
            }


        }

        public void SaveAsXml(string path)
        {
            var xdoc = new XDocument();
            xdoc.Add(new XElement("root"));
            //xdoc.Root = new XElement("root");
            foreach (DataRow row in this.DataTable.Rows)
            {
                var xItem = new XElement("item");
                foreach (DataColumn column in DataTable.Columns)
                {
                    var xElement = new XElement(column.ColumnName);
                    //xElement.Value = row[column].ToString();
                    if (column.ColumnName == "details")
                        xElement.ReplaceNodes(new XCData(row[column].ToString()));
                    else
                        xElement.Value = row[column].ToString();
                    xItem.Add(xElement);
                }
                xdoc.Root.Add(xItem);
            }

            xdoc.Save(path);
        }

        public void ApplyFilters()
        {
            foreach (var filter in FilterList)
            {
                var rowsToDelete = new List<DataRow>();
                foreach (DataRow row in DataTable.Rows)
                {
                    if (!filter.Check(row))
                        rowsToDelete.Add(row); 
                }
                foreach (var dataRow in rowsToDelete)
                {
                    DataTable.Rows.Remove(dataRow);
                }
            }
        }
        public void ApplyTransforms()
        {
            foreach (var transform in OperationList)
            {
                foreach (DataRow row in DataTable.Rows)
                {
                    transform.ApplyToDataRow(row);
                }
            }
        }

    }

    //public class ProductsDataTable
    //{
    //    public ProductsDataTable()
    //    {
    //        this.TransformList = new List<TransformModel>();
    //    }

    //    public DataTable DataTable { get; set; }
    //    public DataTable ResultTable { get; set; }

    //    //public List<FilterModel> FilterList { get; set; }
    //    //public List<OperationModel> OperationList { get; set; }
    //    public List<TransformModel> TransformList { get; set; }

    //    public void ApplyTransforms()
    //    {
    //        this.ResultTable = this.DataTable.Copy();
    //        this.ResultTable.Rows.Clear();

    //        foreach (var transform in this.TransformList)
    //        {
    //            // Get a copy of the data
    //            var dt = this.DataTable.Copy();

    //            // Apply Filters
    //            foreach (var filterModel in transform.Filters)
    //            {
    //                var rowsToDelete = new List<DataRow>();
    //                foreach (DataRow row in dt.Rows)
    //                {
    //                    if (!filterModel.Check(row))
    //                        rowsToDelete.Add(row);
    //                }
    //                foreach (var dataRow in rowsToDelete)
    //                {
    //                    dt.Rows.Remove(dataRow);
    //                }
    //            }

    //            // Apply transforms
    //            foreach (DataRow row in dt.Rows)
    //            {
    //                foreach (var operationModel in transform.Operations)
    //                {
    //                    operationModel.ApplyToDataRow(row);
    //                }
    //            }

    //            // Append data to ResultTable
    //            while (dt.Rows.Count > 0)
    //            {
    //                var dr = dt.Rows[0];
    //                dt.Rows.RemoveAt(0);
    //                this.ResultTable.Rows.Add(dr);
    //            }
    //        }
    //    }

    //}


}


