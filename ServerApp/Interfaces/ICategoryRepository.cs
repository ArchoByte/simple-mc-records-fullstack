using SimpleMcRecords.Models;

namespace SimpleMcRecords.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>?> GetCategoriesAsync();
    Task<Category?> GetCategoryAsync(long id);
    Task<Category?> GetCategoryByNameAsync(string name);
    Task<bool> PutCategoryAsync(Category Category);
    Task PostCategoryAsync(Category Category);
    Task<bool> DeleteCategoryAsync(long id);
    bool CategoryExists(long id);
    bool CategoryExistsByName(string name);
}