using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GittiGidiyor;
using GittiGidiyor.Product;

namespace GGLib
{
    class GGProductService
    {

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


    }
}
