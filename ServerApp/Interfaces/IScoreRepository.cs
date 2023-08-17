using SimpleMcRecords.Models;

namespace SimpleMcRecords.Interfaces;

public interface IScoreRepository
{
    Task<ICollection<Score>?> GetScoresAsync();
    Task<Score?> GetScoreAsync(long id);
    Task<Score?> GetScoreByNameAsync(string name);
    Task<bool> PutScoreAsync(Score advancement);
    Task PostScoreAsync(Score advancement);
    Task<bool> DeleteScoreAsync(long id);
    bool ScoreExists(long id);
    bool ScoreExistsByName(string name);
}
