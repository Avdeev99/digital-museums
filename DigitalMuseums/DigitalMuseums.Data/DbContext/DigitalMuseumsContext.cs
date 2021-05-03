using System.IO;
using System.Linq;
using System.Reflection;
using DigitalMuseums.Core.Domain.Models.Adjacent;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Domain.Models.Location;
using DigitalMuseums.Core.Domain.Models.Order;
using DigitalMuseums.Core.Domain.Models.Secondary;
using Microsoft.EntityFrameworkCore;

namespace DigitalMuseums.Data.DbContext
{
    /// <summary>
    /// The database context.
    /// </summary>
    public class DigitalMuseumsContext : Microsoft.EntityFrameworkCore.DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalMuseumsContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DigitalMuseumsContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
            SeedData();
        }

        /// <summary>
        /// Gets or sets users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets user roles.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets exhibits.
        /// </summary>
        public DbSet<Exhibit> Exhibits { get; set; }

        /// <summary>
        /// Gets or sets exhibitions.
        /// </summary>
        public DbSet<Exhibition> Exhibitions { get; set; }

        /// <summary>
        /// Gets or sets museums.
        /// </summary>
        public DbSet<Museum> Museums { get; set; }

        /// <summary>
        /// Gets or sets souvenirs.
        /// </summary>
        public DbSet<Souvenir> Souvenirs { get; set; }

        /// <summary>
        /// Gets or sets cities.
        /// </summary>
        public DbSet<City> Cities { get; set; }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Gets or sets locations.
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Gets or sets regions.
        /// </summary>
        public DbSet<Region> Regions { get; set; }

        /// <summary>
        /// Gets or sets orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        
        
        public DbSet<SouvenirOrderDetail> SouvenirOrderDetails { get; set; }

        /// <summary>
        /// Gets or sets genres.
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets images.
        /// </summary>
        public DbSet<Image> Images { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void SeedData()
        {
            if (!Countries.Any())
            {
                var insertCountriesFilePath = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"DigitalMuseums.Api\wwwroot\Scripts\InsertCountries.sql");
                var insertCountriesQuery = File.ReadAllText(insertCountriesFilePath);
                Database.ExecuteSqlRaw(insertCountriesQuery);
            }

            if (!Regions.Any())
            {
                var insertRegionsFilePath = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"DigitalMuseums.Api\wwwroot\Scripts\InsertRegions.sql");
                var insertRegionsQuery = File.ReadAllText(insertRegionsFilePath);
                Database.ExecuteSqlRaw(insertRegionsQuery);
            }

            if (!Cities.Any())
            {
                var insertCitiesFilePath = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"DigitalMuseums.Api\wwwroot\Scripts\InsertCities.sql");
                var insertCitiesQuery = File.ReadAllText(insertCitiesFilePath);
                Database.ExecuteSqlRaw(insertCitiesQuery);
            }
        }
    }
}
