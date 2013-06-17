using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GittiGidiyor;
using GittiGidiyor.Product;
using GittiGidiyor.Search;

namespace GGLib
{

    public class GGProductService
    {
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

        public void GetProducts()
        {
            var prodService = ServiceProvider.getProductService();
            var response = prodService.getProducts(0, 100, "A", true, "tr");
            var products = response.products.Select(x => x.product.title).ToList();

            products.GetType();

            var product = response.products[0];

            var str = "";
            foreach (var pr in response.products)
            {
                str += pr.itemId + "\t";
                str += pr.product.title + "\t";
                str += pr.product.affiliateOption.ToString() + "\t";
                str += pr.product.boldOption.ToString() + "\t";
                str += pr.product.catalogOption.ToString() + "\t";
                str += pr.product.vitrineOption.ToString() + "\t";
                str += pr.product.buyNowPrice.ToString() + "\t";
                str += pr.product.startPrice.ToString() + "\t";
                str += pr.product.cargoDetail.cargoCompanies[0] + "\t";

                str += "\n";
            }

                                 
            Console.WriteLine();
        }

        public productType CreateProduct()
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

        }

        private void ex_button1_Click(object sender, EventArgs e)
        {
            setConfig();
            var cityService = ServiceProvider.getCityService();
            var serviceNameResult = cityService.getServiceName();
            var devService = ServiceProvider.getDeveloperService();
            serviceNameResult = devService.getServiceName();

            var appService = ServiceProvider.getApplicationService();
            serviceNameResult = appService.getServiceName();
            //var result = devService.isDeveloper("elektrostil", "Virago97", "tr");
            //var response = devService.createDeveloper("ertanyavuz", "passpass", "tr");
            var response = appService.getApplicationList("QQPyTB2yVSRGRFJMQDcD", "tr");
            var app = response.applications[0];

            var catService = ServiceProvider.getCategoryService();
            serviceNameResult = catService.getServiceName();
            var response2 = catService.getCategories(1, 100, true, true, true, "tr");

            var prodService = ServiceProvider.getProductService();
            serviceNameResult = prodService.getServiceName();
            var response3 = prodService.getProducts(0, 1, "A", true, "tr");
            var prodList = new List<productDetailType>();
            var i = 0;
            while (i < response3.productCount)
            {
                var response4 = prodService.getProducts(i, 100, "A", true, "tr");
                prodList.AddRange(response4.products);
                i += response4.products.Length;
                if (response4.products.Length == 0)
                    break;
            }

            var str = ex_aggregateProducts(prodList);
            this.GetType();
        }

        private string ex_aggregateProducts(IEnumerable<productDetailType> productList)
        {
            var str = productList.Select(x => x.product.title).Aggregate((x, y) => x + "\r\n" + y);
            return str;
        }


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

    }
}

