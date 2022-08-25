using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversityProject.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        void UpdateById(T entity);
        Task DeleteById(int id);

    }
}
