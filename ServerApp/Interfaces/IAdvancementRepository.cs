using SimpleMcRecords.Models;

namespace SimpleMcRecords.Interfaces;

public interface IAdvancementRepository
{
    Task<ICollection<Advancement>?> GetAdvancementsAsync();
    Task<Advancement?> GetAdvancementAsync(long id);
    Task<Advancement?> GetAdvancementByNameAsync(string name);
    Task<long> GetIdByName(string name); 
    Task<bool> PutAdvancementAsync(Advancement advancement);
    Task PostAdvancementAsync(Advancement advancement);
    Task<bool> DeleteAdvancementAsync(long id);
    bool AdvancementExists(long id);
    bool AdvancementExistsByName(string name);
}
