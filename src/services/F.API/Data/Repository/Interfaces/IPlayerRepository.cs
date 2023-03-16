using F.Core.Data;
using F.Models;

namespace F.API.Data.Repository.Interfaces;

public interface IPlayerRepository : IRepository<Player>
{
    Task Add(Player player);

    Task AddWithRank(Player player);

    Task<IEnumerable<Player>> GetAll();

    Task<IEnumerable<Player>> GetAllById(Guid[] ids);

    Task<IEnumerable<Player>> GetAllWithRank();

    Task AddRank(Rank rank);

    Task<IEnumerable<Player>> GetAllWithRankWhereByTime(DateOnly date);
}