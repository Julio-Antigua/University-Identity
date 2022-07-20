using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversityProject.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        void UpdateById(T entity);
        Task DeleteById(int id);

    }
}
