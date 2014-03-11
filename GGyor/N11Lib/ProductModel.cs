using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N11Lib
{
    public class ProductModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string productSellerCode { get; set; }
        public decimal displayPrice { get; set; }
        public int stockAmount { get; set; }
    }
}
