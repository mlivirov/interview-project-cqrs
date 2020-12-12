using Microsoft.EntityFrameworkCore;
using TestProject.Domain.Models;

namespace TestProject.Persistance.Database
{
    public partial class TestProjectDbContext : DbContext
    {
        public TestProjectDbContext()
        {
        }

        public TestProjectDbContext(DbContextOptions<TestProjectDbContext> options): base(options)
        {
        }

        public DbSet<SearchRequest> SearchRequests { get; set; }

        public DbSet<SearchResultEntry> SearchResultEntries { get; set; }
    }
}