using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void getCategories()
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

    }
}
