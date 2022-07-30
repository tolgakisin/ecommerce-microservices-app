using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Common
{
    public interface IRepository<T> where T : BaseEntity
    {
    }
}
