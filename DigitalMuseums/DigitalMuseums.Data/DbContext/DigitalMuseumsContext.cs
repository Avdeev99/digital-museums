using System.IO;
using System.Linq;
using System.Reflection;
using DigitalMuseums.Core.Domain.Models.Adjacent;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Domain.Models.Location;
using DigitalMuseums.Core.Domain.Models.Order;
using DigitalMuseums.Core.Domain.Models.Secondary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace DigitalMuseums.Data.DbContext
{
    public class DigitalMuseumsContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHostingEnvironment _environment;
        
        public DigitalMuseumsContext(DbContextOptions options, IHostingEnvironment environment) : base(options)
        {
            _environment = environment;
            Database.EnsureCreated();
            SeedData();
        }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Exhibit> Exhibits { get; set; }
        
        public DbSet<Exhibition> Exhibitions { get; set; }
        
        public DbSet<Museum> Museums { get; set; }
        
        public DbSet<Souvenir> Souvenirs { get; set; }
        
        public DbSet<City> Cities { get; set; }
        
        public DbSet<Country> Countries { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Order> Orders { get; set; }
        
        public DbSet<SouvenirOrderDetail> SouvenirOrderDetails { get; set; }

        public DbSet<Genre> Genres { get; set; }

 
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void SeedData()
        {
            if (!Countries.Any())
            {
                var insertCountriesFilePath = Path.Combine(_environment.WebRootPath, @"Scripts\InsertCountries.sql");
                var insertCountriesQuery = File.ReadAllText(insertCountriesFilePath);
                Database.ExecuteSqlRaw(insertCountriesQuery);
            }

            if (!Regions.Any())
            {
                var insertRegionsFilePath = Path.Combine(_environment.WebRootPath, @"Scripts\InsertRegions.sql");
                var insertRegionsQuery = File.ReadAllText(insertRegionsFilePath);
                Database.ExecuteSqlRaw(insertRegionsQuery);
            }

            if (!Cities.Any())
            {
                var insertCitiesFilePath = Path.Combine(_environment.WebRootPath, @"Scripts\InsertCities.sql");
                var insertCitiesQuery = File.ReadAllText(insertCitiesFilePath);
                Database.ExecuteSqlRaw(insertCitiesQuery);
            }
        }
    }
}
