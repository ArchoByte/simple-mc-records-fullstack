namespace SimpleMcRecords.Models;

public class Advancement
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public Category? Category { get; set; }
    public ICollection<PlayerAdvancement>? PlayerAdvancements { get; set; }
}
