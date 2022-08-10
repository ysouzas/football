using F.API.Models.DTO.Model;
using F.Models;

namespace F.API.Extensions
{
    public static class RankExtensions
    {
        public static RankDTO ToRankDTO(this Rank me)
        {
            return new RankDTO(me.Score, me.DayOfWeek, me.Date);
        }
    }
}
