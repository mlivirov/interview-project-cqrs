namespace TestProject.Domain.Exceptions
{
    public class ResultNotReadyException : DomainException
    {
        public ResultNotReadyException(string message) : base(message)
        {
        }
    }
}