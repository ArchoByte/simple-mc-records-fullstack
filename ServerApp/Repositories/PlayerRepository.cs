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

    public async Task<Player?> GetPlayerAsync(long id) => await _context.Players.Where(p => p.Id == id).Include(s => s.Scores).Include(pa => pa.PlayerAdvancements).FirstOrDefaultAsync();

    public async Task<Player?> GetPlayerByNameAsync(string name) => await _context.Players.Where(p => p.Name == name).Include(s => s.Scores).Include(pa => pa.PlayerAdvancements).FirstOrDefaultAsync();

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

    public bool PlayerExists(long id) => (_context.Players?.Any(a => a.Id == id)).GetValueOrDefault();

    public bool PlayerExistsByName(string name) => (_context.Players?.Any(a => a.Name == name)).GetValueOrDefault();
}
