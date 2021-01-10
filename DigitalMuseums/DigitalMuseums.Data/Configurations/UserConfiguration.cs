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

            //builder.Property(e => e.UserName).IsRequired();
            //builder.Property(e => e.Email).IsRequired();
            //builder.Property(e => e.Password).IsRequired();
            //builder.Property(e => e.CreationDate).IsRequired();
            //builder.Property(e => e.LastLoginDate).IsRequired();

            //builder.HasIndex(e => e.Email).IsUnique();
            //builder.HasIndex(e => e.UserName).IsUnique();
        }
    }
}
