namespace Lekarna.Data.Configurations
{
    using Lekarna.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> image)
        {
            image
               .HasOne(i => i.Pharmacy)
               .WithOne(p => p.Image)
               .HasForeignKey<Pharmacy>(p => p.ImageId);

            image
               .HasOne(i => i.Supplier)
               .WithOne(p => p.Image)
               .HasForeignKey<Supplier>(p => p.ImageId);
        }
    }
}
