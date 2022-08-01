using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Infrastructure.Context;
using System;

namespace OrderService.Infrastructure.EntityConfigurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems", OrderDbContext.DEFAULT_SCHEMA);

            builder.Property<Guid>(nameof(OrderItem.OrderId)).IsRequired();

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
