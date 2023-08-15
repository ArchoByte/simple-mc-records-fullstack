namespace SimpleMcRecords.Models;

public class PlayerAchievement
{
    public long PlayerId { get; set; }
    public long AchievementId { get; set; }
    public DateTime Time { get; set; }
    public Player? Player { get; set; }
    public Achievement? Achievement { get; set; }
}
