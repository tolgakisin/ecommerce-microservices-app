using IdentityService.API.Data.Entities;
using IdentityService.API.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Data.Common
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public async Task<TEntity> SaveAsync<TEntity>(TEntity entity)
        {
            var idProperty = entity.GetType().GetProperty("Id");
            var idValue = idProperty.GetValue(entity);
            var baseInterfaces = entity.GetType().GetInterfaces();
            bool isAddOperation = idValue == null || (Utils.Common.IsGuid(idValue.ToString()) && Guid.Equals(idValue, Guid.Empty));

            if (isAddOperation)
            {
                if (baseInterfaces.Any(x => x == typeof(IEntity)))
                {
                    idProperty.SetValue(entity, Guid.NewGuid());
                    entity.GetType().GetProperty("CreateDate").SetValue(entity, DateTime.Now);
                }

                this.Entry(entity).State = EntityState.Added;
            }
            else
            {
                if (baseInterfaces.Any(x => x == typeof(IEntity)))
                    entity.GetType().GetProperty("UpdateDate").SetValue(entity, DateTime.Now);

                this.Entry(entity).State = EntityState.Modified;
            }
            await this.SaveChangesAsync();

            return entity;
        }
    }
}
