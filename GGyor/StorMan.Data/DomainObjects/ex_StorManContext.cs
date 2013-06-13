
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.ComponentModel.DataAnnotations;

//namespace StorMan.Data.DomainObjects
//{

//    public class StorManContext : DbContext
//    {
//        public StorManContext()
//            : base("Name=StorManContext")
//        {

//        }

//        public DbSet<Category> Categories { get; set; }
//        public DbSet<LocalCategory> LocalCategories { get; set; }
//    }

//    public class Category
//    {
//        [Key]
//        public string Code { get; set; }
//        public string Name { get; set; }
//        public virtual List<LocalCategory> LocalCategories  { get; set; }

//    }

//    public class LocalCategory
//    {
//        [Key]
//        public string Code { get; set; }
//        public string Name { get; set; }
//        public LocalCategory ParentCategory { get; set; }
//        public List<LocalCategory> ChildCategories { get; set; }
//        public virtual List<Category> RemoteCategories { get; set; }

//    }

    
//}

