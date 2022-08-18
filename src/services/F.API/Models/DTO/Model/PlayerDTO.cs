namespace F.API.Models.DTO.Model;

public record struct PlayerDTO
{
    public PlayerDTO(Guid id, string name, decimal generalScore, decimal mondayScore, decimal wednesdayScore, decimal momentScore)
    {
        Id = id;
        Name = name;
        GeneralScore = generalScore;
        MondayScore = mondayScore;
        WednesdayScore = wednesdayScore;
        MomentScore = momentScore;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal GeneralScore { get; set; } = decimal.Zero;
    public decimal MondayScore { get; set; } = decimal.Zero;
    public decimal WednesdayScore { get; set; } = decimal.Zero;
    public decimal MomentScore { get; set; } = decimal.Zero;

}
