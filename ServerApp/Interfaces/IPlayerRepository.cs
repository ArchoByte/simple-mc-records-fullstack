using SimpleMcRecords.Models;

namespace SimpleMcRecords.Interfaces;

public interface IPlayerRepository
{
    Task<ICollection<Player>?> GetPlayersAsync();
    Task<Player?> GetPlayerAsync(long id);
    Task<Player?> GetPlayerByNameAsync(string name);
    Task<bool> PutPlayerAsync(Player Player);
    Task PostPlayerAsync(Player Player);
    Task<bool> DeletePlayerAsync(long id);
    bool PlayerExists(long id);
    bool PlayerExistsByName(string name);
}