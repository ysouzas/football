using System.Text.Json;
using F.API.Models.DTO.Model;
using F.API.Models.TableStorage;
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
        };
    }

    public static PlayerDTO[] ToPlayerDTO(this List<Player> me)
    {
        return me.Select(p => p.ToPlayerDTO()).ToArray();
    }


    public static PlayerTableStorageEntity ToPlayerTableStorageEntity(this PlayerDTO me)
    {
        return new PlayerTableStorageEntity
        (
            "FOOTBALL",
            me.Id.ToString(),
            ((double)me.GeneralScore),
            me.Name,
            ""
            );
    }

    public static PlayerTableStorageEntity[] ToPlayerTableStorageEntity(this IList<PlayerDTO> me)
    {
        return me.Select(p => p.ToPlayerTableStorageEntity()).ToArray();
    }

    public static PlayerTableStorageEntity ToPlayerTableStorageEntity(this PlayerWithDetailsDTO me)
    {
        return new PlayerTableStorageEntity
        (
            "FOOTBALL",
            me.Id.ToString(),
            ((double)me.GeneralScore),
            me.Name,
            JsonSerializer.Serialize(me.Ranks)
            );
    }

    public static PlayerTableStorageEntity[] ToPlayerTableStorageEntity(this IList<PlayerWithDetailsDTO> me)
    {
        return me.Select(p => p.ToPlayerTableStorageEntity()).ToArray();
    }

    public static PlayerWithDetailsDTO ToPlayerWithDetailsDTO(this Player me)
    {
        var ranksDTO = me.Ranks.Select(r => r.ToRankDTO()).ToArray();

        return new PlayerWithDetailsDTO(me.Id, me.Name, ranksDTO, me.GeneralScore());
    }
}
