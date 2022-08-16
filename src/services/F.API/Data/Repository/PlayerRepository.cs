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
        await _context.Players.AddAsync(player);
    }

    public async Task<IEnumerable<Player>> GetAll()
    {
        return await _context.Players.AsNoTracking().ToListAsync();
    }

    public void Dispose() => _context.Dispose();

    public async Task AddWithRank(Player player)
    {
        await _context.Players.AddAsync(player);
    }

    public async Task<IEnumerable<Player>> GetAllWithRank()
    {
        return await _context.Players
                             .AsNoTracking()
                             .Include(p => p.Ranks)
                             .ToListAsync();
    }

    public async Task AddRank(Rank rank)
    {
        await _context.Ranks.AddAsync(rank);
    }

    public async Task<IEnumerable<Player>> GetAllById(Guid[] ids)
    {
        return await _context.Players
                             .AsNoTracking()
                             .Include(p => p.Ranks)
                             .Where(p => ids.Contains(p.Id))
                             .ToListAsync();
    }
}
