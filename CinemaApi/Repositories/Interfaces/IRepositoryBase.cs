using System.Collections.Generic;

namespace CinemaApi.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity GetByID(object id);
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        void SaveChanges();
    }
}
