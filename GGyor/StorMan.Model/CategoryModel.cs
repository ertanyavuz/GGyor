using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorMan.Model
{
    public class CategoryModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public CategoryModel Parent { get; set; }
        public List<CategoryModel> Children { get; set; }
        public LocalCategoryModel LocalCategoryModel { get; set; }

        public List<AttributeModel> Attributes { get; set; }

        public override string ToString()
        {
            var str = Name;
            var p = Parent;
            if (Children == null)
                Children = new List<CategoryModel>();

            while (p != null)
            {
                str = String.Format("{0} / {1}", p.Name, str);
                p = p.Parent;
            }
            //str = str + String.Format("{0} ({1})", str, subCategories.Count);

            return str;
        }

    }
    public class LocalCategoryModel
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public LocalCategoryModel ParentCategory { get; set; }
        public List<LocalCategoryModel> ChildCategories { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public CategoryModel RemoteCategory { get; set; }

        public string MainCategoryName
        {
            get
            {
                if (this.Level == 1)
                    return this.Name;
                else if (this.Level == 2 && this.ParentCategory != null)
                    return this.ParentCategory.Name;
                else if (this.Level == 3 && this.ParentCategory != null && this.ParentCategory.ParentCategory != null)
                    return this.ParentCategory.ParentCategory.Name;

                return "?";
            }
        }

        public string CategoryName
        {
            get
            {
                if (this.Level == 1)
                    return "-";
                else if (this.Level == 2)
                    return this.Name;
                else if (this.Level == 3 && this.ParentCategory != null)
                    return this.ParentCategory.Name;

                return "?";
            }
        }

        public string SubCategoryName
        {
            get
            {
                if (this.Level == 1)
                    return "-";
                else if (this.Level == 2)
                    return "-";
                else if (this.Level == 3)
                    return this.Name;

                return "?";
            }
        }

        public string RemoteCategoryName
        {
            get
            {
                if (this.RemoteCategory == null)
                    return "N/A";

                return this.RemoteCategory.Name;
            }
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

    public class CategoryMapModel
    {
        public int ID { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
    }
}
