using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorMan.Model;

namespace StorMan.Data.Repositories
{
    public class N11Repository
    {

        public void ClearCtategories(int storeId)
        {
            var context = new StorManEntities();

            context.Database.ExecuteSqlCommand("DELETE FROM AttributeValue");
            context.Database.ExecuteSqlCommand("DELETE FROM CategoryAttribute");
            context.Database.ExecuteSqlCommand("DELETE FROM Attribute");
            context.Database.ExecuteSqlCommand("DELETE FROM Category WHERE StoreID=" + storeId.ToString());

        }

        public void InsertCategory(CategoryModel catModel, int storeId)
        {
            var context = new StorManEntities();

            var cat = new Category
            {
                Code = catModel.Code,
                Name = catModel.Name,
                CrDate = DateTime.Now,
                StoreID = storeId,
                
            };
            context.Categories.Add(cat);

            if (catModel.Parent != null)
            {
                var parentCat = context.Categories.FirstOrDefault(x => x.StoreID == storeId && x.Code == catModel.Parent.Code);
                if (parentCat != null)
                {
                    cat.ParentID = parentCat.ID;
                }
                else
                {
                    cat.GetType();
                }
            }

            context.SaveChanges();

            if (catModel.Attributes != null)
            {
                foreach (var attModel in catModel.Attributes)
                {
                    var att = context.Attributes.FirstOrDefault(x => x.ID == attModel.id);
                    if (att == null)
                    {
                        att = new Attribute
                        {
                            ID = (int) attModel.id,
                            Name = attModel.name,
                            IsMandatory = attModel.mandatory,
                            IsMultipleSelect = attModel.multipleSelect,
                            //inputMethod = attModel.inputMethod
                        };
                        context.Attributes.Add(att);
                        context.SaveChanges();

                        foreach (var keyValuePair in attModel.values)
                        {
                            var attValue = new AttributeValue
                            {
                                Attribute = att,
                                Name = keyValuePair.Value
                            };
                            context.AttributeValues.Add(attValue);
                        }
                        context.SaveChanges();
                    }
                    
                    cat.Attributes.Add(att);
                    context.SaveChanges();
                }
            }

        }

        public List<CategoryModel> GetAllCategories(int storeId)
        {
            var context = new StorManEntities();

            var catList = context.Categories.Where(x => x.StoreID == storeId)
                                            .OrderBy(x => x.ParentID).ThenBy(x => x.ID)
                                            .ToList();
            var attList = context.Attributes.Select(x => new
                                            {
                                                x.ID,
                                                x.IsMandatory,
                                                x.IsMultipleSelect,
                                                x.Name,
                                                CategoryID = x.Categories.Select(y => y.ID).FirstOrDefault()
                                            })
                                            .OrderBy(x => x.CategoryID).ThenBy(x => x.ID)
                                            //.ToDictionary(x => x.CategoryID, x => x);
                                            .ToList();

            var attValueList = context.AttributeValues.OrderBy(x => x.AttributeID).ThenBy(x => x.ID)
                                            .ToList();

            var catTable = catList.ToDictionary(x => x.ID, x => x);

            var modelTable = new Dictionary<long, CategoryModel>();
            foreach (var category in catList)
            {
                var model = new CategoryModel
                {
                    ID = category.ID,
                    Name = category.Name,
                    Code = category.Code,
                    Attributes = new List<AttributeModel>(),
                    Children = new List<CategoryModel>()
                };
                modelTable.Add(model.ID, model);

                if (category.ParentID != null)
                {
                    var parent = modelTable[category.ParentID.Value];
                    if (parent == null)
                    {
                        model.GetType(); // What the!!!
                    }
                    else
                    {
                        model.Parent = parent;
                        parent.Children.Add(model);
                    }
                }
            }
            var leafNodes = modelTable.Values.Where(x => !x.Children.Any()).ToList();
            foreach (var subCategory in leafNodes)
            {
                var thisAttList = attList.Where(x => x.CategoryID == subCategory.ID);
                foreach (var att in thisAttList)
                {
                    var attModel = new AttributeModel
                    {
                        id = att.ID,
                        name = att.Name,
                        code = att.ID.ToString(),
                        multipleSelect = att.IsMultipleSelect,
                        mandatory = att.IsMandatory,
                        values = new List<KeyValuePair<long, string>>()
                    };

                    attModel.values = attValueList.Where(x => x.AttributeID == attModel.id)
                                    .Select(x => new KeyValuePair<long, string>(x.ID, x.Name))
                                    .ToList();
                    subCategory.Attributes.Add(attModel);
                }
            }

            var retList = modelTable.Values.Where(x => x.Parent == null).ToList();

            return retList;

        }
        
        public List<CategoryModel> GetSubCategories(int storeId, int? parentId)
        {
            var context = new StorManEntities();

            var list = context.Categories.Where(x => x.StoreID == storeId && x.ParentID == parentId)
                                        .ToList();
            var modelList = list.Select(x => new CategoryModel
                                            {
                                                ID = x.ID,
                                                Name = x.Name,
                                                Code = x.Code,
                                                Children = new List<CategoryModel>(),
                                                Attributes = x.Attributes.Select(y => new AttributeModel
                                                                    {
                                                                        id = y.ID,
                                                                        name = y.Name,
                                                                        code = y.ID.ToString(),
                                                                        mandatory = y.IsMandatory,
                                                                        multipleSelect = y.IsMultipleSelect,
                                                                        values = y.AttributeValues.Select(z => new KeyValuePair<long, string>(z.ID, z.Name))
                                                                                                    .ToList()
                                                                    }).ToList()
                                            })
                                .ToList();

            return modelList;

        }
    }
}
