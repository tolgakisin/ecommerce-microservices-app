using AutoMapper;
using IdentityService.API.Contracts.Models.Base;
using IdentityService.API.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Responses.Base
{
    public class BaseResponse<TEntity, TModel> : ResponseBase where TEntity : IEntity where TModel : BaseModel
    {
        public IEnumerable<TModel> Models { get; set; }

        public void SetModels(TEntity entity)
        {
            if (entity == null) return;

            MapperConfiguration mapperConfiguration = new MapperConfiguration(configure =>
            {
                configure.CreateMap<TEntity, TModel>();
            });

            IMapper mapper = new Mapper(mapperConfiguration);
            Models = new List<TModel> { mapper.Map<TEntity, TModel>(entity) };
        }

        public void SetModels(IEnumerable<TEntity> entities)
        {
            if (entities == null) return;

            MapperConfiguration mapperConfiguration = new MapperConfiguration(configure =>
            {
                configure.CreateMap<IEnumerable<TEntity>, IEnumerable<TModel>>();
            });

            IMapper mapper = new Mapper(mapperConfiguration);
            Models = mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        }
    }
}
