namespace TestProject.Domain.Models
{
    public class SearchResultEntry : IModel
    {
        public long Id { get; set; }
        
        public string Title { get; set; }

        public string Link { get; set; }

        public long? SearchRequestId { get; set; }
        
        public string SearchEngine { get; set; }
        
        public SearchRequest SearchRequest { get; set; }
    }
}