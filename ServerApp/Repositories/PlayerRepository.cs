using Microsoft.EntityFrameworkCore;
using SimpleMcRecords.Data;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly DataContext _context;
    public PlayerRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Player>?> GetPlayersAsync() => await _context.Players.ToListAsync();

    public async Task<Player?> GetPlayerAsync(long id) => await _context.Players.Where(p => p.Id == id).Include(p => p.Scores).Include(p => p.PlayerAdvancements).FirstOrDefaultAsync();

    public async Task<Player?> GetPlayerByNameAsync(string name) => await _context.Players.Where(p => p.Name == name).Include(p => p.Scores).Include(p => p.PlayerAdvancements).FirstOrDefaultAsync();

    public async Task<long> GetIdByName(string name)
    {
        var player = await _context.Players.Where(p => p.Name == name).FirstOrDefaultAsync();
        if (player == null)
            return 0;
        return player.Id;
    }

    public async Task<bool> PutPlayerAsync(Player player)
    {
        _context.Entry(player).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PlayerExists(player.Id))
                return false;
            throw;
        }

        return true;
    }

    public async Task PostPlayerAsync(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeletePlayerAsync(long id)
    {
        var player = await GetPlayerAsync(id);
        if (player == null)
            return false;

        if (player.Scores != null)
            _context.Scores.RemoveRange(player.Scores);

        if (player.PlayerAdvancements != null)
            _context.PlayerAdvancements.RemoveRange(player.PlayerAdvancements);

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();

        return true;
    }

    public bool PlayerExists(long id) => (_context.Players?.Any(p => p.Id == id)).GetValueOrDefault();

    public bool PlayerExistsByName(string name) => (_context.Players?.Any(p => p.Name == name)).GetValueOrDefault();

    public async Task AddAdvancementAsync(long playerId, long advancementId)
    {
        var player = await _context.Players.Where(p => p.Id == playerId).FirstOrDefaultAsync();
        var advancement = await _context.Advancements.Where(p => p.Id == advancementId).FirstOrDefaultAsync();

        if (player == null || advancement == null)
            throw new NullReferenceException();

        var pa = new PlayerAdvancement()
        {
            PlayerId = player.Id,
            AdvancementId = advancement.Id,
            Time = DateTime.UtcNow,
            Player = player,
            Advancement = advancement
        };

        _context.PlayerAdvancements.Add(pa);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAdvancementAsync(long playerId, long advancementId)
    {
        var pa = await _context.PlayerAdvancements.Where(p => p.PlayerId == playerId).Where(p => p.AdvancementId == advancementId).FirstOrDefaultAsync();

        if (pa == null) throw new NullReferenceException();

        _context.PlayerAdvancements.Remove(pa!);
        await _context.SaveChangesAsync();
    }

    public bool AdvancementExists(long playerId, long advancementId) => (_context.PlayerAdvancements?.Any(pa => pa.PlayerId == playerId && pa.AdvancementId == advancementId)).GetValueOrDefault();
}
