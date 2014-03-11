using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StorMan.Model;

namespace StorMan.Business
{
    public class XmlDataTableService
    {

        public DataTable XmlToDataTable(string xmlPath)
        {
            var dt = new DataTable("ProductsXml");

            var xdoc = XDocument.Load(xmlPath);

            var q = from d in xdoc.Root.Descendants("item")
                    select d;
            var list = q.ToList();

            foreach (var xElement in list)
            {
                var valueList = new List<object>();
                foreach (var attr in xElement.Elements())
                {
                    if (dt.Columns[attr.Name.LocalName] == null)
                        dt.Columns.Add(attr.Name.LocalName);
                }
                var dr = dt.NewRow();
                foreach (var attr in xElement.Elements())
                {
                    dr[attr.Name.LocalName] = attr.Value;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        public void SaveAsXml(DataTable dt, string path)
        {
            var xdoc = new XDocument();
            xdoc.Add(new XElement("root"));
            //xdoc.Root = new XElement("root");
            foreach (DataRow row in dt.Rows)
            {
                var xItem = new XElement("item");
                foreach (DataColumn column in dt.Columns)
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

        public DataTable ApplyFilters(DataTable dt, List<FilterModel> filterList)
        {
            foreach (var filter in filterList)
            {
                var rowsToDelete = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    if (!filter.Check(row))
                        rowsToDelete.Add(row);
                }
                foreach (var dataRow in rowsToDelete)
                {
                    dt.Rows.Remove(dataRow);
                }
            }

            return dt;
        }

        public DataTable ApplyOperations(DataTable dt, List<OperationModel> operationList)
        {
            foreach (var transform in operationList)
            {
                foreach (DataRow row in dt.Rows)
                {
                    transform.ApplyToDataRow(row);
                }
            }
            return dt;
        }

        public DataTable ApplyTransform(DataTable dt, TransformModel transform)
        {
            ApplyFilters(dt, transform.Filters);
            ApplyOperations(dt, transform.Operations);

            return dt;
        }

        public DataTable ApplyConvertedDataSet(ConvertedDataSetModel cds)
        {
            var dt = XmlToDataTable(cds.SourceXmlPath);
            return ApplyConvertedDataSet(cds, dt);
        }
        public DataTable ApplyConvertedDataSet(ConvertedDataSetModel cds, DataTable sourceTable)
        {
            DataTable resultTable = null;
            foreach (var transformModel in cds.Transforms)
            {
                var subTable = sourceTable.Copy();
                ApplyTransform(subTable, transformModel);
                if (resultTable == null)
                    resultTable = subTable;
                else
                {
                    var rowList = new List<DataRow>();
                    foreach (DataRow row in subTable.Rows)
                    {
                        rowList.Add(row);
                        row.Delete();
                    }
                    subTable.AcceptChanges();
                    foreach (var dataRow in rowList)
                    {
                        subTable.Rows.Remove(dataRow);
                        resultTable.Rows.Add(dataRow);
                    }
                }
            }
            return resultTable;
        }

        public DataTable GetUnselectedRows(ConvertedDataSetModel cds, DataTable sourceTable)
        {
            var dt = sourceTable.Copy();
            foreach (var transformModel in cds.Transforms)
            {
                foreach (var filterModel in transformModel.Filters)
                {
                    // Remove rows that checks as true
                    var rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (filterModel.Check(row))
                            rowsToDelete.Add(row);
                    }
                    foreach (var dataRow in rowsToDelete)
                    {
                        dt.Rows.Remove(dataRow);
                    }
                }
            }
            return dt;
        }
        public Dictionary<string, List<TransformModel>> GetMultipleTransformedRows(ConvertedDataSetModel cds, DataTable sourceTable)
        {
            var resultTable = new Dictionary<string, List<TransformModel>>();
            var dt = sourceTable.Copy();
            foreach (DataRow dataRow in dt.Rows)
            {
                resultTable.Add(dataRow[0].ToString(), new List<TransformModel>());
            }

            foreach (var transformModel in cds.Transforms)
            {
                foreach (var filterModel in transformModel.Filters)
                {
                    // Add this transform to the bucket if it checks as true
                    foreach (DataRow row in dt.Rows)
                    {
                        if (filterModel.Check(row))
                        {
                            if (resultTable[row[0].ToString()].All(x => x.ID != transformModel.ID))
                                resultTable[row[0].ToString()].Add(transformModel);
                        }
                    }
                }
            }
            return resultTable;
        }
    }
}
