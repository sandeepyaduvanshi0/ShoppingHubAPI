using ShoppingHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppinHub.DataAccess.Repositery.IRepositery
{
    public interface IBrandRepositery : IRepositery<Brand>
    {
        void Update(Brand brand);
    }
}
