using ProductManager.DataAccess.Data;
using ProductManager.DataAccess.Data.DBContext;
using ProductManager.DataAccess.Models;
using ProductManager.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ProductManagerDbContext context) : base(context)
        {
        }

    }
}
