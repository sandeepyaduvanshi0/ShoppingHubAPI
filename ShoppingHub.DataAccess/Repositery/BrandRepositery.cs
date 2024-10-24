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
    public class BrandRepositery : Repositery<Brand>, IBrandRepositery
    {
        private readonly ApplicationDBContext _dbContext;
        public BrandRepositery(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Brand brand)
        {
            _dbContext.Brands.Update(brand);
        }
    }
}
