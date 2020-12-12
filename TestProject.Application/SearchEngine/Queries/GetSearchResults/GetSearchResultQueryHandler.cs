using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestProject.Application.Services;
using TestProject.Domain.Enums;
using TestProject.Domain.Exceptions;
using TestProject.Domain.Models;

namespace TestProject.Application.SearchEngine.Queries.GetSearchResults
{
    public class GetSearchResultQueryHandler : IRequestHandler<GetSearchResultQuery, IEnumerable<SearchResultEntry>>
    {
        private readonly IRepository _repository;

        public GetSearchResultQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SearchResultEntry>> Handle(GetSearchResultQuery request, CancellationToken cancellationToken)
        {
            var searchRequest = _repository.SearchRequests.FirstOrDefault(t => t.Id == request.SearchId);
            if (searchRequest == null)
            {
                throw new NotFoundException($"Not found, searchId: {request.SearchId}");
            }

            if (searchRequest.Status == SearchRequestStatus.Running)
            {
                throw new ResultNotReadyException($"incomplete search, searchId: {request.SearchId}, search status: {searchRequest.Status}");
            }

            if (searchRequest.Status == SearchRequestStatus.Failed)
            {
                throw new LogicFailureException($"failed search, searchId: {request.SearchId}, search status: {searchRequest.Status}");
            }

            return _repository.SearchResultEntries
                .Where(t => t.SearchRequestId == searchRequest.Id)
                .ToList();
        }
    }
}