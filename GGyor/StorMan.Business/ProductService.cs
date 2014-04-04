using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GGLib;
using StorMan.Model;

namespace StorMan.Business
{
    public class ProductService
    {
        private GGProductService _ggProductService;

        public List<ProductModelGG> GetProductsOnSale()
        {
            var list = _ggProductService.GetProductsOnSale();
            return list;


        }

    }
}
