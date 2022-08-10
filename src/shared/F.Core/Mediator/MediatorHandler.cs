using F.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace F.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }

    public async Task<CommandResponse<Y>> SendCommand<T, Y>(T command) where T : CommandWithResponse<Y>
    {
        return await _mediator.Send(command);
    }
}