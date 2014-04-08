using System.Collections.Generic;

namespace EntegrasyonServiceBase
{
    public class ProductModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string stockCode { get; set; }
        public decimal displayPrice { get; set; }
        public int stockAmount { get; set; }
        public string label { get; set; }
        public string brand { get; set; }
        public string mainCategory { get; set; }
        public string category { get; set; }
        public string subCategory { get; set; }
        public string picture1Path { get; set; }
        public string picture2Path { get; set; }
        public string picture3Path { get; set; }
        public string picture4Path { get; set; }
        public string details { get; set; }
        public string rebatedPriceWithoutTax { get; set; }

        public List<KeyValuePair<string, string>> attributes { get; set; }
    }
}
