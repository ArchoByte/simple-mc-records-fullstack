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
    
    public async Task<ICollection<Score>?> GetScoresAsync() => await _context.Scores.ToListAsync();

    public async Task<Score?> GetScoreAsync(long id) => await _context.Scores.Where(p => p.Id == id).Include(p => p.Player).FirstOrDefaultAsync();

    public async Task<Score?> GetScoreByNameAsync(string name) => await _context.Scores.Where(p => p.Name == name).Include(p => p.Player).FirstOrDefaultAsync();

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

    public bool ScoreExists(long id) => (_context.Scores?.Any(a => a.Id == id)).GetValueOrDefault();

    public bool ScoreExistsByName(string name) => (_context.Scores?.Any(a => a.Name == name)).GetValueOrDefault();
}
