using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using N11Lib.CategoryService;
using N11Lib.ProductService;
using N11Lib.ProductStockService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StorMan.Model;
using Authentication = N11Lib.ProductService.Authentication;
using ProductModel = EntegrasyonServiceBase.ProductModel;

namespace N11Lib
{
    public class N11Service : EntegrasyonServiceBase.EntegrasyonServiceBase
    {
        private string serviceUrlBase = "";
        private string appKey = "67a73f10-3704-426e-ac9e-038a9a8cfcd0";
        private string appSecret = "T8diuwBn8MxX3FsH";

        protected ProductService.Authentication ProductAuthentication
        {
            get
            {
                return new Authentication
                       {
                           appKey = appKey,
                           appSecret = appSecret
                       };
            }
        }
        protected ProductStockService.Authentication StockAuthentication
        {
            get
            {
                return new ProductStockService.Authentication
                {
                    appKey = appKey,
                    appSecret = appSecret
                };
            }
        }

        public List<CategoryModel> GetCategories()
        {
            var service = new CategoryService.CategoryServicePortClient();
            var sw = new Stopwatch();
            sw.Start();

            var result = service.GetTopLevelCategories(new GetTopLevelCategoriesRequest());

            categoryCount = 1;
            var catList = result.categoryList.Select(x => new CategoryModel
            {
                ID = x.id,
                Name = x.name,
                Code = x.id.ToString()
            }).ToList();

            //catList = catList.Take(2).ToList();

            var i = 0;
            catList.ForEach(x =>
            {
                x.Children = getSubCategories(service, x);
                //Debug.WriteLine("{0} - {1} ({2})", i, x.name, x.subCategories.Count);
            });
            sw.Stop();
            Debug.WriteLine("Elapsed=" + sw.ElapsedMilliseconds.ToString());
            return catList;
        }

        private int categoryCount = 1;
        private List<CategoryModel> getSubCategories(CategoryServicePortClient service, CategoryModel cat)
        {
            Debug.WriteLine("{0}\t{1}\t{2}\t{3}", categoryCount, cat.ID, cat.Parent == null ? 0 : cat.Parent.ID, cat.Name);
            categoryCount++;

            var response = service.GetSubCategories(new GetSubCategoriesRequest { categoryId = cat.ID });
            if (response.result.status != "success")
            {
                Debug.WriteLine("Kategori bulunamadı: {0} - {1}", cat.ID, cat.ToString());
                return new List<CategoryModel>();
            }
            if (response.category == null || response.category.Length == 0)
                return new List<CategoryModel>();
            var list = response.category[0].subCategoryList;
            if (list == null)
            {
                // En alt seviye
                //var attResponse = service.GetCategoryAttributes(new GetCategoryAttributesRequest
                //                                                {
                //                                                    categoryId = cat.ID
                //                                                });
                //cat.attributes = attResponse.category.attributeList
                //                                .Select(x => new AttributeModel
                //                                {
                //                                    id = x.id,
                //                                    name = x.name,
                //                                    values = x.valueList.Select(y => new KeyValuePair<long, string>(y.id, y.name)).ToList()
                //                                })
                //                                .ToList();
                
                var obj = callGet("https://api.n11.com/rest/secure/category/getCategoryAttributes.json",
                                    new[] {"id"},
                                    new[] {cat.ID.ToString()});
                var attArr = obj["response"]["data"]["category"]["attributeList"] as JArray;
                if (attArr == null)
                    cat.Attributes = new List<AttributeModel>();
                else
                    cat.Attributes = attArr.Select(x => new AttributeModel
                                        {
                                            id = (long)x["id"],
                                            name = (string)x["name"],
                                            inputMethod = (string)x["inputMethod"],
                                            mandatory = (bool)x["mandatory"],
                                            multipleSelect = (bool)x["multipleSelect"],
                                            code = (string)x["code"],
                                            //values = x.valueList.Select(y => new KeyValuePair<long, string>(y.id, y.name)).ToList()
                                            values = ((JArray)x["valueList"]).Select(y => new KeyValuePair<long, string>((long)y["id"], (string)y["name"])).ToList()
                                        })
                                        .ToList();
                
                return new List<CategoryModel>();
            }

            var subCatList = list.Select(y => new CategoryModel
            {
                ID = y.id,
                Name = y.name,
                Parent = cat,
                Code = y.id.ToString()
            }).ToList();

            subCatList.ForEach(y => y.Children = getSubCategories(service, y));

            return subCatList;
        }

        public object GetProduct(string sellerCode)
        {
            var url = "https://api.n11.com/rest/secure/product/get.json";
            url += String.Format("?appkey={0}&appsecret={1}&sellerCode={2}", appKey, appSecret, StockCodeToSellerCode(sellerCode));

            var req = WebRequest.Create(url);
            req.ContentType = "text/plain";
            //req.Headers.Add("Accept", "application/json");
            var res = req.GetResponse();

            var st = new System.IO.StreamReader(res.GetResponseStream());
            var result = st.ReadToEnd();
            st.Close();

            var obj = JObject.Parse(result);

            var data = obj["response"]["data"] as JObject;

            if (data == null)
                return null;

            var prod = new ProductModel {
                id = (int) data["id"],
                title = (string) data["title"],
                subtitle = (string) data["subtitle"],
                stockCode = sellerCode,
                //productSellerCode = (string)x["productSellerCode"],
                //saleStatus = (string)x["saleStatus"],
                displayPrice = (decimal) data["displayPrice"],
                
            };

            return prod;
        }
        public object GetProducts()
        {
            var service = new ProductService.ProductServicePortClient();
            var result = service.GetProductList(new GetProductListRequest
                                                {
                                                    auth = this.ProductAuthentication,
                                                    pagingData = new RequestPagingData
                                                                 {
                                                                     currentPage = 0,
                                                                     pageSize = 10
                                                                 }
                                                });

            foreach (var product in result.products)
            {
                Debug.WriteLine(product.displayPrice);
            }

            var result2 = service.GetProductByProductId(new GetProductByProductIdRequest
                                                        {
                                                            auth = this.ProductAuthentication,
                                                            productId = 20761918
                                                        });

            var result3 = service.GetProductBySellerCode(new GetProductBySellerCodeRequest
                                                         {
                                                             auth = this.ProductAuthentication,
                                                             sellerCode = "gni_sb_548_elektrostilxml"
                                                         });

            return result.products;
        }


        public List<ProductBasic> GetProductsJson()
        {
            var max = 100;
            var productList = new List<ProductBasic>();

            var itemsPerPage = 100;
            var pageNum = 1;
            while (true)
            {
                var url = "https://api.n11.com/rest/secure/product/list.json";
                url += String.Format("?appkey={0}&appsecret={1}&currentPage={2}&itemsPerPage={3}", appKey, appSecret, pageNum, itemsPerPage);

                var req = WebRequest.Create(url);
                req.ContentType = "text/plain";
                //req.Headers.Add("Accept", "application/json");
                var res = req.GetResponse();

                var st = new System.IO.StreamReader(res.GetResponseStream());
                var result = st.ReadToEnd();
                st.Close();

                var obj = JObject.Parse(result);

                var data = obj["response"]["data"]["products"] as JArray;

                if (data == null)
                    break;

                var list = data.Select(x => new ProductBasic
                {
                    id = (int)x["id"],
                    title = (string)x["title"],
                    subtitle = (string)x["subtitle"],
                    price = (decimal)x["price"],
                    productSellerCode = (string)x["productSellerCode"],
                    saleStatus = (string)x["saleStatus"],
                    displayPrice = (decimal)x["displayPrice"],
                    // x["productStatus"]
                }).ToList();

                if (list.Count == 0)
                    break;

                productList.AddRange(list);

                System.Diagnostics.Debug.WriteLine("Page " + pageNum.ToString());
                pageNum ++;

                //if (pageNum > 5)
                //    break;
            }

            

            return productList;
        }

        public List<ProductModel> GetSourceProductsXml()
        {
            
            //var xmlPath = @"C:\Users\Ertan\Downloads\N11-XML.xml";
            var xmlPath = @"http://www.elektrostil.com/index.php?do=catalog/output&pCode=9211982202";
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

                                                            price = price*(int.Parse(kdv) + 100)/100;
                                                            price = Math.Round(price*100) / 100;
                                                            price += karAmount;
                                                            return price;
                                                        };

            foreach (DataRow dr in dt.Rows)
            {
                var prod = new ProductModel
                {
                    //id = dr["id"],
                    title = (string) dr["label"],
                    stockCode = (string) dr["stockCode"],
                    displayPrice = calculatePrice((string) dr["price4"], (string) dr["currencyAbbr"], (string) dr["tax"]),
                    stockAmount = int.Parse(dr["stockAmount"].ToString()),

                    label = (string) dr["label"],
                    brand = (string) dr["brand"],
                    mainCategory = (string) dr["mainCategory"],
                    category = (string) dr["category"],
                    subCategory = (string) dr["subCategory"],

                    picture1Path = (string)dr["picture1Path"],
                    picture2Path = (string)dr["picture2Path"],
                    picture3Path = (string)dr["picture3Path"],
                    picture4Path = (string)dr["picture4Path"],

                    details = (string)dr["details"],
                    //rebatedPriceWithoutTax = (string)dr["rebatedPriceWithoutTax"],
                };
                productList.Add(prod);


                //<root>
                //  <item>
                //    <rebatedPriceWithoutTax>10.67</rebatedPriceWithoutTax>
                //  </item>
            }

            return productList;
        }


        public object UpdateProducts()
        {
            var sourceList = GetSourceProductsXml();
            var n11List = GetProductsJson();

            //sourceList = sourceList.Where(x => x.productSellerCode.Contains("NX.MFVEY.004")).ToList();
            //n11List = n11List.Where(x => x.productSellerCode.Contains("NX.MFVEY.004")).ToList();

            var i = 0;
            foreach (var sourceProd in sourceList)
            {
                i++;
                //if (i < 900)
                //    continue;
                var destProd = n11List.FirstOrDefault(x => x.productSellerCode.Contains("_" + sourceProd.stockCode + "_")); // == StockCodeToSellerCode(sourceProd.stockCode));
                if (destProd == null)
                {
                    Debug.WriteLine(String.Format("{1}\t{0} hedefte bulunamadı.", sourceProd.stockCode, i));
                }
                else
                {
                    var sourceAmount = sourceProd.stockAmount;
                    var destAmount = GetProductStockJson(destProd.id);

                    if (destProd.displayPrice != sourceProd.displayPrice || sourceAmount != destAmount)
                    {
                        Debug.WriteLine("{6}\t{0}\t{3}\t\t{1}\t{2}\t\t{4}\t{5}", sourceProd.stockCode, sourceProd.displayPrice, destProd.displayPrice, sourceProd.title, sourceAmount, destAmount, i);

                        // Update
                        if (destProd.displayPrice != sourceProd.displayPrice)
                        {
                            // update price
                            Console.WriteLine("price\t{0}\t{1}", destProd.productSellerCode, sourceProd.displayPrice);
                            var diffPercent = (Math.Abs(destProd.displayPrice - sourceProd.displayPrice)*100) / destProd.displayPrice;
                            if (diffPercent > (decimal) 0.05)
                            {
                                Debug.WriteLine("Fiyat çok değişmiş!");
                            }

                            UpdateProduct(destProd.productSellerCode, sourceProd.displayPrice);
                        }
                        if (sourceAmount != destAmount)
                        {
                            // update stock
                            Console.WriteLine("stock\t{0}\t{1}", destProd.productSellerCode, sourceAmount);
                            UpdateProductStock(destProd.productSellerCode, sourceAmount);
                        }
                    }
                    else
                    {
                        Debug.WriteLine(String.Format("{1}\t{0} aynı.", sourceProd.stockCode, i));
                    }
                }
            }

            //i = 0;
            //var diffList = n11List.Where(x => !sourceList.Any(y => x.productSellerCode.Contains(y.productSellerCode))).ToList();
            //foreach (var destProd in diffList)
            //{
            //    i++;
            //    //Debug.WriteLine("{0}", i);
            //    var sourceProd = sourceList.FirstOrDefault(x => destProd.productSellerCode.Contains(x.productSellerCode));
            //    if (sourceProd == null)
            //    {
            //        // Remove
            //        RemoveProduct(destProd.productSellerCode);
            //        Debug.WriteLine("{0} sıfırlandı\t{1}\t{2}", i, destProd.productSellerCode, destProd.title);
            //    }
            //}

            return null;
        }

        public bool CreateProduct(ProductModel product, long categoryId)
        {
            var service = new ProductServicePortClient();
            var response = service.SaveProduct(new SaveProductRequest
                                {
                                    auth = this.ProductAuthentication,
                                    product = new ProductRequest
                                    {
                                        productSellerCode = StockCodeToSellerCode(product.stockCode),
                                        title = product.title,
                                        subtitle = product.label,
                                        description = product.details,
                                        price = product.displayPrice,
                                        //approvalStatus = "",
                                        //attributes = new ProductAttributeRequest[0],
                                        category = new CategoryRequest
                                                   {
                                                       id = categoryId
                                                   },
                                        //discount = new ProductDiscountRequest(),
                                        //expirationDate = "",
                                        images = new ProductImage[]
                                                 {
                                                     new ProductImage
                                                     {
                                                         order = "1",
                                                         url = product.picture1Path
                                                     }, 
                                                 },
                                        preparingDay = "3",                             //////////////////////////////////////
                                        productCondition = "1", // Yeni
                                        //productionDate = "",
                                        //saleEndDate = "",
                                        //saleStartDate = "",
                                        shipmentTemplate = "Ürün Listeleme",
                                        stockItems = new ProductSkuRequest[]
                                                     {
                                                         new ProductSkuRequest
                                                         {
                                                             quantity = product.stockAmount.ToString(),
                                                             sellerStockCode = product.stockCode,
                                                         }
                                                     },

                                    }
                                });

            return response.result.status == "success";

            //var url = "https://api.n11.com/rest/secure/product/createOrUpdate.json";
            //var paramStr = String.Format("");
            //var paramObj = new JObject();
            //paramObj["productSellerCode"] = StockCodeToSellerCode(product.stockCode);
            //paramObj["title"] = product.title;
            //paramObj["subtitle"] = product.label;
            //paramObj["description"] = product.details;
            //paramObj["category"] = JObject.Parse("{ 'id' : " + categoryId.ToString() + " }");
            //paramObj["price"] = product.displayPrice;
            //paramObj["preparingDay"] = 3;
            //paramObj["productCondition"] = "1";   // Yeni ürün
            //paramObj["images"] = JArray.Parse("[ { 'url': '" + product.picture1Path + "', 'order': 1 }  ]");
            //paramObj["stockItem"] = JObject.Parse("{ 'quantity': " + product.stockAmount.ToString() + " }");
            //paramObj["shipmentTemplate"] = "Ürün Listeleme";

            //var obj = callPost(url, paramObj.ToString());

            //return true;
        }

        public bool UpdateProduct(string sellerCode, decimal newPrice)
        {
            // gni_sb_548_elektrostilxml
            var service = new ProductServicePortClient();
            var result = service.UpdateProductPriceBySellerCode(new UpdateProductPriceBySellerCodeRequest
                                                   {
                                                       auth = this.ProductAuthentication,
                                                       productSellerCode = sellerCode,
                                                       price = newPrice
                                                   });
            return result.result.status == "success";
        }


        public object GetProductStock(string sellerCode)
        {
            var service = new ProductStockServicePortClient();
            var result = service.GetProductStockByProductSellerCode(new GetProductStockByProductSellerCodeRequest
                                                       {
                                                           auth = this.StockAuthentication,
                                                           productSellerCode = sellerCode
                                                       });
            return (result.result.status == "success");
        }
        public int GetProductStockJson(long productId)
        {
            var url = @"https://api.n11.com/rest/secure/product/getStock.json";
            url = url += String.Format("?appkey={0}&appsecret={1}&productId={2}", appKey, appSecret, productId);

            var req = WebRequest.Create(url);
            req.ContentType = "text/plain";
            //req.Headers.Add("Accept", "application/json");
            var res = req.GetResponse();

            var st = new System.IO.StreamReader(res.GetResponseStream());
            var result = st.ReadToEnd();
            st.Close();

            var obj = JObject.Parse(result);

            var arr = obj["response"]["data"]["stockItems"] as JArray;
            var amount = (int) arr[0]["quantity"];

            return amount;
        }

        public bool UpdateProductStock(string sellerCode, int stockAmount)
        {
            //var service = new ProductStockServicePortClient();
            //var stockItem = new StockItemForUpdateStockWithSellerStockCode
            //                {
            //                    quantity = stockAmount.ToString(),
            //                    sellerStockCode = sellerCode
            //                };
            //var arr = new[] {stockItem};
            //var result = service.UpdateStockByStockSellerCode(new UpdateStockByStockSellerCodeRequest
            //                                                  {
            //                                                      auth = this.StockAuthentication,
            //                                                      //stockItems = new StockItemForUpdateStockWithSellerStockCode[] { }
            //                                                  });
            //return result.result.status == "success";

            var obj = CallPostForStockUpdate(sellerCode, stockAmount);

            //var obj = callServiceWithPost(@"https://api.n11.com/rest/secure/product/updateStock.json",
            //                            new[] { "sellerCode", "stock" },
            //                            new[] { sellerCode, stockAmount.ToString() });

            return ((string)obj["response"]["header"]["error"]) == "";
            
        }

        public bool RemoveProduct(string sellerCode)
        {
            //var service = new ProductServicePortClient();
            //var result = service.DeleteProductBySellerCode(new DeleteProductBySellerCodeRequest
            //                                               {
            //                                                   auth = this.ProductAuthentication,
            //                                                   productSellerCode = sellerCode
            //                                               });
            //return result.result.status == "success";
            
            var obj = CallPostForStockUpdate(sellerCode, 0);

            return ((string)obj["response"]["header"]["error"]) == "";
            
        }

        public object GetShipmentTemplates()
        {
            var service = new ShipmentCompanyService.ShipmentCompanyServicePortClient();

            var prod = GetProduct("91.000015");

            return null;
        }

        protected JObject callServiceWithGet(string url, string[] keys, string[] values)
        {
            url = url += String.Format("?appkey={0}&appsecret={1}", appKey, appSecret);
            for (var i = 0; i < keys.Length; i++)
                url += "&" + keys[i] + "=" + values[i];

            var req = WebRequest.Create(url);
            req.ContentType = "text/plain";
            //req.Headers.Add("Accept", "application/json");
            var res = req.GetResponse();

            var st = new System.IO.StreamReader(res.GetResponseStream());
            var result = st.ReadToEnd();
            st.Close();

            var obj = JObject.Parse(result);

            return obj;

        }

        protected JObject callServiceWithPost(string url, string[] keys, string[] values)
        {
            
            var wc = new WebClient();
            //wc.BaseAddress = url;
            wc.Headers["Content-type"] = "multipart/form-data";
            wc.QueryString.Add("appkey", appKey);
            wc.QueryString.Add("appsecret", appSecret);

            var bodyStr = "{";
            for (var i = 0; i < keys.Length; i++)
                bodyStr += "\"" + keys[i] + "\" : \"" + values[i] + "\", ";
            bodyStr = bodyStr.Substring(0, bodyStr.Length - 2);
            bodyStr += "}";

            //wc.QueryString.Add("data", bodyStr);


            //var bodyBytes = System.Text.Encoding.UTF8.GetBytes(bodyStr);
            var wcResult = wc.UploadData(url, "post", System.Text.Encoding.UTF8.GetBytes(bodyStr));
            var coll = new System.Collections.Specialized.NameValueCollection();
            coll.Add("appkey", appKey);
            coll.Add("appsecret", appSecret);
            //coll.Add("data", bodyStr);
            //var wcResult = wc.UploadValues(url, "post", coll);
            wcResult.ToString();
            var resultStr = System.Text.Encoding.UTF8.GetString(wcResult);


            return null;


            //var req = (HttpWebRequest) WebRequest.Create(url);
            //req.Method = "POST";
            //var boundary = "---N11BoundarySkAQdHysJKel8YBM";
            //req.ContentType = "multipart/form-data; boundary=" + boundary;

            //var paramStr = String.Format("{2}appkey={0}&appsecret={1}", appKey, appSecret, boundary);
            //paramStr += "&";


            //var data = System.Text.Encoding.UTF8.GetBytes(paramStr);
            //req.GetRequestStream().Write(data, 0, data.Length);

            //var res = req.GetResponse();

            //var st = new System.IO.StreamReader(res.GetResponseStream());
            //var result = st.ReadToEnd();
            //st.Close();

            //var obj = JObject.Parse(result);

            //return obj;

        }


        public JObject callPost(string url, string jsonParamStr)
        {
            var request = WebRequest.Create(url);
            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            Action<Stream, string, string, bool> addPostParam = (st, paramName, paramValue, asFile) =>
            {
                var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                st.Write(buffer, 0, buffer.Length);
                var contentTypeStr = asFile
                            ? string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", paramName, paramName + ".json", Environment.NewLine)
                                        + string.Format("Content-Type: {0}{1}{1}", "application/json", Environment.NewLine)
                            : string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", paramName, Environment.NewLine);
                buffer = Encoding.ASCII.GetBytes(contentTypeStr);
                st.Write(buffer, 0, buffer.Length);
                buffer = Encoding.UTF8.GetBytes(paramValue + Environment.NewLine);
                st.Write(buffer, 0, buffer.Length);
            };

            using (var requestStream = request.GetRequestStream())
            {
                addPostParam(requestStream, "appkey", appKey, false);
                addPostParam(requestStream, "appsecret", appSecret, false);
                addPostParam(requestStream, "data", jsonParamStr, true);
                
                var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            }

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var stream = new StreamReader(responseStream))
            {
                var str = stream.ReadToEnd();
                return JObject.Parse(str);
            }

        }

        public JObject callGet(string url, string[] keyArr, string[] valueArr)
        {
            url += String.Format("?appkey={0}&appsecret={1}", appKey, appSecret);
            for (var i = 0; i < keyArr.Length; i++)
                url += String.Format("&{0}={1}", keyArr[i], valueArr[i]);

            var req = WebRequest.Create(url);
            req.ContentType = "text/plain";
            //req.Headers.Add("Accept", "application/json");
            var res = req.GetResponse();

            var st = new System.IO.StreamReader(res.GetResponseStream());
            var result = st.ReadToEnd();
            st.Close();

            var obj = JObject.Parse(result);

            return obj;
        }

        public JObject CallPostForStockUpdate(string sellerCode, int stockAmount)
        {
            var url = @"https://api.n11.com/rest/secure/product/updateStock.json";

            var request = WebRequest.Create(url);
            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            using (var requestStream = request.GetRequestStream())
            {
                var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", "appkey", Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.UTF8.GetBytes(appKey + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", "appsecret", Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.UTF8.GetBytes(appSecret + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);

                
                buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", "data", "data.json", Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", "application/json", Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);
                //file.Stream.CopyTo(requestStream);
                var data = System.Text.Encoding.UTF8.GetBytes("{	\"sellerCode\" : \"" + sellerCode + "\", 	\"stock\" : \"" + stockAmount.ToString() + "\" }");
                requestStream.Write(data, 0, data.Length);
                buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
                
                var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            }

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var stream = new StreamReader(responseStream))
            {
                var str = stream.ReadToEnd();
                return JObject.Parse(str);
            }
        }


        public static string StockCodeToSellerCode(string stockCode)
        {
            return String.Format("gni_{0}_elektrostilxml", stockCode);
        }

        public static string SellerCodeToStockCode(string sellerCode)
        {
            if (sellerCode.StartsWith("gni_") && sellerCode.EndsWith("_elektrostilxml"))
                return sellerCode.Substring(4, sellerCode.Length - 19);

            return "";
        }

    }
}
