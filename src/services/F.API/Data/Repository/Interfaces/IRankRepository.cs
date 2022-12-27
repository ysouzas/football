using F.Core.Data;
using F.Models;

namespace F.API.Data.Repository.Interfaces;

public interface IRankRepository : IRepository<Rank>
{
    Task Add(Rank rank);

    Task AddRanks(Rank[] ranks);
}