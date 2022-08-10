using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Models.DTO;
using F.Core.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace F.API.Controllers
{
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

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddPlayerDTO addPlayerDTO)
        {
            var command = AddPlayerCommand.CreateFromDTO(addPlayerDTO);
            return CustomResponse(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
