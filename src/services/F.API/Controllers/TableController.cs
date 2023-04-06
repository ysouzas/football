using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Models.DTO.Add;
using F.Core.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace F.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TableController : MainController
{
    private readonly IMediator _mediator;

    public TableController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Populate()
    {
        var command = new GetAllPlayersToTableQuery();
        return CustomResponse(await _mediator.Send(command));
    }
}


