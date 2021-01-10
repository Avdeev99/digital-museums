using DigitalMuseums.Core.Domain.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMuseums.Data.Configurations
{
    /// <summary>
    /// The region table configuration.
    /// </summary>
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable(nameof(Region), "public");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.CountryId).IsRequired();

            builder
                .HasMany(src => src.Cities)
                .WithOne(dest => dest.Region)
                .HasForeignKey(e => e.RegionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
