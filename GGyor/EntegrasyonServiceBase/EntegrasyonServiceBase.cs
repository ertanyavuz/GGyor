using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace EntegrasyonServiceBase
{
    public class EntegrasyonServiceBase
    {
        public Dictionary<string, decimal> GetDovizKurlari()
        {
            var xmlPath = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            var USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml.Replace(".", ",");
            var EUR = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml.Replace(".", ",");

            var table = new Dictionary<string, decimal>();
            table.Add("USD", decimal.Parse(USD));
            table.Add("EUR", decimal.Parse(EUR));

            return table;

        }

        public delegate void ProductUpdated(ProductUpdateType updateType, ProductModel newProduct, Dictionary<string, object> oldProduct);

        public ProductUpdated ProductUpdatedEvent;


    }

    public enum ProductUpdateType
    {
        None,
        Create,
        Update,
        Remove,
    }
}
