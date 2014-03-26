using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorMan.Data.Repositories;
using StorMan.Model;

namespace StorMan.Business
{
    public class N11DataService
    {

        public void ClearCtategories(int storeId)
        {
            new N11Repository().ClearCtategories(storeId);
        }

        public void InsertCategory(CategoryModel catModel, int storeId)
        {
            new N11Repository().InsertCategory(catModel, storeId);
        }

        public List<CategoryModel> GetAllCategories(int storeId)
        {
            return new N11Repository().GetAllCategories(storeId);
        }

        public List<CategoryModel> GetSubCategories(int storeId, int? parentId)
        {
            return new N11Repository().GetSubCategories(storeId, parentId);
        }

    }
}
