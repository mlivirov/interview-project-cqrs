using System.Collections.Generic;
using MediatR;
using TestProject.Domain.Models;

namespace TestProject.Application.SearchEngine.Queries.GetSearchResults
{
    public sealed class GetSearchResultQuery : IRequest<IEnumerable<SearchResultEntry>>
    {
        public long SearchId { get; set; }
    }
}