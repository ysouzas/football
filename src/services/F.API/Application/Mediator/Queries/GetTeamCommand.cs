using F.API.Models.DTO.Model;
using F.Core.Messages;

namespace F.API.Application.Mediator.Queries
{
    public class GetTeamCommand : CommandWithResponse<TeamDTO[]>
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