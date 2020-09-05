namespace Lekarna.Data.Configurations
{
    using Lekarna.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> supplier)
        {
            supplier
               .HasMany(o => o.Offers)
               .WithOne(s => s.Supplier)
               .HasForeignKey(s => s.SupplierId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
