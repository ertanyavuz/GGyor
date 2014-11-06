using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EntegrasyonServiceBase;
using GittiGidiyor;
using GittiGidiyor.Product;
using GittiGidiyor.Search;
using StorMan.Model;

namespace GGLib
{

    public class GGProductService : EntegrasyonServiceBase.EntegrasyonServiceBase
    {
        public const string GG_XML_PATH = "http://www.elektrostil.com/index.php?do=catalog/output&pCode=968613169";
        public const string PRICE_COLUMN = "price5";

        public GGProductService()
        {
            setConfig();
        }
        private void setConfig()
        {
            var config = new AuthConfig();
            config.ApiKey = "dTPUgGvVPktCT8TxY74Kkt5szgzMF5UH";
            config.SecretKey = "QZYmEu2VwCxxj3qj";
            config.RoleName = "elektrostil";
            config.RolePass = "PYwQZqfFYJUaNJgdcaJQ7y6jbD8Emhrw";
            ConfigurationManager.setAuthParameters(config);
        }

        private string productDetailToString(productDetailType pr, int i = 0)
        {
            var prodStr = "";
            if (i > 0)
                prodStr += i.ToString() + "\t";
            prodStr += pr.itemId + "\t";
            prodStr += pr.product.title + "\t";
            prodStr += pr.product.affiliateOption.ToString() + "\t";
            prodStr += pr.product.boldOption.ToString() + "\t";
            prodStr += pr.product.catalogOption.ToString() + "\t";
            prodStr += pr.product.vitrineOption.ToString() + "\t";
            prodStr += pr.product.buyNowPrice.ToString() + "\t";
            prodStr += pr.product.startPrice.ToString() + "\t";
            prodStr += pr.product.cargoDetail.cargoCompanies[0] + "\t";

            return prodStr;
        }

        public List<ProductModel> GetProducts(string productType)
        {
            //A - Aktif Şatışlar
            //L - Yeni Listelenenler
            //S - Satılan Ürünler
            //U - Satılmayan Ürünler
            //R - Yeniden Listelenenler
            var prodService = ServiceProvider.getProductService();
            var i = 1;

            var prodList = new List<productDetailType>();

            Action<productServiceListResponse> addToProductList = x =>
            {
                if (x.ackCode == "success")
                {
                    if (x.products == null)
                        return;
                    prodList.AddRange(x.products);
                    foreach (var pr in x.products)
                    {
                        var prodStr = productDetailToString(pr, i++);
                        System.Diagnostics.Debug.WriteLine(prodStr);
                    }
                }
                else
                {
                    throw new Exception("Servis hata mesajı döndürdü: " + x.error.message);
                }
            };

            var response = prodService.getProducts(0, 100, productType, true, "tr");
            addToProductList(response);
            var totalCount = response.productCount;

            while (prodList.Count < totalCount)
            {
                response = prodService.getProducts(prodList.Count, 100, productType, true, "tr");
                addToProductList(response);
            }

            var list = prodList.Select(x => new ProductModel
            {
                id = x.productId,
                title = x.product.title,
                subtitle = x.product.subtitle,
                stockCode = x.itemId,
                displayPrice = (decimal)x.product.buyNowPrice,
                stockAmount = x.product.productCount,
                brand = "",
                details = x.product.description,
                label = x.product.title,
                picture1Path = x.product.photos.Any()
                                ? x.product.photos[0].url
                                : "",
                attributes = x.product.specs != null ? x.product.specs.Select(y => new KeyValuePair<string, string>(y.name, y.value)).ToList()
                                                    : new List<KeyValuePair<string, string>>()
            }).ToList();
            return list;
        }

        public List<ProductModel> GetActiveProducts()
        {
            return GetProducts("A");
        }
        public List<ProductModel> GetFinishedProducts()
        {
            return GetProducts("U");
        }

        public ProductModel GetProduct(string stockCode)
        {
            var prodService = ServiceProvider.getProductService();
            var response = prodService.getProduct("", stockCode, "tr");
            if (response.ackCode == "success")
            {
                return new ProductModel
                       {
                           id = response.productDetail.productId,
                           title = response.productDetail.product.title,
                           subtitle = response.productDetail.product.subtitle,
                           stockCode = response.productDetail.itemId,
                           displayPrice = (decimal)response.productDetail.product.buyNowPrice,
                           stockAmount = response.productDetail.product.productCount,
                           brand = "",
                           details = response.productDetail.product.description,
                           label = response.productDetail.product.title,
                           picture1Path = response.productDetail.product.photos.Any()
                                   ? response.productDetail.product.photos[0].url
                                   : "",
                           attributes = response.productDetail.product.specs.Select(y => new KeyValuePair<string, string>(y.name, y.value))
                               .ToList()
                       };
            }
            else
            {
                return null;
            }
        }

        public object CreateProduct()
        {
            var prodService = ServiceProvider.getProductService();

            var product = new productType();
            product.categoryCode = "a";
            product.title = "Test Ürünü";

            product.specs = new[]
                {
                    new specType {name = "Marka", type = "Combo", value = "Patates", required = true},
                    new specType {name = "Durumu", type = "Combo", value = "Sıfır", required = true},
                    new specType {name = "Garanti", type = "Combo", value = "Yok", required = true}
                };

            product.photos = new[]
                {
                    new photoType {photoId = 0, photoIdSpecified = true, url = "http://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Bamberger_Hoernle.jpg/320px-Bamberger_Hoernle.jpg"},
                    new photoType {photoId = 1, photoIdSpecified = true, url = "http://upload.wikimedia.org/wikipedia/commons/thumb/3/30/Russet_potato_.jpg/320px-Russet_potato_.jpg"}
                };

            product.pageTemplate = 1;
            product.pageTemplateSpecified = true;
            product.description = "Patates (Solanum tuberosum), patlıcangiller (Solanaceae) familyasından yumruları yenen otsu bitki türü.";
            product.format = "A";
            product.startPrice = 0;
            product.startPriceSpecified = true;
            product.buyNowPrice = 5.00;
            product.buyNowPriceSpecified = true;
            product.listingDays = 1;
            product.listingDaysSpecified = true;
            product.productCount = 1;
            product.productCountSpecified = true;

            product.cargoDetail = new cargoDetailType
                {
                    city = "20",
                    cargoCompanies = new[] {"Aras", "Yurtiçi"},
                    cargoDescription = "Kargo alıcıya aittir",
                    shippingPayment = "B",
                    shippingWhere = "Turkey"
                };

            product.affiliateOption = false;
            product.boldOption = false;
            product.catalogOption = true;
            product.vitrineOption = false;
            product.startDate = "17.05.2013 00:00:00";

            var response = prodService.insertProduct("ptt001", product, false, false, "tr");
            if (response != null && response.ackCode == "success")
            {
                var id = response.productId;
                return product;
            }

            return null;

        }

        public void UpdateProduct()
        {
            var service = ServiceProvider.getProductService();
            //service.updateProduct();
        }

        public void UpdateProductPrice(string stockCode, double price)
        {
            var service = ServiceProvider.getProductService();
            price = Math.Round(price, 1);
            if (price > 2000)
                price.GetType();

            productServiceResponse response = null;
            var numTries = 0;
            while (response == null)
            {
                try
                {
                    response = service.updatePrice("", stockCode, price, false, "tr");
                }
                catch (Exception ex)
                {
                    numTries++;
                    if (numTries >= 3)
                    {
                        throw new Exception("Web servis çağrısı başarısız! ", ex);
                    }
                    System.Threading.Thread.Sleep(3000);
                }
            }
            //if (response == null)
            //    throw new Exception("Web servis çağrısı başarısız! ");

            if (response.ackCode != "success")
            {
                if (response.error.message == "Satılan bir ürün için güncelleme yapamazsınız.")
                    System.Diagnostics.Debug.Write(" Ürün satıldığı için güncelleme yapılamadı!");
                else if (response.error.message != "Ürünün bitmesine 12 saatten az süre kaldığı için güncelleme yapamazsınız.")
                    response.GetType();
                else
                    response.GetType();
            }
            else
                System.Diagnostics.Debug.Write(" X ");
        }

        public void UpdateProductStock(string stockCode, int stockAmount)
        {
            var service = ServiceProvider.getProductService();
            var response = service.updateStock("", stockCode, stockAmount, false, "tr");
            if (response.ackCode != "success")
                throw new Exception("Fiyat güncellenemedi: " + response.error.message);
        }

        public void RemoveProducts(List<string> stockCodeList)
        {
            var service = ServiceProvider.getProductService();
            var response = service.finishEarly(new List<int>(), stockCodeList, "tr");
            if (response.ackCode != "success")
                throw new Exception("Ürünlerin satışı bitirilemedi: " + response.error.message);
        }

        //private void ex_button1_Click(object sender, EventArgs e)
        //{
        //    setConfig();
        //    var cityService = ServiceProvider.getCityService();
        //    var serviceNameResult = cityService.getServiceName();
        //    var devService = ServiceProvider.getDeveloperService();
        //    serviceNameResult = devService.getServiceName();

        //    var appService = ServiceProvider.getApplicationService();
        //    serviceNameResult = appService.getServiceName();
        //    //var result = devService.isDeveloper("elektrostil", "Virago97", "tr");
        //    //var response = devService.createDeveloper("ertanyavuz", "passpass", "tr");
        //    var response = appService.getApplicationList("QQPyTB2yVSRGRFJMQDcD", "tr");
        //    var app = response.applications[0];

        //    var catService = ServiceProvider.getCategoryService();
        //    serviceNameResult = catService.getServiceName();
        //    var response2 = catService.getCategories(1, 100, true, true, true, "tr");

        //    var prodService = ServiceProvider.getProductService();
        //    serviceNameResult = prodService.getServiceName();
        //    var response3 = prodService.getProducts(0, 1, "A", true, "tr");
        //    var prodList = new List<productDetailType>();
        //    var i = 0;
        //    while (i < response3.productCount)
        //    {
        //        var response4 = prodService.getProducts(i, 100, "A", true, "tr");
        //        prodList.AddRange(response4.products);
        //        i += response4.products.Length;
        //        if (response4.products.Length == 0)
        //            break;
        //    }

        //    var str = ex_aggregateProducts(prodList);
        //    this.GetType();
        //}

        //private string ex_aggregateProducts(IEnumerable<productDetailType> productList)
        //{
        //    var str = productList.Select(x => x.product.title).Aggregate((x, y) => x + "\r\n" + y);
        //    return str;
        //}
        
        public void SiemensUpdate()
        {
            var prodService = ServiceProvider.getProductService();
            //var response = prodService.getProducts(0, 100, "A", true, "tr");
            //var products = response.products.Select(x => x.product.title).ToList();

            var searchService = ServiceProvider.getSearchService();
            var criteria = new searchCriteriaType
                {
                    format = "S",
                    seller = "elektrostil"
                };
            var searchResponse = searchService.search("Siemens", criteria, 0, 100, true, true, "IA", "tr");


            for (int i = 5; i < searchResponse.count; i++)
            {
                var product = searchResponse.products[i];

                var id = product.productId;

                var p = prodService.getProduct(id.ToString(), "", "tr");

                p.productDetail.product.cargoDetail.shippingPayment = "B";

                var prodResponse = prodService.updateProduct(p.productDetail.productId.ToString(), p.productDetail.itemId, p.productDetail.product, true, false, false, "tr");
                if (prodResponse.ackCode != "success")
                {
                    prodResponse.GetType();
                }
            }

            

            searchResponse.GetType();

        }

        public List<ProductModelGG> GetProductsOnSale()
        {
            var prodService = ServiceProvider.getProductService();
            
            return new List<ProductModelGG>();
        }

        protected bool relistProducts(List<string> productIdList)
        {
            var service = ServiceProvider.getProductService();

            var tempList = productIdList.ToList();
            while (tempList.Any())
            {
                var postList = tempList.GetRange(0, Math.Min(tempList.Count, 400));
                tempList = tempList.Skip(postList.Count).ToList();
                var response = service.relistProducts(new List<int>(), postList, "tr");
                if (response.ackCode != "success")
                    //return false;
                    throw new Exception("Relist failed: " + response.error.message);
                
                // (4) adet ürünü yeniden listeleyebilmeniz için (0.2) TL odemeniz gerekmektedir. (#FT-RAkEPz3Gs33Y) ödeme çeki ile ödemenizi gerçekleştirebilirsiniz.
                var m = System.Text.RegularExpressions.Regex.Match(response.result, "\\((#[\\d\\w\\-]+?)\\)");
                if (!m.Success)
                    throw new Exception("Relist failed: " + "Payment voucher not received.");

                var paymentResponse = service.payPrice(m.Groups[1].Value, "ERKAN", "YAVUZ", "5472440123026771", "829", "07", "17", "tr");
                if (paymentResponse.ackCode != "success")
                    throw new Exception("Payment for Relist failed: " + response.error.message);
                
                //return true;
                
            }

            //var response = service.relistProducts(new List<int>(), productIdList, "tr");
            //if (response.ackCode == "success")
            //{
            //    // (4) adet ürünü yeniden listeleyebilmeniz için (0.2) TL odemeniz gerekmektedir. (#FT-RAkEPz3Gs33Y) ödeme çeki ile ödemenizi gerçekleştirebilirsiniz.
            //    var m = System.Text.RegularExpressions.Regex.Match(response.result, "\\(#([\\d\\w\\-]+?)\\)");
            //    if (m.Success)
            //    {
            //        var paymentResponse = service.payPrice("#" + m.Groups[1].Value, "ERKAN", "YAVUZ", "5472440123026771", "829", "07", "17", "tr");
            //        if (paymentResponse.ackCode == "success")
            //            return true;
            //    }
            //    return true;
            //}
            return true;
        }

        public bool RelistProducts()
        {
            // Kaynak XML
            var sourceList = GetSourceProductsXml(GG_XML_PATH, PRICE_COLUMN);

            // GG - Satışı sona ermiş ürünler
            var ggOldList = GetFinishedProducts();

            var nullList = ggOldList.Where(x => x.stockCode == null).ToList();

            // Yeniden listelenecek ürünler : Satışı sona erip kaynak XML'de yer alan ürünler.
            var relistList = ggOldList.Where(x => sourceList.Any(y => (x.stockCode ?? "").Contains("_" + y.stockCode + "_"))).Select(x => x.stockCode).ToList();

            // Yeniden listele
            relistProducts(relistList);

            return true;
        }

        public bool UpdateProducts()
        {
            // Kaynak XML
            var sourceList = GetSourceProductsXml(GG_XML_PATH, PRICE_COLUMN);
            
            // GG - Satıştaki ürünler : 
            var ggActiveList = GetActiveProducts();
            ggActiveList = ggActiveList.Where(x => x.stockCode != null).ToList();

            // Stok değeri 1 olan ürünleri 0 yap.
            var subList = sourceList.Where(x => x.stockAmount == 1).ToList();
            //subList.ForEach(x =>
            //{
            //    if (x.stockAmount == 1)
            //        x.stockAmount = 0;
            //});
            sourceList.RemoveAll(x => subList.Any(y => y == x));

            var i = 0;
            foreach (var sourceProd in sourceList)
            {
                i++;
                var destProd = ggActiveList.FirstOrDefault(x => x.stockCode.Contains("_" + sourceProd.stockCode + "_")); // == StockCodeToSellerCode(sourceProd.stockCode));

                if (destProd == null)
                {
                    // Yeni ürün.
                    Debug.WriteLine(String.Format("{1}\t{0} hedefte bulunamadı.", sourceProd.stockCode, i));
                }
                else
                {
                    var sourceAmount = sourceProd.stockAmount;
                    //var destAmount = GetProductStock(destProd.stockCode);
                    var destAmount = destProd.stockAmount;

                    sourceProd.displayPrice = Math.Ceiling((sourceProd.displayPrice*10))/10;

                    if (destProd.displayPrice != sourceProd.displayPrice || sourceAmount != destAmount)
                    {
                        Debug.WriteLine("{6}\t{0}\t{3}\t\t{1}\t{2}\t\t{4}\t{5}", sourceProd.stockCode, sourceProd.displayPrice, destProd.displayPrice, sourceProd.title, sourceAmount, destAmount, i);

                        // Update
                        if (destProd.displayPrice != sourceProd.displayPrice)
                        {
                            // update price
                            Console.WriteLine("price\t{0}\t{1}", destProd.stockCode, sourceProd.displayPrice);
                            var diffPercent = (Math.Abs(destProd.displayPrice - sourceProd.displayPrice)) / destProd.displayPrice;
                            if (diffPercent > (decimal)0.05)
                            {
                                Debug.WriteLine("Fiyat çok değişmiş!");
                            }

                            UpdateProductPrice(destProd.stockCode, (double) sourceProd.displayPrice);
                        }
                        if (sourceAmount != destAmount)
                        {
                            // update stock
                            Console.WriteLine("stock\t{0}\t{1}", destProd.stockCode, sourceAmount);
                            UpdateProductStock(destProd.stockCode, sourceAmount);
                        }
                    }
                    else
                    {
                        Debug.WriteLine(String.Format("{1}\t{0} aynı.", sourceProd.stockCode, i));
                    }
                }
            }

            i = 0;
            var diffList = ggActiveList.Where(x => x.stockCode != null && !sourceList.Any(y => x.stockCode.Contains("_" + y.stockCode + "_"))).ToList();
            var removeList = new List<string>();
            foreach (var destProd in diffList)
            {
                i++;
                if (destProd.title.Contains("Timberland"))
                {
                    Debug.WriteLine("{0} skipped\t{1}\t{2}", i, destProd.stockCode, destProd.title);
                    continue;
                }
                //Debug.WriteLine("{0}", i);
                var sourceProd = sourceList.FirstOrDefault(x => destProd.stockCode.Contains("_" + x.stockCode + "_"));
                if (sourceProd == null)
                {
                    // Remove
                    //RemoveProduct(destProd.stockCode);
                    removeList.Add(destProd.stockCode);
                    Debug.WriteLine("{0} sıfırlandı\t{1}\t{2}", i, destProd.stockCode, destProd.title);
                }
                else
                {
                    sourceProd.GetType();
                }
            }

            if (removeList.Any())
                RemoveProducts(removeList);

            return false;
        }
    }
}

