using System.Diagnostics.CodeAnalysis;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Data.DbContext;
using DigitalMuseums.Data.Factories;
using DigitalMuseums.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMuseums.Data.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbDataAccess(
            this IServiceCollection services,
            string dbConnectionStringName)
        {
            services.AddDbContext<DigitalMuseumsContext>(options => options.UseNpgsql(dbConnectionStringName));

            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
