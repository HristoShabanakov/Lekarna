namespace Lekarna.Data.Configurations
{
    using Lekarna.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            entity.HasKey(x => new { x.MedicineId, x.OrderId });

            entity.HasOne(x => x.Order)
                .WithMany(x => x.OrdersItems)
                .HasForeignKey(x => x.OrderId);

            entity.HasOne(x => x.Medicine)
                .WithMany(x => x.OrdersItems)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
