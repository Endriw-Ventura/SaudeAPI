using Microsoft.Extensions.DependencyInjection;

namespace MoviesAPI.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }
    }
}
