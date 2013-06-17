using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorMan.Data.Repositories;
using StorMan.Model;

namespace UnitTestProject1.BusinessTest
{
    [TestClass]
    public class BusinessTest
    {
        [TestMethod]
        public void GetCategoriesFromXml()
        {
            var repo = new CategoryRepository();
            var xmlPath = @"http://www.elektrostil.com/index.php?do=catalog/output&pCode=968613169";

            var xdoc = XDocument.Load(xmlPath);

            var q = from d in xdoc.Root.Descendants("item")
                    select d;
            var list = q.ToList();

            var catTable = new Dictionary<string, Dictionary<string, List<string>>>();

            foreach (var xElement in list)
            {
                var children = xElement.Elements().ToList();
                if (children.Count > 0)
                {
                    System.Diagnostics.Debug.Write(children[0].Value);
                    var mainCategory = children.Where(x => x.Name == "mainCategory").Select(x => x.Value).FirstOrDefault();
                    var category = children.Where(x => x.Name == "category").Select(x => x.Value).FirstOrDefault();
                    var subCategory = children.Where(x => x.Name == "subCategory").Select(x => x.Value).FirstOrDefault();

                    if (String.IsNullOrWhiteSpace(mainCategory))
                        continue;
                    if (!catTable.ContainsKey(mainCategory))
                        catTable.Add(mainCategory, new Dictionary<string, List<string>>());

                    if (String.IsNullOrWhiteSpace(category))
                        continue;
                    var subTable = catTable[mainCategory];
                    if (!subTable.ContainsKey(category))
                        subTable.Add(category, new List<string>());

                    if (String.IsNullOrWhiteSpace(subCategory))
                        continue;
                    if (!subTable[category].Contains(subCategory))
                        subTable[category].Add(subCategory);

                    //if (!catTable.Any(x => x.Item1 == mainCategory && x.Item2 == category && x.Item3 == subCategory))
                    //    catTable.Add(new Tuple<string, string, string>(mainCategory, category, subCategory));
                }
            }
            //catTable = catTable.OrderBy(x => x.Item3).ThenBy(x => x.Item2).ThenBy(x => x.Item1).ToList();

            var modelList = new List<LocalCategoryModel>();
            foreach (var mainCatName in catTable.Keys)
            {
                var table = catTable[mainCatName];
                var mainCategory = new LocalCategoryModel
                    {
                        Name = mainCatName,
                        Level = 1,
                        ParentCategory = null
                    };
                modelList.Add(mainCategory);

                foreach (var catName in table.Keys)
                {
                    var subList = table[catName];
                    var category = new LocalCategoryModel
                        {
                            Name = catName,
                            Level = 2,
                            ParentCategory = mainCategory
                        };
                    modelList.Add(category);
                    foreach (var subCatName in subList)
                    {
                        var subCategory = new LocalCategoryModel
                            {
                                Name = subCatName,
                                Level = 3,
                                ParentCategory = category
                            };
                        modelList.Add(subCategory);
                    }
                }
            }

            repo.SyncLocalCategories(modelList);

            //foreach (var tuple in catTable)
            //{
                
            //    var catName = "";
            //    var level = 0;
            //    LocalCategoryModel parentCategory = null;
            //    if (!String.IsNullOrWhiteSpace(tuple.Item3))
            //    {
            //        catName = tuple.Item3;
            //        level = 3;
            //        parentCategory = modelList.Single(x => x.Name == tuple.Item2 && x.ParentCategory.Name == tuple.Item1 && x.Level == 2);
            //    }
            //    else if (!String.IsNullOrWhiteSpace(tuple.Item2))
            //    {
            //        catName = tuple.Item2;
            //        level = 2;
            //        parentCategory = modelList.Single(x => x.Name == tuple.Item1 && x.Level == 1);
            //    }
            //    else
            //    {
            //        catName = tuple.Item1;
            //        level = 1;
            //        parentCategory = null;
            //    }

            //    modelList.Add(new LocalCategoryModel
            //        {
            //            Code = "",
            //            Name = catName,
            //            Level = level,
            //            ParentCategory = parentCategory
            //        });
            //}

            //repo.SyncLocalCategories(modelList);


            //using (var reader = XmlReader.Create(@"C:\elektrostil.xml"))
            //{
            //    while (reader.Read())
            //    {
            //        switch (reader.NodeType)
            //        {
            //            case XmlNodeType.Element:
            //                System.Diagnostics.Debug.WriteLine(reader.Name);
            //                break;
            //            case XmlNodeType.Text:
            //                System.Diagnostics.Debug.WriteLine(reader.Value);
            //                break;
            //            case XmlNodeType.XmlDeclaration:
            //            case XmlNodeType.ProcessingInstruction:
            //                System.Diagnostics.Debug.WriteLine(reader.Name + ": " + reader.Value);
            //                break;
            //            case XmlNodeType.Comment:
            //                System.Diagnostics.Debug.WriteLine(reader.Value);
            //                break;
            //            case XmlNodeType.EndElement:
            //                System.Diagnostics.Debug.WriteLine("end");
            //                break;
            //            case XmlNodeType.Whitespace:
            //                break;
            //            default:
            //                System.Diagnostics.Debug.WriteLine(reader.NodeType.ToString());
            //                break;
            //        }
            //    }
            //}

            System.Diagnostics.Debug.WriteLine("");

            Assert.IsTrue(true);

        }

        [TestMethod]
        public void GetProducts()
        {
            var service = new GGLib.GGProductService();

            service.GetProducts();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SiemensUpdateTest()
        {
            var service = new GGLib.GGProductService();

            service.SiemensUpdate();

            Assert.IsTrue(true);
        }
        
    }
}
