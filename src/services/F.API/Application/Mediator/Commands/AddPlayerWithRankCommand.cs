using F.API.Models.DTO.Add;
using F.Core.Messages;
using F.Models;
using FluentValidation;

namespace F.API.Application.Mediator.Commands;

public class AddPlayerWithRankCommand : Command
{
    public string Name { get; set; } = string.Empty;
    public Rank Rank { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new AddPlayerWithRankValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public static AddPlayerWithRankCommand CreateFromDTO(AddPlayerWithRankDTO dto)
    {
        Rank rank = new(dto.Score, dto.DayOfWeek, dto.Date);

        return new AddPlayerWithRankCommand()
        {
            Name = dto.Name,
            Rank = rank
        };
    }

    public class AddPlayerWithRankValidation : AbstractValidator<AddPlayerWithRankCommand>
    {
        public AddPlayerWithRankValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name must be set");

            RuleFor(c => c.Rank)
                .NotNull()
                .WithMessage("Rank must be set");
        }
    }
}