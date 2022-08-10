using F.API.Models.DTO.Add;
using F.Core.Messages;
using FluentValidation;

namespace F.API.Application.Mediator.Commands;

public class AddPlayerCommand : Command
{
    public string Name { get; set; } = string.Empty;

    public override bool IsValid()
    {
        ValidationResult = new AddPlayerValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public static AddPlayerCommand CreateFromDTO(AddPlayerDTO dto)
    {
        return new AddPlayerCommand()
        {
            Name = dto.Name
        };
    }

    public class AddPlayerValidation : AbstractValidator<AddPlayerCommand>
    {
        public AddPlayerValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name must be set");
        }
    }
}