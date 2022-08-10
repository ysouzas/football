using F.Core.Data;
using F.Models;

namespace F.API.Data.Repository.Interfaces;

public interface IPlayerRepository : IRepository<Player>
{
    Task Add(Player player);

    Task<IEnumerable<Player>> GetAll();
}