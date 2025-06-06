using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.Data;
using MoviesAPI.Services;

namespace MoviesAPI.Configuration
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SaudeConnection");

            services.AddDbContext<APIContext>(opts => opts.UseSqlServer(connectionString));

            services.AddScoped<SpecialtyService>();
            services.AddScoped<DoctorService>();
            services.AddScoped<UserService>();
            services.AddScoped<AddressService>();
            services.AddScoped<UserInfoService>();
            services.AddScoped<EventService>();
            services.AddScoped<LoginService>();
            services.AddScoped<EmailService>();

            return services;
        }
    }
}
