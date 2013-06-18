using System;
using System.Collections.Generic;
using System.Data;
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

        public ProductsXmlCollection(string xmlPath)
        {
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

    }


    public class XmlFilter
    {
        public string FieldName { get; set; }
        public XmlFilterType FilterType { get; set; }
        public int IntValue { get; set; }
        public string StrValue { get; set; }
        public double DecValue { get; set; }

    }

    public class XmlTransform
    {
        public XmlTransformType Type { get; set; }
        public XmlTransformOperation Operation { get; set; }

        public int IntValue { get; set; }
        public string StrValue { get; set; }
        public double DecValue { get; set; }

    }
    
    
    public enum XmlTransformType
    {
        FiyatAyarlama,  // Fiyat arttır-azalt. yüzde ile veya ekleyerek
        KurDonusturme,  // Kurları TL'ye çevirme
        AciklamaEkle,   // Açıklama alanlarının başına ve sonuna yazı ekleme
        AlanKaldirma,   // XML'deki bir alanı tümüyle kaldırma
        StokAyarlama,   // Stok sayısını arttırıp azaltma, yüzde ile veya adet ile.
        KargoAyarlama   // Alıcı-satıcı öder durumu.
    }
    public enum XmlTransformOperation
    {
        Carpma,
        Toplama
    }

    public enum XmlFilterType
    {
        
    }
}


