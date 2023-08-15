namespace SimpleMcRecords.Models;

public class Achievement
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public Achievement? Parent { get; set; }
    public ICollection<PlayerAchievement>? PlayerAchievements { get; set; }
}
