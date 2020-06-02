using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using StackOverHead.LibCommon.Entities;

namespace StackOverHead.LibCommon.Repositories
{
    public abstract class BaseRepository<TEntity, TDTO> : IBaseRepository<TEntity>
        where TEntity : Entity
        where TDTO : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TDTO> DbSet;
        protected readonly IEntityDtoConverter<TEntity, TDTO> _factory;

        protected BaseRepository(DbContext dbContext, IEntityDtoConverter<TEntity, TDTO> factory)
        {
            _context = dbContext;
            DbSet = _context.Set<TDTO>();
            _factory = factory;
        }

        public void Register(TEntity entity)
        {
            var data = _factory.Execute(entity);
            data = BeforePost(data, EntityState.Added);
            DbSet.Add(data);
            _context.SaveChanges();
        }

        public async Task RegisterAsync(TEntity entity)
        {
            var data = _factory.Execute(entity);
            data = BeforePost(data, EntityState.Added);
            await DbSet.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            var data = _factory.Execute(entity);
            _context.Entry(data).State = EntityState.Modified;
            data = BeforePost(data, EntityState.Modified);
            DbSet.Update(data);
            _context.SaveChanges();
            _context.Entry(data).State = EntityState.Detached;
        }

        public void Remove(TEntity entity)
        {
            var data = _factory.Execute(entity);
            DbSet.Remove(data);
            _context.SaveChanges();
        }

        protected virtual TDTO BeforePost(TDTO model, EntityState state)
        {
            return model;
        }

        public TEntity GetById(Guid id)
        {
            var data = DbSet.Find(id);
            if (data == null)
                return null;
            _context.Entry(data).State = EntityState.Detached;
            var entity = _factory.Execute(data);
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var data = await DbSet.FindAsync(id);
            if (data == null)
                return null;
            var entity = _factory.Execute(data);
            _context.Entry(data).State = EntityState.Detached;
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().Select(dto => _factory.Execute(dto)).ToList();
        }

        protected void Unchange<T>(T model)
        {
            if (model == null)
                return;
            _context.Entry(model).State = EntityState.Unchanged;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                _context.Dispose();

                disposedValue = true;
            }
        }

        ~BaseRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
