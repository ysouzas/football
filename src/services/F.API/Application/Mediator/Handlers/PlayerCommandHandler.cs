using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Data.Repository.Interfaces;
using F.API.Extensions;
using F.API.Models.DTO.Model;
using F.API.Models.DTO.Queries;
using F.Core.Data;
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
                                                    IRequestHandler<GetRanking, CommandResponse<Ranking>>


{
    private readonly IPlayerRepository _playerRepository;
    private readonly IDealer _dealer;
    private readonly ICache _cache;


    public PlayerCommandHandler(IPlayerRepository playerRepository, IDealer dealer, ICache cache)
    {
        _playerRepository = playerRepository;
        _dealer = dealer;
        _cache = cache;
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
        var playersFromCache = await _cache.GetCacheDataAsync<IEnumerable<PlayerDTO>>("players");

        if (playersFromCache is not null && playersFromCache.Any())
        {
            var orderedPlayersFromCache = playersFromCache.OrderByDescending(p => p.GeneralScore).ToArray();
            return CommandResponse<PlayerDTO[]>.Create(orderedPlayersFromCache);
        }

        var playersFromDatabase = await _playerRepository.GetAllWithRank();
        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerDTO()).OrderByDescending(p => p.GeneralScore).ToArray();

        await _cache.SetCacheDataAsync<IEnumerable<PlayerDTO>>("players", playersDTO, 1440);
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
        var playersToList = new List<Player>();
        var idsToGetOnDatabase = request.Ids.ToList();

        foreach (var id in request.Ids)
        {
            var playerFromCache = await _cache.GetCacheDataAsync<Player>(id.ToString());

            if (playerFromCache is not null)
            {
                playersToList.Add(playerFromCache);

                idsToGetOnDatabase.Remove(id);
            }
        }

        if (idsToGetOnDatabase.Count > 0)
        {
            var playersFromDatabase = await _playerRepository.GetAllById(idsToGetOnDatabase.ToArray());

            foreach (var playerToCache in playersFromDatabase)
            {
                playerToCache.GeneralScore();

                playerToCache.Ranks = new List<Rank>();

                await _cache.SetCacheDataAsync(playerToCache.Id.ToString(), playerToCache, 1440);
            }

            playersToList.AddRange(playersFromDatabase);
        }

        var teams = request.Ids.Length > 14 ? _dealer.SortTeamsRandom(playersToList.ToList(), 3) : _dealer.SortTeams(playersToList.ToList(), 3);

        var players = teams.Values
                           .Select(v => v.Players)
                           .ToArray();

        var playersDTO = players.Select(p => p.ToPlayerDTO())
                                .ToArray();

        var teamsDTO = playersDTO.Select(p => new TeamDTO(p.OrderByDescending(a => a.GeneralScore).ToArray(), p.Sum(c => c.GeneralScore)));

        return CommandResponse<TeamDTO[]>.Create(teamsDTO.OrderBy(t => t.Score).ToArray());
    }

    public async Task<CommandResponse<Ranking>> Handle(GetRanking request, CancellationToken cancellationToken)
    {
        var dateTime = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        var twoMonthAgoDate = dateTime.AddMonths(-1);

        var playersFromDatabase = await _playerRepository.GetAllWithRankWhereByTime(twoMonthAgoDate);

        var playersDTO = playersFromDatabase.Select(p => p.ToPlayerDTO()).ToArray();

        var ranking = new Ranking { Date = twoMonthAgoDate.ToString(), Players = playersDTO };

        return CommandResponse<Ranking>.Create(ranking);

    }
}
