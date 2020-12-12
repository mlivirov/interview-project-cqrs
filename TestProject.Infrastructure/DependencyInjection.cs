using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Application.Services;
using TestProject.Infrastructure.Services;

namespace TestProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISearchService, GoogleSearchService>();
            services.AddTransient<ISearchService, BingSearchService>();

            services.AddOptions<SearchServiceOptions>().Configure(c =>
            {
                configuration.GetSection("SearchServices").Bind(c);
            });
        }
    }
}