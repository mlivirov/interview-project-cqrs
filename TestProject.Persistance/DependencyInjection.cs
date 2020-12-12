
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Application.Services;
using TestProject.Persistance.Database;

namespace TestProject.Persistance
{
    public static class DependencyInjection
    {
        public static void AddPersistance(this IServiceCollection services)
        {
            services.AddDbContext<TestProjectDbContext>(t =>
                    {
                        t.UseSqlite("Data Source=TestProject.db");
                    });

            services.AddTransient<IUnitOfWork>(t => t.GetService<TestProjectDbContext>());
            services.AddTransient<IRepository>(t => t.GetService<TestProjectDbContext>());
        }
    }
}