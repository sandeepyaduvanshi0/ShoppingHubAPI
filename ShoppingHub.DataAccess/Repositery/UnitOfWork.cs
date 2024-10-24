using ShoppingHub.DataAccess.Data;
using ShoppinHub.DataAccess.Repositery.IRepositery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppinHub.DataAccess.Repositery
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _dbContext;
        public ICategoryRepositery Category { get; private set; }
        public IBrandRepositery Brand { get; private set; }

        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepositery(_dbContext);           
            Brand = new BrandRepositery(_dbContext);           
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
