using F.API.Models.DTO.Model;

namespace F.API.Models.DTO.Queries;

public record struct Ranking
{
    public PlayerDTO[] Players { get; set; }

    public DateTime Date { get; set; }
}
