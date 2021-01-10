using DigitalMuseums.Core.Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMuseums.Data.Configurations
{
    /// <summary>
    /// The user table configuration.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User), "public");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.UserName).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.BirthDate).IsRequired();

            builder.HasIndex(e => e.Email).IsUnique();

            builder
                .HasOne(src => src.Museum)
                .WithOne(dest => dest.User)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(src => src.Orders)
                .WithOne(dest => dest.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
