using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Data.Repository.Interfaces;
using F.API.Extensions;
using F.API.Models.DTO.Get;
using F.Core.Messages;
using F.Models;
using FluentValidation.Results;
using MediatR;

namespace F.API.Application.Mediator;

public class PlayerCommandHandler : CommandHandler, IRequestHandler<AddPlayerCommand, ValidationResult>,
                                                    IRequestHandler<AddPlayerWithRankCommand, ValidationResult>,
                                                    IRequestHandler<GetAllPlayersQuery, CommandResponse<GetAllPlayersDTO>>,
                                                    IRequestHandler<GetAllPlayersWithDetailsQuery, CommandResponse<GetAllPlayersWithDetailsDTO>>

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
        var playersFromDatabase = await _playerRepository.GetAllWithRank();
        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerDTO()).OrderByDescending(p => p.Score);

        var getAllPlayersDTO = new GetAllPlayersDTO(playersDTO.ToArray());

        return CommandResponse<GetAllPlayersDTO>.Create(getAllPlayersDTO);
    }

    public async Task<ValidationResult> Handle(AddPlayerWithRankCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        Player player = new(request.Name);
        player.AddRank(request.Rank);
        await _playerRepository.AddWithRank(player);

        return await PersistData(_playerRepository.UnitOfWork);
    }

    public async Task<CommandResponse<GetAllPlayersWithDetailsDTO>> Handle(GetAllPlayersWithDetailsQuery request, CancellationToken cancellationToken)
    {
        var playersFromDatabase = await _playerRepository.GetAllWithRank();
        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerWithDetailsDTO());

        var getAllPlayersDTO = new GetAllPlayersWithDetailsDTO(playersDTO.ToArray());

        return CommandResponse<GetAllPlayersWithDetailsDTO>.Create(getAllPlayersDTO);
    }
}
