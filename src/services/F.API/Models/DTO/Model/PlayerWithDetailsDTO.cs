namespace F.API.Models.DTO.Model;

public record struct PlayerWithDetailsDTO
{
    public PlayerWithDetailsDTO(Guid id, string name, RankDTO[] ranks, decimal generalScore)
    {
        Id = id;
        Name = name;
        Ranks = ranks;
        GeneralScore = generalScore;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal GeneralScore { get; set; } = decimal.Zero;

    public RankDTO[] Ranks { get; set; }
}
