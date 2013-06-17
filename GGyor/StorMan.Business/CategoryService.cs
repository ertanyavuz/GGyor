using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorMan.Data.Repositories;
using StorMan.Model;

namespace StorMan.Business
{
    public class CategoryService
    {
        CategoryRepository _repository = new CategoryRepository();

        public List<CategoryModel> SyncCategoriesFromServer()
        {
            return null;
        }

        public List<LocalCategoryModel> GetLocalCategoryList()
        {
            var list = _repository.GetLocalCategoryList();

            return list;
        }

        public Dictionary<string, CategoryModel> GetCategoryTable()
        {
            var table = _repository.GetCategoryTable();

            return table;
        }
    }
}
