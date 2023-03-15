using F.API.Models.DTO.Add;
using F.Core.Messages;
using F.Models;
using FluentValidation;

namespace F.API.Application.Mediator.Commands;

public class AddRanksCommand : Command
{
    public Rank[] Ranks { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new AddRanksValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public static AddRanksCommand CreateFromDTO(AddRankDTO[] dto)
    {
        return new AddRanksCommand()
        {
            Ranks = dto.Select(r => new Rank(r.Score, r.DayOfWeek, r.Date, r.PlayerId)).ToArray(),
        };
    }

    public class AddRanksValidation : AbstractValidator<AddRanksCommand>
    {
        public AddRanksValidation()
        {
            RuleFor(c => c.Ranks)
                .NotEmpty()
                .WithMessage("Ranks must be set");
        }
    }
}