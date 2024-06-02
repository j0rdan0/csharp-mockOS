using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Localization;

public sealed class ErrorMessage : IErrorMessage
{

    private readonly IStringLocalizer<ErrorMessage> _localizer;

    public ErrorMessage(IStringLocalizer<ErrorMessage> localizer)
    {
        _localizer = localizer;
    }
    [return: NotNullIfNotNull(nameof(_localizer))]
    public string? GetMessage(string errorCode) => _localizer[errorCode];

}

public interface IErrorMessage
{
    string? GetMessage(string errorCode);
}