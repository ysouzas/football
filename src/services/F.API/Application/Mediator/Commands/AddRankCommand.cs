using F.API.Models.DTO.Add;
using F.Core.Messages;
using FluentValidation;

namespace F.API.Application.Mediator.Commands;


public class AddRankCommand : Command
{
    public decimal Score { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public DateTime Date { get; set; }
    public Guid PlayerId { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new AddRankValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public static AddRankCommand CreateFromDTO(AddRankDTO dto)
    {
        return new AddRankCommand()
        {
            Score = dto.Score,
            DayOfWeek = dto.DayOfWeek,
            Date = dto.Date,
            PlayerId = dto.PlayerId
        };
    }

    public class AddRankValidation : AbstractValidator<AddRankCommand>
    {
        public AddRankValidation()
        {
            RuleFor(c => c.Score)
                .NotEmpty()
                .WithMessage("Score must be set");

            RuleFor(c => c.DayOfWeek)
                .NotEmpty()
                .WithMessage("DayOfWeek must be set");

            RuleFor(c => c.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId must be set");
        }
    }
}