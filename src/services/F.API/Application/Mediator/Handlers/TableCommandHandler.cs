using F.API.Application.Mediator.Commands;
using F.API.Application.Mediator.Queries;
using F.API.Data.Repository.Interfaces;
using F.API.Data.Storage.Interfaces;
using F.API.Extensions;
using F.Core.Messages;
using F.Models;
using FluentValidation.Results;
using MediatR;

namespace F.API.Application.Mediator.Handlers
{
    public class TableCommandHandler : CommandHandler,
                                  IRequestHandler<GetAllPlayersToTableQuery, ValidationResult>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMediator _mediator;
        private readonly IPlayerTableStorage _playerTableStorage;

        public TableCommandHandler(IPlayerRepository playerRepository, IMediator mediator, IPlayerTableStorage playerTableStorage)
        {
            _playerRepository = playerRepository;
            _mediator = mediator;
            _playerTableStorage = playerTableStorage;
        }


        public async Task<ValidationResult> Handle(GetAllPlayersToTableQuery request, CancellationToken cancellationToken)
        {
            var command = new GetAllPlayersWithDetailsQuery();
            var result = await _mediator.Send(command, cancellationToken);

            foreach (var playerEntity in result.Response?.ToPlayerTableStorageEntity())
            {
                _playerTableStorage.InsertOrReplace(playerEntity);

            }

            return ValidationResult;
        }
    }
}