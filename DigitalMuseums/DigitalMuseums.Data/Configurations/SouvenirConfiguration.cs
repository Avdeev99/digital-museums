using DigitalMuseums.Core.Domain.Models.Adjacent;
using DigitalMuseums.Core.Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMuseums.Data.Configurations
{
    /// <summary>
    /// The souvenir table configuration.
    /// </summary>
    public class SouvenirConfiguration : IEntityTypeConfiguration<Souvenir>
    {
        public void Configure(EntityTypeBuilder<Souvenir> builder)
        {
            builder.ToTable(nameof(Souvenir), "public");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.AvailableUnits).IsRequired();
            builder.Property(e => e.Tags).IsRequired();

            builder
                .HasMany(src => src.Orders)
                .WithMany(dest => dest.Souvenirs)
                .UsingEntity<SouvenirOrderDetails>(
                        e => e.HasOne(src => src.Order).WithMany().HasForeignKey(o => o.OrderId),
                        e => e.HasOne(src => src.Souvenir).WithMany().HasForeignKey(o => o.SouvenirId),
                        e => e.HasKey(od => od.Id));
            
            builder
                .HasOne(src => src.Museum)
                .WithMany(dest => dest.Souvenirs)
                .HasForeignKey(e => e.MuseumId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
