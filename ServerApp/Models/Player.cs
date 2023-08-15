namespace SimpleMcRecords.Models;

public class Player
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Score>? Scores { get; set; }
    public ICollection<PlayerAchievement>? PlayerAchievements { get; set; }
}
