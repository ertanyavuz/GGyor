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

        public void GetCategories(int storeId)
        {
            var context = new StorManEntities();

            var catList = context.Categories.Where(x => x.StoreID == storeId).ToList();


        }
    }
}
