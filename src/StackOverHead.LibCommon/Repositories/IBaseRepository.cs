using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using StackOverHead.LibCommon.Entities;

namespace StackOverHead.LibCommon.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        void Register(TEntity entity);
        Task RegisterAsync(TEntity entity);
        TEntity GetById(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
