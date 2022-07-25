using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Interfaces;

namespace UniversityProject.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly UniversityContext _context;
        protected DbSet<T> _entities;

        public BaseRepository(UniversityContext context)
        {
            this._context = context;
            this._entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return  _entities.AsEnumerable();
        }
        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);                 
        }

        public void UpdateById(T entity)
        {
            _entities.Update(entity);            
        }

        public async Task DeleteById(int id)
        {
            T entity = await _entities.FindAsync(id);
            _entities.Remove(entity);        
        }   
    }
}
