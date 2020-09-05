namespace Lekarna.Data.Configurations
{
    using Lekarna.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order
                .HasOne(p => p.Pharmacy)
                .WithMany(o => o.Orders)
                .HasForeignKey(p => p.PharmacyId)
                .HasForeignKey(o => o.OrderItemId);
        }
    }
}
