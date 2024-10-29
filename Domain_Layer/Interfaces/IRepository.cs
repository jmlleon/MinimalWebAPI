using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
      Task<IEnumerable<T>> GetAll();
      Task<T> GetById(int studentId);
      Task<T> Add(T entity);
      Task<T> Update(T entity);
      void Delete(int entity);

    }
}
