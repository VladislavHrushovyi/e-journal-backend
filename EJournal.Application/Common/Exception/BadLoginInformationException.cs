namespace EJournal.Application.Common.Exception;

public class BadLoginInformationException : System.Exception
{
    public BadLoginInformationException(string message) : base(message)
    {
        
    }
}