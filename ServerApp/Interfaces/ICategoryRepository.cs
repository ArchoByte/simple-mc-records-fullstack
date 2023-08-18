using SimpleMcRecords.Models;

namespace SimpleMcRecords.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>?> GetCategoriesAsync();
    Task<Category?> GetCategoryAsync(long id);
    Task<Category?> GetCategoryByNameAsync(string name);
    Task<long> GetIdByName(string name); 
    Task<bool> PutCategoryAsync(Category category);
    Task PostCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(long id);
    bool CategoryExists(long id);
    bool CategoryExistsByName(string name);
}