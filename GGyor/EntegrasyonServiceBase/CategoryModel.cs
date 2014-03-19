using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntegrasyonServiceBase
{
    public class CategoryModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public List<CategoryModel> subCategories { get; set; }
        public CategoryModel parent { get; set; }

        public List<AttributeModel> attributes { get; set; }

        public override string ToString()
        {
            var str = name;
            var p = parent;
            if (subCategories == null)
                subCategories = new List<CategoryModel>();

            while (p != null)
            {
                str = String.Format("{0} / {1}", p.name, str);
                p = p.parent;
            }
            //str = str + String.Format("{0} ({1})", str, subCategories.Count);

            return str;
        }

        
    }

    public class AttributeModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public bool mandatory { get; set; }
        public bool multipleSelect { get; set; }
        public List<KeyValuePair<long, string>> values { get; set; }

        public string inputMethod { get; set; }

        public string code { get; set; }
    }
}
