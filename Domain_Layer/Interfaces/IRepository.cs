using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
      Task<IEnumerable<T>> GetAll();
      Task<IQueryable<T>> ExecuteQuery(Expression<Func<T, bool>> predicate);
      Task<T?> GetById(int entityId);
      Task<T> Add(T entity);
      Task<int> Update(T entity);
      Task<int> Delete(int entityId);

    }
}
