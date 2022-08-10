namespace F.API.Models.DTO.Model;

public record struct PlayerWithDetailsDTO
{
    public PlayerWithDetailsDTO(Guid id, string name, RankDTO[] ranks)
    {
        Id = id;
        Name = name;
        Ranks = ranks;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public RankDTO[] Ranks { get; set; }
}
