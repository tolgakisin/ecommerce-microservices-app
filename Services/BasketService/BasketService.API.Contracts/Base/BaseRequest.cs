using AutoMapper;
using BasketService.API.Contracts.Mappings;
using BasketService.Data.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Base
{
    public abstract class BaseRequest<TModel, TEntity> : IRequest where TModel : BaseModel where TEntity : BaseEntity
    {
        public IEnumerable<TModel> Models { get; set; }

        public List<TEntity> GetEntities()
        {
            MapperConfiguration mapperConfiguration = new(configure =>
            {
                configure.AddMaps(typeof(BaseRequest<TModel, TEntity>).Assembly);
            });
            IMapper mapper = new Mapper(mapperConfiguration);

            return mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(Models).ToList();
        }
    }
}
