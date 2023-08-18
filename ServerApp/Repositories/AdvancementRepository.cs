using Microsoft.EntityFrameworkCore;
using SimpleMcRecords.Data;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Repositories;

public class AdvancementRepository : IAdvancementRepository
{
    private readonly DataContext _context;
    public AdvancementRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Advancement>?> GetAdvancementsAsync() => await _context.Advancements.Include(a => a.Category).ToListAsync();

    public async Task<Advancement?> GetAdvancementAsync(long id) => await _context.Advancements.Where(a => a.Id == id).Include(a => a.Category).Include(a => a.PlayerAdvancements).FirstOrDefaultAsync();

    public async Task<Advancement?> GetAdvancementByNameAsync(string name) => await _context.Advancements.Where(a => a.Name == name).Include(a => a.Category).Include(a => a.PlayerAdvancements).FirstOrDefaultAsync();

    public async Task<long> GetIdByName(string name)
    {
        var advancement = await _context.Advancements.Where(a => a.Name == name).FirstOrDefaultAsync();
        if (advancement == null)
            return 0;
        return advancement.Id;
    }

    public async Task<bool> PutAdvancementAsync(Advancement advancement)
    {
        _context.Entry(advancement).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AdvancementExists(advancement.Id))
                return false;
            throw;
        }

        return true;
    }

    public async Task PostAdvancementAsync(Advancement advancement)
    {
        _context.Advancements.Add(advancement);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAdvancementAsync(long id)
    {
        var advancement = await GetAdvancementAsync(id);
        if (advancement == null)
            return false;

        if (advancement.PlayerAdvancements != null)
            _context.PlayerAdvancements.RemoveRange(advancement.PlayerAdvancements);

        _context.Advancements.Remove(advancement);
        await _context.SaveChangesAsync();

        return true;
    }

    public bool AdvancementExists(long id) => (_context.Advancements?.Any(a => a.Id == id)).GetValueOrDefault();

    public bool AdvancementExistsByName(string name) => (_context.Advancements?.Any(a => a.Name == name)).GetValueOrDefault();
}