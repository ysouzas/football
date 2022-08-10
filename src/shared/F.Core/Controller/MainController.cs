using F.Core.Communication;
using F.Core.Messages;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace F.Core.Controller;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(object? result = null)
    {
        if (ValidOperation())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
    {
        { "Messages", Errors.ToArray() }
    }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AddErrorToStack(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddErrorToStack(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse<T>(CommandResponse<T> commandResponse)
    {
        var errors = commandResponse?.ValidationResult?.Errors ?? null;

        if (errors is null)
            return CustomResponse(commandResponse.Response );


        foreach (var error in errors)
        {
            AddErrorToStack(error.ErrorMessage);
        }

        return CustomResponse(commandResponse.Response);
    }

    protected ActionResult CustomResponse(ResponseResult responseResult)
    {
        ResponseHasErrors(responseResult);

        return CustomResponse();
    }

    protected bool ResponseHasErrors(ResponseResult responseResult)
    {
        if (responseResult == null || !responseResult.Errors.Messages.Any()) return false;

        foreach (var errorMessage in responseResult.Errors.Messages)
        {
            AddErrorToStack(errorMessage);
        }

        return true;
    }

    protected bool ValidOperation()
    {
        return !Errors.Any();
    }

    protected void AddErrorToStack(string error)
    {
        Errors.Add(error);
    }

    protected void CleanErrors()
    {
        Errors.Clear();
    }
}