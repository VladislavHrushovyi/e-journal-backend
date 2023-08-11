namespace EJournal.Application.Common.Exception;

public class BadRequestValidatorsException : System.Exception
{
    private string Errors;
    public BadRequestValidatorsException(string error) : base(error)
    {
        Errors = error;
    }

    public BadRequestValidatorsException(string[] errors) : this(string.Join("\n", errors))
    {
        
    }
}