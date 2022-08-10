namespace F.API.Models.DTO;

public record struct AddPlayerDTO
{
    public AddPlayerDTO()
    {

    }
    public AddPlayerDTO(string name)
    {
        Name = name;
    }

    public string Name { get; set; } = string.Empty;
}
