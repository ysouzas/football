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

        var dateTime = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        var twoMonthsAgoDate = dateTime.AddMonths(-2);

        var ranks = rank.Where(r => r.DateOnlyGeneral() >= twoMonthsAgoDate).OrderBy(c => c.Date).ToList();
        var count = ranks.Count;

        if (count >= 8)
            return Math.Round(ranks.Sum(r => r.Score) / count, 2);

        ranks = rank.Where(r => r.DateOnlyGeneral().Year >= twoMonthsAgoDate.Year).OrderBy(c => c.Date).ToList();

        count = ranks.Count;

        if (count >= 10)
            return Math.Round(ranks.Sum(r => r.Score) / count, 2);

        return Math.Round(rank.Sum(r => r.Score) / rank.Count, 2);
    }
}