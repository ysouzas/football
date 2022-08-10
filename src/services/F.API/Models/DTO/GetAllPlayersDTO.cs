namespace F.API.Models.DTO;

public record struct GetAllPlayersDTO
{
    public GetAllPlayersDTO(PlayerDTO[] players)
    {
        Players = players;
    }
    public readonly PlayerDTO[] Players { get; }
}
