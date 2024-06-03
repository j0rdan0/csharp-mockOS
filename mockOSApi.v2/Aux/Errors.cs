namespace mockOSApi.Errors;

public class ErrorMessages
{

    public Dictionary<string, string> ErrorMessage { get; set; } = new Dictionary<string, string>();

    public ErrorMessages()
    {
        ErrorMessage["PATH_ERROR"] = "The path does not exists or could not be found";
    }
}