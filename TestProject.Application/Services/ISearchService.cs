using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Domain.Models;

namespace TestProject.Application.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchResultEntry>> SearchAsync(string searchPhrase, CancellationToken cancellationToken);
    }
}