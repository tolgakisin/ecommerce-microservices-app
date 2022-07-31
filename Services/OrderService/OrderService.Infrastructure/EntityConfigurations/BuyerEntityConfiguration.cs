using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.BuyerAggregate;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.EntityConfigurations
{
    public class BuyerEntityConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable("Buyers", OrderDbContext.DEFAULT_SCHEMA);

            builder.Property<int>("_payments")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Payments")
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Buyer.Payments));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
