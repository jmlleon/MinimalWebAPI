

using Application_Layer.DTO;

namespace Application_Layer.Interfaces
{
    public interface IStudentService<T> where T : BaseDTO
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int studentId);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        void Delete(int entity);

    }
}
