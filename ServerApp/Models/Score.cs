namespace SimpleMcRecords.Models;

public class Score
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public int? Value { get; set; }
    public Player? Player { get; set; }
}
