using System;
using System.Collections.Generic;
using System.Data;
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

        public List<ProductModel> GetSourceProductsXml(string xmlPath, string priceColumn)
        {

            //var xmlPath = @"C:\Users\Ertan\Downloads\N11-XML.xml";
            //var xmlPath = @"http://www.elektrostil.com/index.php?do=catalog/output&pCode=9211982202";
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

            var kurTable = GetDovizKurlari();
            var usdKur = kurTable["USD"];
            var eurKur = kurTable["EUR"];
            var karAmount = 10;

            if (usdKur < 1 || eurKur < 1)
            {
                throw new Exception("Kurlarda bir hata var.");
            }

            var productList = new List<ProductModel>();
            Func<string, string, string, decimal> calculatePrice = (x, curr, kdv) =>
            {
                var price = decimal.Parse(x.Replace(".", ","));
                if (curr == "USD")
                    price = price * usdKur;
                else if (curr == "EUR")
                    price = price * eurKur;
                else if (curr != "TL")
                    throw new NotImplementedException();

                price = price * (int.Parse(kdv) + 100) / 100;
                price = Math.Round(price * 100) / 100;
                price += karAmount;
                return price;
            };

            foreach (DataRow dr in dt.Rows)
            {
                var prod = new ProductModel
                {
                    //id = dr["id"],
                    title = (string)dr["label"],
                    stockCode = (string)dr["stockCode"],
                    displayPrice = calculatePrice((string)dr[priceColumn], (string)dr["currencyAbbr"], (string)dr["tax"]),
                    stockAmount = int.Parse(dr["stockAmount"].ToString()),

                    label = (string)dr["label"],
                    brand = (string)dr["brand"],
                    mainCategory = (string)dr["mainCategory"],
                    category = (string)dr["category"],
                    subCategory = (string)dr["subCategory"],

                    picture1Path = (string)dr["picture1Path"],
                    //picture2Path = (string)dr["picture2Path"],
                    //picture3Path = (string)dr["picture3Path"],
                    //picture4Path = (string)dr["picture4Path"],

                    details = (string)dr["details"],
                    //rebatedPriceWithoutTax = (string)dr["rebatedPriceWithoutTax"],
                };
                productList.Add(prod);

                // Stok değeri 1 olan ürünleri kaldır.
                var subList = productList.Where(x => x.stockAmount == 1).ToList();
                productList.RemoveAll(x => subList.Any(y => y == x));

            }

            return productList;
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
