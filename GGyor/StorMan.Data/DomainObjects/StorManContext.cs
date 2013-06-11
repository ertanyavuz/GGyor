
namespace StorMan.Data.DomainObjects
{
    public class StorManContext
    {
    }

    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }
    public class LocalCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Category RemoteCategory { get; set; }
    }

}

