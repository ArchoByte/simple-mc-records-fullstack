using Microsoft.EntityFrameworkCore;
using SimpleMcRecords.Data;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Repositories;

public class ScoreRepository : IScoreRepository
{
    private readonly DataContext _context;
    public ScoreRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Score>?> GetScoresAsync() => await _context.Scores.Include(s => s.Player).ToListAsync();

    public async Task<Score?> GetScoreAsync(long id) => await _context.Scores.Where(s => s.Id == id).Include(s => s.Player).FirstOrDefaultAsync();

    public async Task<Score?> GetScoreByNameAndPlayerAsync(string name, string playerName) => await _context.Scores.Where(s => s.Name == name).Include(s => s.Player).Where(s => s.Player!.Name == playerName).FirstOrDefaultAsync();

    public async Task<long> GetIdByNameAndPlayer(string name, string playerName)
    {
        var score = await _context.Scores.Where(s => s.Name == name).Include(s => s.Player).Where(s => s.Player!.Name == playerName).FirstOrDefaultAsync();
        if (score == null)
            return 0;
        return score.Id;
    }
    
    public async Task<bool> PutScoreAsync(Score score)
    {
        _context.Entry(score).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ScoreExists(score.Id))
                return false;
            throw;
        }

        return true;
    }

    public async Task PostScoreAsync(Score score)
    {
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteScoreAsync(long id)
    {
        var score = await GetScoreAsync(id);
        if (score == null)
            return false;

        _context.Scores.Remove(score);
        await _context.SaveChangesAsync();

        return true;
    }

    public bool ScoreExists(long id) => (_context.Scores?.Any(s => s.Id == id)).GetValueOrDefault();

    public bool ScoreExistsByNameAndPlayer(string name, string playerName) => (_context.Scores?.Include(s => s.Player).Any(s => s.Name == name && s.Player!.Name == playerName)).GetValueOrDefault();
}
