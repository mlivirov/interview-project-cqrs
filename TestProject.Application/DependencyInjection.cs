using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Application.SearchEngine.Commands.CreateSearch;
using TestProject.Application.Services;

namespace TestProject.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.Using<CustomMediator>();
            },
            typeof(CreateSearchCommand).Assembly);
        }
    }
}