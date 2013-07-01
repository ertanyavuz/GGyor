using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace StorMan.Model
{
    public class ConvertedDataSetModel
    {
        public ConvertedDataSetModel()
        {
            this.Transforms = new List<TransformModel>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string SourceXmlPath { get; set; }
        public string XmlString { get; set; }

        public List<TransformModel> Transforms { get; set; }
        public DataTable DataTable { get; set; }
        public DataTable ResultTable { get; set; }

        public void LoadXml()
        {
            this.DataTable = new DataTable("ProductsXml");

            var xdoc = XDocument.Load(this.SourceXmlPath);
            //var originalFile = String.Format("es_urun_{0}.xml", DateTime.s);
            //xdoc.Save("");

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
        public void ApplyTransforms()
        {
            this.ResultTable = this.DataTable.Copy();
            this.ResultTable.Rows.Clear();

            foreach (var transform in this.Transforms)
            {
                // Get a copy of the data
                var dt = this.DataTable.Copy();

                // Apply Filters
                foreach (var filterModel in transform.Filters)
                {
                    var rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!filterModel.Check(row))
                            rowsToDelete.Add(row);
                    }
                    foreach (var dataRow in rowsToDelete)
                    {
                        dt.Rows.Remove(dataRow);
                    }
                }

                // Apply transforms
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var operationModel in transform.Operations)
                    {
                        operationModel.ApplyToDataRow(row);
                    }
                }

                // Append data to ResultTable
                while (dt.Rows.Count > 0)
                {
                    var dr = dt.Rows[0];
                    this.ResultTable.ImportRow(dr);
                    dt.Rows.RemoveAt(0);
                }
            }
        }
        public void SaveAsXml(string path)
        {
            var xdoc = new XDocument();
            xdoc.Add(new XElement("root"));
            //xdoc.Root = new XElement("root");
            foreach (DataRow row in this.ResultTable.Rows)
            {
                var xItem = new XElement("item");
                foreach (DataColumn column in ResultTable.Columns)
                {
                    var xElement = new XElement(column.ColumnName);
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

    public class TransformModel
    {
        public TransformModel()
        {
            this.Operations = new List<OperationModel>();
            this.Filters = new List<FilterModel>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public List<OperationModel> Operations { get; set; }
        public List<FilterModel> Filters { get; set; }
    }

    public class FilterModel
    {
        public FilterModel()
        {
            //this.Value = "";
        }
        public int ID { get; set; }
        public string FieldName { get; set; }
        public FilterTypeEnum FilterType { get; set; }
        public object Value { get; set; }

        public bool Check(DataRow row)
        {
            if (row.Table.Columns.Contains(this.FieldName))
            {
                var rowValue = row[this.FieldName];
                if (rowValue == null || rowValue == System.DBNull.Value)
                    return false;

                return rowValue.ToString() == this.Value.ToString();
            }
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static List<FilterModel> Parse(string filterString)
        {
            throw new NotImplementedException();

            var s = filterString.Split("And".ToArray());
            return s.Select(x => new FilterModel
            {

            }).ToList();
        }
    }

    public enum FilterTypeEnum
    {
        Equals
    }

    public class OperationModel
    {
        public OperationModel()
        {
            this.Value = "";
        }
        public int ID { get; set; }
        public OperationTypeEnum OperationType { get; set; }

        public string FieldName { get; set; }
        public Type DataType { get; set; }
        public object Value { get; set; }

        public void ApplyToDataRow(DataRow row)
        {
            if (row.Table.Columns.Contains(this.FieldName))
            {
                var rowValue = row[this.FieldName];
                if (rowValue == null)
                    return;
                if (this.OperationType == OperationTypeEnum.Toplama)
                {
                    var floatValue = stringToFloat(rowValue.ToString());
                    floatValue += stringToFloat(Value.ToString());
                    row[this.FieldName] = floatToStr(floatValue);
                }
                else if (this.OperationType == OperationTypeEnum.Carpma)
                {
                    var floatValue = stringToFloat(rowValue.ToString());
                    floatValue *= stringToFloat(Value.ToString());
                    row[this.FieldName] = floatToStr(floatValue);
                }
                else if (this.OperationType == OperationTypeEnum.Eşitleme)
                {
                    row[this.FieldName] = this.Value.ToString();
                }
                else if (this.OperationType == OperationTypeEnum.Ekleme)
                {
                    if (rowValue == System.DBNull.Value)
                        rowValue = "";
                    var stringValue = rowValue.ToString();
                    stringValue += this.Value.ToString().Replace("\\n", "\n").Replace("\\t", "\t");
                    row[this.FieldName] = stringValue;
                }
            }
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} - {2}", this.FieldName, this.OperationType, this.Value);
        }

        public static float stringToFloat(string floatStr)
        {
            floatStr = floatStr.Replace(",", "");
            var floatValue = float.Parse(floatStr, CultureInfo.InvariantCulture);
            return floatValue;
        }
        public static string floatToStr(float floatValue)
        {
            var floatStr = floatValue.ToString();
            floatStr = floatStr.Replace(",", ".");
            return floatStr;
        }

    }


    //// Alan (operand) Değer
    //// (Alan, Değer) => (...)
    //// Action<Alan, Değer>
    //public enum XmlOperationType
    //{
    //    FiyatAyarlama,  // Fiyat arttır-azalt. yüzde ile veya ekleyerek
    //    KurDonusturme,  // Kurları TL'ye çevirme
    //    AciklamaEkle,   // Açıklama alanlarının başına ve sonuna yazı ekleme
    //    AlanKaldirma,   // XML'deki bir alanı tümüyle kaldırma
    //    StokAyarlama,   // Stok sayısını arttırıp azaltma, yüzde ile veya adet ile.
    //    KargoAyarlama   // Alıcı-satıcı öder durumu.
    //}

    public enum OperationTypeEnum
    {
        Carpma,
        Toplama,
        Eşitleme,
        Ekleme
    }
}
