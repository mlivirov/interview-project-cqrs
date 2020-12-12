using MediatR;

namespace TestProject.Application.SearchEngine.Commands.CreateSearch
{
    public sealed class CreateSearchCommand : IRequest<long>
    {
        public string SearchPhrase { get; set; }
    }
}