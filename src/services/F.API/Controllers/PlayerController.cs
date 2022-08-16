using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Models.DTO.Add;
using F.Core.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace F.API.Controllers;

[Route("api/[controller]")]
public class PlayerController : MainController
{
    private readonly IMediator _mediator;

    public PlayerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var command = new GetAllPlayersQuery();
        return CustomResponse(await _mediator.Send(command));
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddPlayerDTO addPlayerDTO)
    {
        var command = AddPlayerCommand.CreateFromDTO(addPlayerDTO);
        return CustomResponse(await _mediator.Send(command));
    }


    [HttpGet("GetAllWithRank")]
    public async Task<IActionResult> GetAllWithRank()
    {
        var command = new GetAllPlayersWithDetailsQuery();
        return CustomResponse(await _mediator.Send(command));

    }

    [HttpPost("AddPlayerWithRank")]
    public async Task<IActionResult> AddPlayerWithRankAsync(AddPlayerWithRankDTO addPlayerWithRankDTO)
    {
        var command = AddPlayerWithRankCommand.CreateFromDTO(addPlayerWithRankDTO);
        return CustomResponse(await _mediator.Send(command));
    }

    [HttpPost("AddRank")]
    public async Task<IActionResult> AddRankAsync(AddRankDTO addRankDTO)
    {
        var command = AddRankCommand.CreateFromDTO(addRankDTO);
        return CustomResponse(await _mediator.Send(command));
    }

    [HttpPost("teams")]
    public async Task<IActionResult> Teams(Guid[] ids)
    {
        var command = GetTeamCommand.Create(ids);
        return CustomResponse(await _mediator.Send(command));
    }
}
