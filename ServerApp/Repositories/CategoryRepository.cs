using Microsoft.EntityFrameworkCore;
using SimpleMcRecords.Data;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;
    public CategoryRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Category>?> GetCategoriesAsync() => await _context.Categories.ToListAsync();

    public async Task<Category?> GetCategoryAsync(long id) => await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<Category?> GetCategoryByNameAsync(string name) => await _context.Categories.Where(c => c.Name == name).FirstOrDefaultAsync();

    public async Task<long> GetIdByName(string name)
    {
        var category = await _context.Categories.Where(c => c.Name == name).FirstOrDefaultAsync();
        if (category == null)
            return 0;
        return category.Id;
    }

    public async Task<bool> PutCategoryAsync(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(category.Id))
                return false;
            throw;
        }

        return true;
    }

    public async Task PostCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteCategoryAsync(long id)
    {
        var category = await GetCategoryAsync(id);
        if (category == null)
            return false;
        
        if (category.Advancements != null)
        {
            foreach (var advancement in category.Advancements)
            {
                var currentAdv = await _context.Advancements.Where(a => a.Id == advancement.Id).Include(pa => pa.PlayerAdvancements).FirstOrDefaultAsync();

                if (currentAdv == null) throw new NullReferenceException();

                if (currentAdv.PlayerAdvancements != null)
                    _context.PlayerAdvancements.RemoveRange(currentAdv.PlayerAdvancements);
                _context.Advancements.Remove(currentAdv);
            }
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }

    public bool CategoryExists(long id) => (_context.Categories?.Any(c => c.Id == id)).GetValueOrDefault();

    public bool CategoryExistsByName(string name) => (_context.Categories?.Any(c => c.Name == name)).GetValueOrDefault();
}
