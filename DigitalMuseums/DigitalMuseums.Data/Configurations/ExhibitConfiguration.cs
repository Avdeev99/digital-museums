using DigitalMuseums.Core.Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMuseums.Data.Configurations
{
    /// <summary>
    /// The exhibit table configuration.
    /// </summary>
    public class ExhibitConfiguration : IEntityTypeConfiguration<Exhibit>
    {
        public void Configure(EntityTypeBuilder<Exhibit> builder)
        {
            builder.ToTable(nameof(Exhibit), "public");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Author).IsRequired();
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.Tags).IsRequired();
            
            builder
                .HasOne(src => src.Museum)
                .WithMany(dest => dest.Exhibits)
                .HasForeignKey(e => e.MuseumId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
