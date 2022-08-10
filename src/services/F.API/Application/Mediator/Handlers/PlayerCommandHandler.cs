
using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Data.Repository.Interfaces;
using F.API.Extensions;
using F.API.Models.DTO;
using F.Core.Messages;
using F.Models;
using FluentValidation.Results;
using MediatR;

namespace F.API.Application.Mediator;

public class PlayerCommandHandler : CommandHandler, IRequestHandler<AddPlayerCommand, ValidationResult>, IRequestHandler<GetAllPlayersQuery, CommandResponse<GetAllPlayersDTO>>
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerCommandHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<ValidationResult> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        Player player = new(request.Name);

        await _playerRepository.Add(player);

        return await PersistData(_playerRepository.UnitOfWork);
    }

    public async Task<CommandResponse<GetAllPlayersDTO>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
    {
        var playersFromDatabase = await _playerRepository.GetAll();
        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerDTO());

        var getAllPlayersDTO = new GetAllPlayersDTO(playersDTO.ToArray());

        return CommandResponse<GetAllPlayersDTO>.Create(getAllPlayersDTO);
    }
}
