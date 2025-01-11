using Domain_Layer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EntityFrameWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentContext _studentContext;

        public UnitOfWork(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _studentContext.SaveChangesAsync(); 
        }
    }
}
