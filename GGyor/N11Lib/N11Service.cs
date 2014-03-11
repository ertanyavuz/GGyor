﻿using System;
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
using Authentication = N11Lib.ProductService.Authentication;

namespace N11Lib
{
    public class N11Service
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

        public object GetCategories()
        {
            var service = new CategoryService.CategoryServicePortClient();
            var result = service.GetTopLevelCategories(new GetTopLevelCategoriesRequest());

            return result.categoryList;
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

                //if (pageNum > 15)
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

            var productList = new List<ProductModel>();
            Func<string, string, string, decimal> calculatePrice = (x, curr, kdv) =>
                                                        {
                                                            var price = decimal.Parse(x.Replace(".", ","));
                                                            if (curr == "USD")
                                                                price = price*(decimal) 2.2158;
                                                            else if (curr == "EUR")
                                                                price = price*(decimal) 3.0755;
                                                            else if (curr != "TL")
                                                                throw new NotImplementedException();

                                                            price = price*(int.Parse(kdv) + 100)/100;
                                                            price = Math.Round(price*100) / 100;
                                                            price += 10;
                                                            return price;
                                                        };

            foreach (DataRow dr in dt.Rows)
            {
                //var prod = new ProductBasic
                //           {
                //               //id = dr["id"],
                //               title = (string) dr["label"],
                //               //subtitle = (string)x["subtitle"],
                //               //price = (decimal)x["price"],
                //               productSellerCode = (string) dr["stockCode"],
                //               //saleStatus = (string)x["saleStatus"],
                //               displayPrice = calculatePrice((string)dr["price4"], (string)dr["currencyAbbr"], (string) dr["tax"]),

                //               //// x["productStatus"]
                //           };
                var prod = new ProductModel
                           {
                               //id = dr["id"],
                               title = (string)dr["label"],
                               //subtitle = (string)x["subtitle"],
                               //price = (decimal)x["price"],
                               productSellerCode = (string)dr["stockCode"],
                               //saleStatus = (string)x["saleStatus"],
                               displayPrice = calculatePrice((string)dr["price4"], (string)dr["currencyAbbr"], (string)dr["tax"]),
                               stockAmount = int.Parse(dr["stockAmount"].ToString())
                           };
                productList.Add(prod);
            }

            return productList;
        }


        public object UpdateProducts()
        {
            var sourceList = GetSourceProductsXml();
            var n11List = GetProductsJson();

            var i = 0;
            //foreach (var sourceProd in sourceList)
            //{
            //    i++;
            //    //if (i < 900)
            //    //    continue;
            //    var destProd = n11List.FirstOrDefault(x => x.productSellerCode.Contains(sourceProd.productSellerCode));
            //    if (destProd == null)
            //    {
            //        Debug.WriteLine(String.Format("{1}\t{0} hedefte bulunamadı.", sourceProd.productSellerCode, i));
            //    }
            //    else
            //    {
            //        var sourceAmount = sourceProd.stockAmount;
            //        var destAmount = GetProductStockJson(destProd.id);

            //        if (destProd.displayPrice != sourceProd.displayPrice || sourceAmount != destAmount)
            //        {
            //            Debug.WriteLine("{6}\t{0}\t{3}\t\t{1}\t{2}\t\t{4}\t{5}", sourceProd.productSellerCode, sourceProd.displayPrice, destProd.displayPrice, sourceProd.title, sourceAmount, destAmount, i);

            //            // Update
            //            if (destProd.displayPrice != sourceProd.displayPrice)
            //            {
            //                // update price
            //                Console.WriteLine("price\t{0}\t{1}", destProd.productSellerCode, sourceProd.displayPrice);
            //                UpdateProduct(destProd.productSellerCode, sourceProd.displayPrice);
            //            }
            //            if (sourceAmount != destAmount)
            //            {
            //                // update stock
            //                Console.WriteLine("stock\t{0}\t{1}", destProd.productSellerCode, sourceAmount);
            //                UpdateProductStock(destProd.productSellerCode, sourceAmount);
            //            }
            //        }
            //        else
            //        {
            //            Debug.WriteLine(String.Format("{1}\t{0} aynı.", sourceProd.productSellerCode, i));
            //        }
            //    }
            //}

            i = 0;
            var diffList = n11List.Where(x => !sourceList.Any(y => x.productSellerCode.Contains(y.productSellerCode))).ToList();
            foreach (var destProd in diffList)
            {
                i++;
                Debug.WriteLine("{0}", i);
                var sourceProd = sourceList.FirstOrDefault(x => destProd.productSellerCode.Contains(x.productSellerCode));
                if (sourceProd == null)
                {
                    // Remove
                    RemoveProduct(destProd.productSellerCode);
                }
            }

            return null;
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

    }
}