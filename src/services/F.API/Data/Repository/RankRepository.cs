using F.API.Data.Repository.Interfaces;
using F.Core.Data;
using F.Models;

namespace F.API.Data.Repository;

public class RankRepository : IRankRepository
{
    private readonly ApiContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public RankRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task Add(Rank rank)
    {
        await _context.Ranks.AddAsync(rank);
    }

    public async Task AddRanks(Rank[] ranks)
    {
        await _context.Ranks.AddRangeAsync(ranks);
    }

    public void Dispose() => _context.Dispose();


}
