namespace TestProject.Domain.Exceptions
{
    public class LogicFailureException : DomainException
    {
        public LogicFailureException(string message) : base(message)
        {
        }
    }
}