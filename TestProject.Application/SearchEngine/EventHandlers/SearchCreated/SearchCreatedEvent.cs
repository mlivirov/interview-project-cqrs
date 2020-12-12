using MediatR;
using TestProject.Domain.Models;

namespace TestProject.Application.SearchEngine.EventHandlers.SearchCreated
{
    public class SearchCreatedEvent : INotification
    {
        public long SearchRequestId { get; set; }
    }
}