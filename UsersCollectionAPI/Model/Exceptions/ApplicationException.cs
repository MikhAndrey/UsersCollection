namespace UsersCollectionAPI.Model.Exceptions;

public class ApplicationException : Exception
{
    public int ExceptionId { get; private set; }
    
    protected ApplicationException(string message, int id) : base(message)
    {
        ExceptionId = id;
    }
}
