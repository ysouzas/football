using F.API.Models.DTO.Model;

namespace F.API.Models.DTO.Get;

public record struct GetAllPlayersWithDetailsDTO
{
    public GetAllPlayersWithDetailsDTO(PlayerWithDetailsDTO[] players)
    {
        Players = players;
    }

    public PlayerWithDetailsDTO[] Players { get; set; }
}