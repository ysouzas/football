using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Data.Repository.Interfaces;
using F.API.Extensions;
using F.API.Models.DTO.Model;
using F.Core.Messages;
using F.Dealer.Interfaces;
using F.Models;
using FluentValidation.Results;
using MediatR;

namespace F.API.Application.Mediator;

public class PlayerCommandHandler : CommandHandler, IRequestHandler<AddPlayerCommand, ValidationResult>,
                                                    IRequestHandler<AddPlayerWithRankCommand, ValidationResult>,
                                                    IRequestHandler<GetAllPlayersQuery, CommandResponse<PlayerDTO[]>>,
                                                    IRequestHandler<GetAllPlayersWithDetailsQuery, CommandResponse<PlayerWithDetailsDTO[]>>,
                                                    IRequestHandler<GetTeamCommand, CommandResponse<TeamDTO[]>>,
                                                    IRequestHandler<GetRanking, CommandResponse<PlayerDTO[]>>


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

    public async Task<CommandResponse<PlayerDTO[]>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
    {
        var playersFromDatabase = await _playerRepository.GetAllWithRank();
        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerDTO()).OrderByDescending(p => p.GeneralScore).ToArray();

        return CommandResponse<PlayerDTO[]>.Create(playersDTO);
    }

    public async Task<ValidationResult> Handle(AddPlayerWithRankCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        Player player = new(request.Name);
        player.AddRank(request.Rank);
        await _playerRepository.AddWithRank(player);

        return await PersistData(_playerRepository.UnitOfWork);
    }

    public async Task<CommandResponse<PlayerWithDetailsDTO[]>> Handle(GetAllPlayersWithDetailsQuery request, CancellationToken cancellationToken)
    {
        var playersFromDatabase = await _playerRepository.GetAllWithRank();
        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerWithDetailsDTO()).ToArray();

        return CommandResponse<PlayerWithDetailsDTO[]>.Create(playersDTO);
    }

    public async Task<CommandResponse<TeamDTO[]>> Handle(GetTeamCommand request, CancellationToken cancellationToken)
    {
        var playersFromDatabase = await _playerRepository.GetAllById(request.Ids);

        var teams = request.Ids.Length > 14 ? _dealer.SortTeamsRandom(playersFromDatabase.ToList(), 3) : _dealer.SortTeams(playersFromDatabase.ToList(), 3);

        var players = teams.Values
                           .Select(v => v.Players)
                           .ToArray();

        var playersDTO = players.Select(p => p.ToPlayerDTO())
                                .ToArray();

        var teamsDTO = playersDTO.Select(p => new TeamDTO(p.OrderByDescending(a => a.GeneralScore).ToArray(), p.Sum(c => c.GeneralScore)));

        return CommandResponse<TeamDTO[]>.Create(teamsDTO.OrderBy(t => t.Score).ToArray());
    }

    public async Task<CommandResponse<PlayerDTO[]>> Handle(GetRanking request, CancellationToken cancellationToken)
    {
        var dateTime = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        var threeMonthAgoDate = dateTime.AddMonths(-2);

        var playersFromDatabase = await _playerRepository.GetAllWithRankWhereByTime(threeMonthAgoDate);

        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerDTO()).ToArray();

        return CommandResponse<PlayerDTO[]>.Create(playersDTO);

    }
}
