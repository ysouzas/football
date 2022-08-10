using F.Core.Messages;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F.Core.Mediator;

public interface IMediatorHandler
{
    Task<ValidationResult> SendCommand<T>(T comando) where T : Command;
    Task<CommandResponse<Y>> SendCommand<T, Y>(T command) where T : CommandWithResponse<Y>;
}

