using SimpleMcRecords.Models;

namespace SimpleMcRecords.Interfaces;

public interface IScoreRepository
{
    Task<ICollection<Score>?> GetScoresAsync();
    Task<Score?> GetScoreAsync(long id);
    Task<Score?> GetScoreByNameAndPlayerAsync(string name, string playerName);
    Task<long> GetIdByNameAndPlayer(string name, string playerName);
    Task<bool> PutScoreAsync(Score score);
    Task PostScoreAsync(Score score);
    Task<bool> DeleteScoreAsync(long id);
    bool ScoreExists(long id);
    bool ScoreExistsByNameAndPlayer(string name, string playerName);
}
