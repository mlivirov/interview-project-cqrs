using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Application.Services;
using TestProject.Domain.Models;

namespace TestProject.Persistance.Database
{
    public partial class TestProjectDbContext : IUnitOfWork, IRepository
    {
        IQueryable<SearchResultEntry> IRepository.SearchResultEntries => SearchResultEntries;

        IQueryable<SearchRequest> IRepository.SearchRequests => SearchRequests;

        async Task IUnitOfWork.AddAsync(IModel model, CancellationToken cancellationToken)
        {
            await AddAsync(model, cancellationToken);
        }

        Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return SaveChangesAsync(cancellationToken);
        }
    }
}