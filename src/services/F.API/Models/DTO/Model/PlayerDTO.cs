namespace F.API.Models.DTO.Model;

public record struct PlayerDTO
{
    public PlayerDTO(Guid id, string name, decimal score)
    {
        Id = id;
        Name = name;
        Score = score;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Score { get; set; }
}
