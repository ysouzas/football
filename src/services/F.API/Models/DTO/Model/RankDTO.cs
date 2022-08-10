namespace F.API.Models.DTO.Model;

public record struct RankDTO
{
    public RankDTO(decimal score, DayOfWeek dayOfWeek, DateTime date)
    {
        Score = score;
        DayOfWeek = dayOfWeek;
        Date = date;
    }

    public decimal Score { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public DateTime Date { get; set; }
}
