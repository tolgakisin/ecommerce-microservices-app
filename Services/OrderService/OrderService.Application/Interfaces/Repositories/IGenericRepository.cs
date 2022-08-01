using OrderService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Query();
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task<T> SaveAsync(T entity);
        Task<IEnumerable<T>> SaveAsync(List<T> entities);
    }
}
