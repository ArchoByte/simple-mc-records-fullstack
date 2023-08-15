namespace SimpleMcRecords.Models;

public class PlayerAdvancement
{
    public long PlayerId { get; set; }
    public long AdvancementId { get; set; }
    public DateTime Time { get; set; }
    public Player? Player { get; set; }
    public Advancement? Advancement { get; set; }
}
