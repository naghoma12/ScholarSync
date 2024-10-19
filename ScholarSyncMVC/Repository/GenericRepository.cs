using Microsoft.EntityFrameworkCore;
using ScholarSyncMVC.Data;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;

namespace ScholarSyncMVC.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> , IDisposable where T : BaseEntity
    {
        private readonly ScholarSyncConext _conext;

        public GenericRepository(ScholarSyncConext conext)
        {
            _conext = conext;
        }

        public void Add(T entity)
        {
            _conext.Add(entity);
        }

        public int Complet()
        {
          return _conext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _conext.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            
            return await _conext.Set<T>().Where(x =>x.IsDeleted == false).ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return _conext.Set<T>().FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        }

        public void Update(T entity)
        {
           _conext.Update(entity);
        }

        public void Dispose()
        {
            _conext.Dispose();
        }
    }
}
