using GoodBurger.API.Entities.Orders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodBurger.API.Database.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.NameClient).IsRequired().HasMaxLength(100);
        builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");
        builder.Property(o => o.Discount).HasColumnType("decimal(18,2)");
        builder.Property(o => o.Total).HasColumnType("decimal(18,2)");
        builder.Property(o => o.CreatedAt)
            .HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("now()");

        builder.HasMany(o => o.OrderItems)
               .WithOne()                  
               .HasForeignKey(i => i.OrderId) 
               .OnDelete(DeleteBehavior.Cascade);
    }
}