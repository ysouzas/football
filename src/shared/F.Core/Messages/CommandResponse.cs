using FluentValidation.Results;

namespace F.Core.Messages;

public class CommandResponse<T>
{
    public CommandResponse(T? response, ValidationResult? validationResult)
    {
        ValidationResult = validationResult;
        Response = response;
    }

    public ValidationResult? ValidationResult { get; set; }
    public T? Response { get; set; }
    public bool IsValid => ValidationResult?.IsValid ?? false;


    public static CommandResponse<T> Create(T? response, ValidationResult? validationResult = null)
    {
        return new CommandResponse<T>(response, validationResult);
    }
}