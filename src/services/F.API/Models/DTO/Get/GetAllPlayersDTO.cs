using F.API.Models.DTO.Model;

namespace F.API.Models.DTO.Get;

public record struct GetAllPlayersDTO
{
    public GetAllPlayersDTO(PlayerDTO[] players)
    {
        Players = players;
    }
    public readonly PlayerDTO[] Players { get; }
}
