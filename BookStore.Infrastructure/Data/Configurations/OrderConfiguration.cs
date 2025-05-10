using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId);

            builder.HasMany(o => o.OrderDetails).WithOne(od => od.Order).HasForeignKey(od => od.OrderId);
        }
    }

    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.Id);
            builder.Property(od => od.Quantity).IsRequired();
            builder.Property(od => od.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne(od => od.Book).WithMany().HasForeignKey(od => od.BookId);
        }
    }
}