
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace StorMan.Data.DomainObjects
{

    public class StorManContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<LocalCategory> LocalCategories { get; set; }
    }

    public class Category
    {
        [Key]
        public int ID { get; set; } 
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual List<LocalCategory> LocalCategories  { get; set; }

    }

    public class LocalCategory
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual List<Category> RemoteCategories { get; set; }

    }

    
}

