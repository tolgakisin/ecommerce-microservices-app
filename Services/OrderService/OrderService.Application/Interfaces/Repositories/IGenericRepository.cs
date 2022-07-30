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
        Task<T> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> SaveAsync(T t);
    }
}
