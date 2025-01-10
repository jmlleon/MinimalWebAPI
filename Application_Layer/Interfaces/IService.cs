

using Application_Layer.DTO;
using Domain_Layer.Errors;
using Domain_Layer.Models;
using System.Linq.Expressions;

namespace Application_Layer.Interfaces
{
    public interface IService<T, U> where T : BaseDTO where U : BaseEntity
    {
        Task<CustomResult<IEnumerable<T>>> GetAll();
        Task<CustomResult<T>> GetById(int entityId);         
        Task<CustomResult<IQueryable<T>>> ExecuteQuery(Expression<Func<U, bool>> predicate);   
        
        //Return with Result class of Milan
        Task<Result> Add(T entity);
        Task<CustomResult<int>> Update(T entity, int id);
        Task<CustomResult<int>> Delete(int entity);

    }
}
