using F.API.Models.DTO.Get;
using F.Core.Messages;

namespace F.API.Application.Mediator.Queries
{
    public class GetTeamCommand : CommandWithResponse<GetTeamsDTO>
    {
        public Guid[] Ids { get; set; }

        public static GetTeamCommand Create(Guid[] ids)
        {
            return new GetTeamCommand()
            {
                Ids = ids
            };
        }
    }
}