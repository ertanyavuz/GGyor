using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using GittiGidiyor;
using GittiGidiyor.Category;

namespace GGLib
{
    public class GGCategoryService
    {

        public GGCategoryService()
        {
            setConfig();
        }

        private static void setConfig()
        {
            var config = new AuthConfig();
            config.ApiKey = "dTPUgGvVPktCT8TxY74Kkt5szgzMF5UH";
            config.SecretKey = "QZYmEu2VwCxxj3qj";
            config.RoleName = "elektrostil";
            config.RolePass = "PYwQZqfFYJUaNJgdcaJQ7y6jbD8Emhrw";
            ConfigurationManager.setAuthParameters(config);
        }

        public List<Category> getCategories()
        {
            var service = ServiceProvider.getCategoryService();

            var i = 0;
            var rowCount = 0;
            var catList = new List<categoryType>();
            do
            {
                var response = service.getCategories(i, 100, true, true, true, "tr");
                rowCount = response.categoryCount;
                catList.AddRange(response.categories);
                i += 100;

            } while (i < rowCount);

            Console.WriteLine(catList.Count);
        }

        public void XmlParser()
        {

            var xdoc = XDocument.Load(@"C:\elektrostil.xml");
            var q = from d in xdoc.Root.Descendants("item")
                    select d;
            var list = q.ToList();

            foreach (var xElement in list)
            {
                var children = xElement.Elements().ToList();
                if (children.Count > 0)
                {
                    System.Diagnostics.Debug.Write(children[0].Value);
                }
            }

            using (var reader = XmlReader.Create(@"C:\elektrostil.xml"))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            System.Diagnostics.Debug.WriteLine(reader.Name);
                            break;
                        case XmlNodeType.Text:
                            System.Diagnostics.Debug.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.XmlDeclaration:
                        case XmlNodeType.ProcessingInstruction:
                            System.Diagnostics.Debug.WriteLine(reader.Name + ": " + reader.Value);
                            break;
                        case XmlNodeType.Comment:
                            System.Diagnostics.Debug.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                            System.Diagnostics.Debug.WriteLine("end");
                            break;
                        case XmlNodeType.Whitespace:
                            break;
                        default:
                            System.Diagnostics.Debug.WriteLine(reader.NodeType.ToString());
                            break;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("");
        }
    }
}
