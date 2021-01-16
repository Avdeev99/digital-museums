using DigitalMuseums.Core.Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMuseums.Data.Configurations
{
    /// <summary>
    /// The museum table configuration.
    /// </summary>
    public class MuseumConfiguration : IEntityTypeConfiguration<Museum>
    {
        public void Configure(EntityTypeBuilder<Museum> builder)
        {
            builder.ToTable(nameof(Museum), "public");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.LocationId).IsRequired();
            builder.Property(e => e.GenreId).IsRequired();

            builder
                .HasOne(src => src.Genre)
                .WithMany(dest => dest.Museums)
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(src => src.Location)
                .WithMany(dest => dest.Museums)
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
