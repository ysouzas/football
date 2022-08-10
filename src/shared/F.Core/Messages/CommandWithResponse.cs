using MediatR;

namespace F.Core.Messages;

public class CommandWithResponse<T> : IRequest<CommandResponse<T>>
{
    public DateTime Timestamp { get; private set; }

    protected CommandWithResponse()
    {
        Timestamp = DateTime.Now;
    }
}