namespace F.Models;

public class Rank : Entity
{
    public decimal Score { get; set; }
    public DayOfWeek DayOfWeek { get; set; }

    public Guid PlayerId { get; set; }

    // EF Relation
    public Player Player { get; set; }
}
