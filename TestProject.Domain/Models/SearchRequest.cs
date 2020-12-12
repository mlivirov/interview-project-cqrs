using System;
using TestProject.Domain.Enums;

namespace TestProject.Domain.Models
{
    public class SearchRequest : IModel
    {
        public long Id { get; set; }
        
        public string SearchPhrase { get; set; }
        
        public DateTime Created { get; set; }

        public SearchRequestStatus Status { get; set; }
        
        public DateTime? StatusUpdated { get; set; }
    }
}