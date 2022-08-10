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
}