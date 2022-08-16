using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Data.Repository.Interfaces;
using F.API.Extensions;
using F.API.Models.DTO.Get;
using F.API.Models.DTO.Model;
using F.Core.Messages;
using F.Dealer.Interfaces;
using F.Models;
using FluentValidation.Results;
using MediatR;

namespace F.API.Application.Mediator;

public class PlayerCommandHandler : CommandHandler, IRequestHandler<AddPlayerCommand, ValidationResult>,
                                                    IRequestHandler<AddPlayerWithRankCommand, ValidationResult>,
                                                    IRequestHandler<GetAllPlayersQuery, CommandResponse<GetAllPlayersDTO>>,
                                                    IRequestHandler<GetAllPlayersWithDetailsQuery, CommandResponse<GetAllPlayersWithDetailsDTO>>,
                                                    IRequestHandler<GetTeamCommand, CommandResponse<GetTeamsDTO>>


{
    private readonly IPlayerRepository _playerRepository;
    private readonly IDealer _dealer;


    public PlayerCommandHandler(IPlayerRepository playerRepository, IDealer dealer)
    {
        _playerRepository = playerRepository;
        _dealer = dealer;
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

    public async Task<CommandResponse<GetTeamsDTO>> Handle(GetTeamCommand request, CancellationToken cancellationToken)
    {
        var playersFromDatabase = await _playerRepository.GetAllById(request.Ids);
        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerDTO());

        var rng = new Random();
        var teams = _dealer.SortTeams(playersFromDatabase.ToList(), 3);


        var asd1 = new List<TeamDTO>();

        foreach (var team in teams.Values.OrderBy(a => rng.Next()))
        {
            var aa = team.Players.Select(p => p.ToPlayerDTO()).OrderByDescending(p => p.Score).ToArray();

            var teamDto = new TeamDTO(aa, aa.Sum(c => c.Score));

            asd1.Add(teamDto);
        }

        var getAllPlayersDTO = new GetTeamsDTO(asd1.ToArray());

        return CommandResponse<GetTeamsDTO>.Create(getAllPlayersDTO);
    }
}
