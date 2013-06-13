using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.BusinessTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCategoriesFromXml()
        {
            var xmlPath = @"C:\Users\e.yavuz\Downloads\es.xml";

            var xdoc = XDocument.Load(xmlPath);

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
