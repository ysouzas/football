using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F.Models;

public class Player : Entity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // EF Relation
    public ICollection<Rank> Ranks { get; set; }

    [NotMapped]
    public decimal? GeneralRank { get; set; } = null;

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
        if (GeneralRank is null)
        {
            GeneralRank = this.BaseScoreCalculation(this.Ranks);

            return GeneralRank.Value;
        };

        return GeneralRank.Value;
    }

    private decimal BaseScoreCalculation(ICollection<Rank> rank)
    {
        if (rank.Count == 0)
            return 0;

        var dateTime = DateTime.Now;
        var oneMonthAgoDate = dateTime.AddMonths(-1);
        var hasTwo = rank.Where(r => r.Date >= oneMonthAgoDate).Any(r => r.DayOfWeek == DayOfWeek.Monday) && rank.Where(r => r.Date >= oneMonthAgoDate).Any(r => r.DayOfWeek == DayOfWeek.Wednesday);
        var ranks = rank.Where(r => r.Date >= oneMonthAgoDate).OrderBy(c => c.Date).ToList();
        var count = ranks.Count;

        if (count >= 7 && hasTwo)
            return Math.Round(ranks.Sum(r => r.Score) / count, 2);


        var oneAndHalfMonthAgoDate = oneMonthAgoDate.AddDays(-14);
        hasTwo = rank.Where(r => r.Date >= oneAndHalfMonthAgoDate).Any(r => r.DayOfWeek == DayOfWeek.Monday) && rank.Where(r => r.Date >= oneAndHalfMonthAgoDate).Any(r => r.DayOfWeek == DayOfWeek.Wednesday);
        ranks = rank.Where(r => r.Date >= oneAndHalfMonthAgoDate).OrderBy(c => c.Date).ToList();
        count = ranks.Count;

        if (count >= 9 && hasTwo)
            return Math.Round(ranks.Sum(r => r.Score) / count, 2);


        var twoMonthsAgoDate = dateTime.AddMonths(-2);

        ranks = rank.Where(r => r.Date >= twoMonthsAgoDate).OrderBy(c => c.Date).ToList();
        count = ranks.Count;

        if (count >= 7)
            return Math.Round(ranks.Sum(r => r.Score) / count, 2);

        var threeMonthsAgoDate = dateTime.AddMonths(-3);

        ranks = rank.Where(r => r.Date >= threeMonthsAgoDate).OrderBy(c => c.Date).ToList();
        count = ranks.Count;

        if (count > 9)
            return Math.Round(ranks.Sum(r => r.Score) / count, 2);

        return Math.Round(rank.Sum(r => r.Score) / rank.Count, 2);
    }
}