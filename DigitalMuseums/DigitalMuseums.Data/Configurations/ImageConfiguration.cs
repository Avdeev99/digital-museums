using DigitalMuseums.Core.Domain.Models.Secondary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMuseums.Data.Configurations
{
    /// <summary>
    /// The image table configuration.
    /// </summary>
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable(nameof(Image), "public");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Path).IsRequired();

            builder
                .HasOne(src => src.Museum)
                .WithMany(dest => dest.Images)
                .HasForeignKey(e => e.MuseumId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(src => src.Exhibit)
                .WithMany(dest => dest.Images)
                .HasForeignKey(e => e.ExhibitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(src => src.Souvenir)
                .WithMany(dest => dest.Images)
                .HasForeignKey(e => e.SouvenirId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
