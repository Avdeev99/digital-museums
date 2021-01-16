using System.Text;
using DigitalMuseums.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DigitalMuseums.Api.Extensions
{
    public static class AuthConfiguratorExtensions
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiAuthOptions>(configuration.GetSection(nameof(ApiAuthOptions)));
            
            var apiAuthOptions = configuration.GetSection(nameof(ApiAuthOptions)).Get<ApiAuthOptions>();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(apiAuthOptions.Secret)),
                        ValidIssuer = apiAuthOptions.Issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}