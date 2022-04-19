namespace RedRiftGame.Domain;

public class MatchHandlingException : Exception
{
    public MatchHandlingException(string message) : base(message)
    {
    }
}