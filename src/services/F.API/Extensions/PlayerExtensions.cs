using F.API.Models.DTO.Model;
using F.Models;

namespace F.API.Extensions;

public static class PlayerExtensions
{
    public static PlayerDTO ToPlayerDTO(this Player me)
    {
        decimal rank = Math.Round(me.Ranks.Sum(r => r.Score) / me.Ranks.Count, 2);

        return new PlayerDTO(me.Id, me.Name, rank);
    }

    public static PlayerWithDetailsDTO ToPlayerWithDetailsDTO(this Player me)
    {
        var ranksDTO = me.Ranks.Select(r => r.ToRankDTO()).ToArray();

        return new PlayerWithDetailsDTO(me.Id, me.Name, ranksDTO);
    }
}
