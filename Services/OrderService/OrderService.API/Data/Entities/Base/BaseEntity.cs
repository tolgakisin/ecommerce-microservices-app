using System;

namespace OrderService.API.Data.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CreateDate { get; set; }
        public Guid UpdateDate { get; set; }
    }
}
