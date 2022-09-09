using F.API.Models.DTO.Model;
using F.Models;

namespace F.API.Extensions;

public static class PlayerExtensions
{
    public static PlayerDTO ToPlayerDTO(this Player me)
    {
        return new PlayerDTO
        {
            Id = me.Id,
            Name = me.Name,
            GeneralScore = me.GeneralScore(),
            MondayScore = me.DayOfWeekScoreCalculation(DayOfWeek.Monday),
            WednesdayScore = me.DayOfWeekScoreCalculation(DayOfWeek.Wednesday)
        };
    }

    public static PlayerDTO[] ToPlayerDTO(this List<Player> me)
    {
        return me.Select(p => p.ToPlayerDTO()).ToArray();
    }

    public static PlayerWithDetailsDTO ToPlayerWithDetailsDTO(this Player me)
    {
        var ranksDTO = me.Ranks.Select(r => r.ToRankDTO()).ToArray();

        return new PlayerWithDetailsDTO(me.Id, me.Name, ranksDTO);
    }
}
