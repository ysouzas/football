using F.API.Data.Repository.Interfaces;
using F.Core.Data;
using F.Models;
using Microsoft.EntityFrameworkCore;

namespace F.API.Data.Repository;

public class PlayerRepository : IPlayerRepository
{
    private readonly ApiContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public PlayerRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task Add(Player player)
    {
        _context.Players.Add(player);
    }

    public async Task<IEnumerable<Player>> GetAll()
    {
        return await _context.Players.AsNoTracking().ToListAsync();
    }

    public void Dispose() => _context.Dispose();
}
