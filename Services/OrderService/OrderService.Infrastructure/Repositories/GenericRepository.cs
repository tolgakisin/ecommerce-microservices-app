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

        public IEnumerable<T> GetAll()
        {
            return _orderDbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _orderDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> SaveAsync(T t)
        {
            if (t == null) return null;

            try
            {
                _orderDbContext.Entry(t).State = t.Id == default ? EntityState.Added : EntityState.Modified;

                if ((await _orderDbContext.SaveChangesAsync()) < 1) return null;
            }
            catch (Exception)
            {
                throw;
            }

            return t;
        }
    }
}
