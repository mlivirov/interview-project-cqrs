using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Application.SearchEngine.Commands.CreateSearch;
using TestProject.Application.SearchEngine.Queries.GetSearchResults;
using TestProject.Domain.Models;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<long> Post([FromBody] CreateSearchCommand createSearchCommand, CancellationToken cancellationToken)
        {
            return _mediator.Send(createSearchCommand, cancellationToken);
        }

        [HttpGet]
        public Task<IEnumerable<SearchResultEntry>> Get([FromQuery] long searchId)
        {
            return _mediator.Send(new GetSearchResultQuery
            {
                SearchId = searchId
            });
        }
    }
}