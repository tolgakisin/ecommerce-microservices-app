using AutoMapper;
using BasketService.Data.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Base
{
    public class BaseResponse<TEntity, TModel> : ResponseBase where TEntity : IEntity where TModel : BaseModel
    {
        public IEnumerable<TModel> Models { get; set; }

        public void SetModels(TEntity entity)
        {
            if (entity == null) return;

            MapperConfiguration mapperConfiguration = new(configure =>
            {
                configure.AddMaps(typeof(BaseResponse<TEntity, TModel>).Assembly);
            });

            IMapper mapper = new Mapper(mapperConfiguration);
            Models = new List<TModel> { mapper.Map<TEntity, TModel>(entity) };
        }

        public void SetModels(IEnumerable<TEntity> entities)
        {
            if (entities == null) return;

            MapperConfiguration mapperConfiguration = new(configure =>
            {
                configure.AddMaps(typeof(BaseResponse<TEntity, TModel>).Assembly);
            });

            IMapper mapper = new Mapper(mapperConfiguration);
            Models = mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        }
    }

    public abstract class ResponseBase
    {
        public bool IsSuccessed { get; set; }
        public string ErrorMessage { get; set; }
    }
}
