using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ElektrostilXmlEditor
{
    public class ProductsXmlCollection
    {
        private DataTable _dataTable;
        public DataTable DataTable
        {
            get { return _dataTable; }
        }

        public List<XmlFilter> FilterList { get; set; }
        public List<XmlTransform> TransformList { get; set; }

        public ProductsXmlCollection(string xmlPath)
        {
            TransformList = new List<XmlTransform>();
            FilterList = new List<XmlFilter>();

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
            foreach (var transform in TransformList)
            {
                foreach (DataRow row in DataTable.Rows)
                {
                    transform.ApplyToDataRow(row);
                }
            }
        }

    }


    public class XmlFilter
    {
        public string FieldName { get; set; }
        public XmlFilterType FilterType { get; set; }
        //public int IntValue { get; set; }
        //public string StrValue { get; set; }
        //public double DecValue { get; set; }
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

        public static List<XmlFilter> Parse(string filterString)
        {
            var s = filterString.Split("And".ToArray());
            return s.Select(x => new XmlFilter
                {
                    
                }).ToList();
        }
    }

    public enum XmlFilterType
    {
        Equals
    }

    public class XmlTransform
    {
        //public XmlTransformType Type { get; set; }
        public XmlTransformOperation Operation { get; set; }

        public string FieldName { get; set; }
        public Type DataType { get; set; }
        public object Value { get; set; }

        // FieldName, Operation, Value
        public void ApplyToDataRow(DataRow row)
        {
            if (row.Table.Columns.Contains(this.FieldName))
            {
                var rowValue = row[this.FieldName];
                if (rowValue == null)
                    return;
                if (this.Operation == XmlTransformOperation.Toplama)
                {
                    var floatValue = stringToFloat(rowValue.ToString());
                    floatValue += stringToFloat(Value.ToString());
                    row[this.FieldName] = floatToStr(floatValue);
                }
                else if (this.Operation == XmlTransformOperation.Carpma)
                {
                    var floatValue = stringToFloat(rowValue.ToString());
                    floatValue *= stringToFloat(Value.ToString());
                    row[this.FieldName] = floatToStr(floatValue);
                }
                else if (this.Operation == XmlTransformOperation.Eşitleme)
                {
                    row[this.FieldName] = this.Value.ToString();
                }
                else if (this.Operation == XmlTransformOperation.Ekleme)
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
            return String.Format("{0} - {1} - {2}", this.FieldName, this.Operation, this.Value);
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
    //public enum XmlTransformType
    //{
    //    FiyatAyarlama,  // Fiyat arttır-azalt. yüzde ile veya ekleyerek
    //    KurDonusturme,  // Kurları TL'ye çevirme
    //    AciklamaEkle,   // Açıklama alanlarının başına ve sonuna yazı ekleme
    //    AlanKaldirma,   // XML'deki bir alanı tümüyle kaldırma
    //    StokAyarlama,   // Stok sayısını arttırıp azaltma, yüzde ile veya adet ile.
    //    KargoAyarlama   // Alıcı-satıcı öder durumu.
    //}

    public enum XmlTransformOperation
    {
        Carpma,
        Toplama,
        Eşitleme,
        Ekleme
    }

}


