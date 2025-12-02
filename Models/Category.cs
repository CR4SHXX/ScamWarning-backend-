namespace ScamWarning.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Emoji { get; set; } = "⚠️";
        public string? Description { get; set; }

        // Navigation property
        public ICollection<Warning> Warnings { get; set; } = null!;
    }
}