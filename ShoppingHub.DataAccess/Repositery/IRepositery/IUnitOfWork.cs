using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppinHub.DataAccess.Repositery.IRepositery
{
    public interface IUnitOfWork
    {
        ICategoryRepositery Category { get; }
        IBrandRepositery Brand { get; }
        void Save();
    }
}
