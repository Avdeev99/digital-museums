using DigitalMuseums.Core.Domain.Models.Adjacent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMuseums.Data.Configurations
{
    public class SouvenirOrderDetailConfiguration : IEntityTypeConfiguration<SouvenirOrderDetail>
    {
        public void Configure(EntityTypeBuilder<SouvenirOrderDetail> builder)
        {
            builder.ToTable(nameof(SouvenirOrderDetail), "public");

            builder.HasKey(x => new {x.SouvenirId, x.OrderId});
            
            builder
                .HasOne(src => src.Order)
                .WithMany(dest => dest.OrderDetails)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasOne(src => src.Souvenir)
                .WithMany(dest => dest.OrderDetails)
                .HasForeignKey(e => e.SouvenirId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        }
    }
}