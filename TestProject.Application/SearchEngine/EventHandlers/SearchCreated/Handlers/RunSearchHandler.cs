using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Application.Services;
using TestProject.Domain.Enums;

namespace TestProject.Application.SearchEngine.EventHandlers.SearchCreated.Handlers
{
    public class RunSearchHandler : INotificationHandler<SearchCreatedEvent>
    {
        private readonly IEnumerable<ISearchService> _searchServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository _repository;

        public RunSearchHandler(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IRepository repository)
        {
            _searchServices = serviceProvider.GetServices<ISearchService>();
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task Handle(SearchCreatedEvent notification, CancellationToken cancellationToken)
        {
            var searchRequest = _repository.SearchRequests.First(t => t.Id == notification.SearchRequestId);

            var searches = _searchServices.Select(async t =>
            {
                var resultEntries = await t.SearchAsync(searchRequest.SearchPhrase, cancellationToken);
                foreach (var resultEntry in resultEntries)
                {
                    resultEntry.SearchRequestId = searchRequest.Id;
                    await _unitOfWork.AddAsync(resultEntry, cancellationToken);
                }
            });

            try
            {
                await Task.WhenAll(searches);
                searchRequest.Status = SearchRequestStatus.Finished;
                searchRequest.StatusUpdated = DateTime.UtcNow;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                searchRequest.Status = SearchRequestStatus.Failed;
                searchRequest.StatusUpdated = DateTime.UtcNow;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}