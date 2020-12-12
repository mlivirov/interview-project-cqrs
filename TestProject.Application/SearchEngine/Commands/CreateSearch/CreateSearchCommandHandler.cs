using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestProject.Application.SearchEngine.EventHandlers.SearchCreated;
using TestProject.Application.Services;
using TestProject.Domain.Enums;
using TestProject.Domain.Models;

namespace TestProject.Application.SearchEngine.Commands.CreateSearch
{
    public class CreateSearchCommandHandler : IRequestHandler<CreateSearchCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreateSearchCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<long> Handle(CreateSearchCommand request, CancellationToken cancellationToken)
        {
            var searchRequest = new SearchRequest
            {
                SearchPhrase = request.SearchPhrase,
                Created = DateTime.UtcNow,
                Status = SearchRequestStatus.Running,
            };

            await _unitOfWork.AddAsync(searchRequest, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new SearchCreatedEvent
            {
                SearchRequestId = searchRequest.Id
            }, cancellationToken);

            return searchRequest.Id;
        }
    }
}