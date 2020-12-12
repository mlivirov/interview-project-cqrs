using System.Linq;
using TestProject.Domain.Models;

namespace TestProject.Application.Services
{
    public interface IRepository
    {
        IQueryable<SearchRequest> SearchRequests { get; }

        IQueryable<SearchResultEntry> SearchResultEntries { get; }
    }
}