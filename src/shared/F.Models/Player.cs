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

    public decimal Score()
    {
        if (Ranks.Count > 0)
            return Math.Round(Ranks.Sum(r => r.Score) / Ranks.Count, 2);

        return 0;
    }
}