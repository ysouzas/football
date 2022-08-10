namespace F.API.Models.DTO.Add;

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
