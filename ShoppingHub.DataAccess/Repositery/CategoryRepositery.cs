using ShoppingHub.DataAccess.Data;
using ShoppingHub.Models;

using ShoppinHub.DataAccess.Repositery.IRepositery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppinHub.DataAccess.Repositery
{
    public class CategoryRepositery : Repositery<Category>, ICategoryRepositery
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryRepositery(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }
    }
}
