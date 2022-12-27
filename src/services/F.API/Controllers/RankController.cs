using F.API.Application.Mediator.Commands;
using F.API.Models.DTO.Add;
using F.Core.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace F.API.Controllers;

[Route("api/[controller]")]
public class RankController : MainController
{
    private readonly IMediator _mediator;

    public RankController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddRankDTO addRankDTO)
    {
        var command = AddRankCommand.CreateFromDTO(addRankDTO);
        return CustomResponse(await _mediator.Send(command));
    }

    [HttpPost("AddListOfRanks")]
    public async Task<IActionResult> AddRanks(AddRankDTO[] addRanksDTO)
    {
        var command = AddRanksCommand.CreateFromDTO(addRanksDTO);
        return CustomResponse(await _mediator.Send(command));
    }
}
