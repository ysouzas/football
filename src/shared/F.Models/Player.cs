using System.ComponentModel.DataAnnotations;

namespace F.Models;

public class Player : Entity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // EF Relation
    public ICollection<Rank> Ranks { get; set; }

    public Player()
    {

    }

    public Player(string name)
    {
        Name = name;
        Ranks = new List<Rank>();
    }

    public void AddRank(Rank rank)
    {
        Ranks.Add(rank);
    }

    public decimal GeneralScore()
    {
        return this.BaseScoreCalculation(this.Ranks);
    }

    public decimal DayOfWeekScoreCalculation(DayOfWeek dayOfWeek)
    {
        return this.BaseScoreCalculation(this.Ranks.Where(r => r.DayOfWeek == dayOfWeek).ToList());
    }

    private decimal BaseScoreCalculation(ICollection<Rank> rank)
    {
        if (rank.Count == 0)
            return 0;

        if (rank.Count is >= 12)
        {
            return Math.Round(rank.OrderBy(r => r.Date).TakeLast(12).Sum(r => r.Score) / 12, 2);
        }

        return Math.Round(rank.Sum(r => r.Score) / rank.Count, 2);
    }
}