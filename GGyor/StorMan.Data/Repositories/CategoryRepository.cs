using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StorMan.Data.DomainObjects;


namespace StorMan.Data.Repositories
{
    public class CategoryRepository
    {
        private StorManContext _context;
        public CategoryRepository()
        {
            _context = new StorManContext();
        }

        public List<Category> GetCategoryList()
        {
            var category = new Category
                {
                    Code = "a",
                    Name = "Bilgisayar"
                };

        }
    }
}
