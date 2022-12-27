using F.API.Application.Mediator.Commands;
using F.API.Data.Repository.Interfaces;
using F.Core.Messages;
using F.Models;
using FluentValidation.Results;
using MediatR;

public class RankCommandHandler : CommandHandler,
                                  IRequestHandler<AddRankCommand, ValidationResult>,
                                  IRequestHandler<AddRanksCommand, ValidationResult>
{
    private readonly IRankRepository _rankRepository;

    public RankCommandHandler(IRankRepository rankRepository)
    {
        _rankRepository = rankRepository;
    }

    public async Task<ValidationResult> Handle(AddRankCommand request, CancellationToken cancellationToken)
    {
        var rank = new Rank(request.Score, request.DayOfWeek, request.Date, request.PlayerId);

        await _rankRepository.Add(rank);

        return await PersistData(_rankRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AddRanksCommand request, CancellationToken cancellationToken)
    {
        await _rankRepository.AddRanks(request.Ranks);

        return await PersistData(_rankRepository.UnitOfWork);
    }
}
