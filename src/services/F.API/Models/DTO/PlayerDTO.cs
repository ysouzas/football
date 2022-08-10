namespace F.API.Models.DTO;

public record struct PlayerDTO
{
    public PlayerDTO(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
