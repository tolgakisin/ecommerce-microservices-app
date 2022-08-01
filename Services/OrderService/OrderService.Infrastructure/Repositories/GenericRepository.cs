using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Common;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OrderDbContext _orderDbContext;

        public GenericRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public IQueryable<T> Query()
        {
            return _orderDbContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _orderDbContext.Set<T>().AsNoTracking();
        }

        public async virtual Task<T> GetByIdAsync(Guid id)
        {
            return await _orderDbContext.Set<T>().FindAsync(id);
        }

        public async virtual Task<T> SaveAsync(T entity)
        {
            if (entity == null) return null;

            try
            {
                _orderDbContext.AttachRange(entity);
                _orderDbContext.Entry(entity).State = entity.Id == Guid.Empty ? EntityState.Added : EntityState.Modified;

                if ((await _orderDbContext.SaveChangesAsync()) < 1) return null;
            }
            catch (Exception ex)
            {
                throw;
            }

            return entity;
        }

        public async virtual Task<IEnumerable<T>> SaveAsync(List<T> entities)
        {
            if (entities == null || !entities.Any()) return null;

            try
            {
                _orderDbContext.AttachRange(entities);
                foreach (var entity in entities)
                {
                    _orderDbContext.Entry(entity).State = entity.Id == Guid.Empty ? EntityState.Added : EntityState.Modified;
                }

                if ((await _orderDbContext.SaveChangesAsync()) < 1) return null;
            }
            catch (Exception ex)
            {
                throw;
            }

            return entities;
        }
    }
}
