using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StorMan.Model;


namespace StorMan.Data.Repositories
{
    public class CategoryRepository : RepositoryBase
    {
        
        public CategoryRepository()
        {
            _context = new StorManEntities();
        }

        public List<Category> GetCategoryList()
        {
            //var category = new Category
            //    {
            //        Code = "a",
            //        Name = "Bilgisayar"
            //    };
            //_context.Categories.Add(category);
            //_context.SaveChanges();

            var list = _context.Categories.OrderBy(x => x.Code).ToList();

            return list;

        }

        public bool SyncCategories(List<CategoryModel> serverList)
        {
            var currentList = _context.Categories
                        .OrderBy(x => x.Code)
                        //.Select(x => new Category
                        //{
                        //    Code = x.Code,
                        //    Name = x.Name
                        //})
                        .ToList();

            this.Sync(serverList, currentList, (model, category) => model.Code.CompareTo(category.Code), (model, category) =>
                {
                    var updated = false;
                    if (category.Code != model.Code)
                    {
                        category.Code = model.Code;
                        updated = true;
                    }
                    if (category.Name != model.Code)
                    {
                        category.Name = model.Name;
                        updated = true;
                    }

                    if (category.ID == 0)
                        category.CrDate = DateTime.Now;
                    if (category.ID != 0 && updated)
                        category.UpdDate = DateTime.Now;
                    return true;
                });

            _context.SaveChanges();

            return true;

        }
    }
}
