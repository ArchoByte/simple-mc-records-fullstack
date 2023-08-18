using SimpleMcRecords.Models;

namespace SimpleMcRecords.Interfaces;

public interface IPlayerRepository
{
    Task<ICollection<Player>?> GetPlayersAsync();
    Task<Player?> GetPlayerAsync(long id);
    Task<Player?> GetPlayerByNameAsync(string name);
    Task<long> GetIdByName(string name); 
    Task<bool> PutPlayerAsync(Player player);
    Task PostPlayerAsync(Player player);
    Task<bool> DeletePlayerAsync(long id);
    bool PlayerExists(long id);
    bool PlayerExistsByName(string name);
    Task AddAdvancementAsync(long playerId, long advancementId);
    Task RemoveAdvancementAsync(long playerId, long advancementId);
    bool AdvancementExists(long playerId, long advancementId);
}