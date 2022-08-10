namespace F.Models;

public class Rank : Entity
{
    public Rank(decimal score, DayOfWeek dayOfWeek, DateTime date)
    {
        Score = score;
        DayOfWeek = dayOfWeek;
        Date = date;
    }

    public Rank(decimal score, DayOfWeek dayOfWeek, DateTime date, Guid playerId)
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

    // EF Relation
    public Player Player { get; set; }
}
