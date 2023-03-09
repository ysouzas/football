namespace F.API.Models.DTO.Model;

public record struct PlayerDTO
{
    public PlayerDTO(Guid id, string name, decimal generalScore)
    {
        Id = id;
        Name = name;
        GeneralScore = generalScore;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal GeneralScore { get; set; } = decimal.Zero;
}
