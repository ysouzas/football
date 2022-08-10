using F.API.Models.DTO;
using F.Models;

namespace F.API.Extensions;

public static class PlayerExtensions
{
    public static PlayerDTO ToPlayerDTO(this Player me)
    {
        return new PlayerDTO(me.Id, me.Name);
    }
}
