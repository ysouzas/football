namespace F.API.Models.DTO.Add;

public record struct AddRankDTO
{
    public AddRankDTO(decimal score, DayOfWeek dayOfWeek, DateTime date, Guid playerId)
    {
        Score = score;
        DayOfWeek = dayOfWeek;
        Date = date;
        PlayerId = playerId;
    }

    public decimal Score { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public DateTime Date { get; set; }
    public Guid PlayerId { get; set; }
}
