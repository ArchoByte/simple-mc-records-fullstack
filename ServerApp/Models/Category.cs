namespace SimpleMcRecords.Models;

public class Category
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Advancement>? Advancements { get; set; }
}