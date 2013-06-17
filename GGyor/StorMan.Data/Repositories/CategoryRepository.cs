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

        public List<CategoryModel> GetCategoryList()
        {
            //var category = new Category
            //    {
            //        Code = "a",
            //        Name = "Bilgisayar"
            //    };
            //_context.Categories.Add(category);
            //_context.SaveChanges();

            var list = _context.Categories.OrderBy(x => x.Code)
                                .Select(x => new CategoryModel
                                    {
                                        Code = x.Code,
                                        Name = x.Name,
                                        LocalCategoryModel = x.LocalCategories.Select(y => new LocalCategoryModel
                                            {
                                                ID = y.ID,
                                                Name = y.Name,
                                                Code = y.Code,
                                                Level = y.Level ?? 0
                                            }).FirstOrDefault()
                                    })
                                .ToList();

            foreach (var categoryModel in list)
            {
                categoryModel.LocalCategoryModel.RemoteCategory = categoryModel;
                if (categoryModel.Code.Length > 1)
                    categoryModel.Parent = list.FirstOrDefault(x => x.Code == categoryModel.Code.Substring(0, categoryModel.Code.Length - 1));
                categoryModel.Children = list.Where(x => x.Code.StartsWith(categoryModel.Code)).ToList();
            }

            return list;

        }

        public List<LocalCategoryModel> GetLocalCategoryList()
        {
            var list = _context.LocalCategories.OrderBy(x => x.ID)
                            .Select(x => new LocalCategoryModel
                                {
                                    ID = x.ID,
                                    Name = x.Name,
                                    Code = x.Code,
                                    Level = x.Level ?? 0,
                                    ParentID = x.ParentID ?? 0
                                })
                        .ToList();
            list.Where(x => x.Level == 2).ToList().ForEach(x =>
                {
                    x.ParentCategory = list.FirstOrDefault(y => y.Level == 1 && y.ID == x.ParentID);
                });
            list.Where(x => x.Level == 3).ToList().ForEach(x =>
            {
                x.ParentCategory = list.FirstOrDefault(y => y.Level == 2 && y.ID == x.ParentID);
            });

            return list;
        }

        public Dictionary<string, CategoryModel> GetCategoryTable()
        {
            var catTable = _context.Categories.OrderBy(x => x.Code)
                            .ToDictionary(x => x.Code, x => new CategoryModel
                            {
                                ID = x.ID,
                                Name = x.Name,
                                Code = x.Code
                            });
            return catTable;
        }

        public bool SyncLocalCategories(List<LocalCategoryModel> serverList)
        {
            var currentList = _context.LocalCategories
                            .OrderBy(x => x.ID)
                            .ToList();
            this.Sync(serverList, currentList, (memObj, dbObj) => memObj.ID.CompareTo(dbObj.ID), (memObj, dbObj) =>
                {
                    dbObj.Code = memObj.Code;
                    dbObj.Name = memObj.Name;
                    dbObj.Level = memObj.Level;

                    if (memObj.ParentCategory == null)
                    {
                        dbObj.ParentID = null;
                    }
                    else
                    {
                        if (memObj.Level == 2)
                        {
                            var parent = currentList.FirstOrDefault(x => x.Name == memObj.ParentCategory.Name && x.Level == 1)
                                            ??
                                        _context.LocalCategories.Local.FirstOrDefault(x => x.Name == memObj.ParentCategory.Name && x.Level == 1);
                            dbObj.LocalCategory2 = parent;
                        }
                        else if (memObj.Level == 3)
                        {
                            var parent = currentList.FirstOrDefault(x => x.Name == memObj.ParentCategory.Name && x.Level == 2)
                                            ??
                                        _context.LocalCategories.Local.FirstOrDefault(x => x.Name == memObj.ParentCategory.Name && x.Level == 2);
                            dbObj.LocalCategory2 = parent;
                        }
                    }

                    if (dbObj.ID == 0)
                        dbObj.CrDate = DateTime.Now;
                    return true;
                });

            _context.SaveChanges();

            return true;
        }

        public bool SyncCategories(List<CategoryModel> serverList)
        {
            var currentList = _context.Categories
                        .OrderBy(x => x.Code)
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
