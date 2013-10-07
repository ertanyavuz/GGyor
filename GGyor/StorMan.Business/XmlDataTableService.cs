using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StorMan.Business
{
    public class XmlDataTableService
    {

        public string LoadXml(string xmlUrl)
        {
            
        }

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

    }
}
