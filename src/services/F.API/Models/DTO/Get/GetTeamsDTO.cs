using F.API.Models.DTO.Model;
using F.Models;

namespace F.API.Models.DTO.Get
{
    public record struct GetTeamsDTO
    {
        public GetTeamsDTO(TeamDTO[] teams)
        {
            Teams = teams;
        }

        public TeamDTO[] Teams { get; set; }
    }
}
