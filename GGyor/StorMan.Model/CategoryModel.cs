using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorMan.Model
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public LocalCategoryModel LocalCategoryModel { get; set; }
    }
    public class LocalCategoryModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public CategoryModel RemoteCategory { get; set; }        
    }

}
