namespace ScamWarning.Models
{
    public class Warning
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string WarningSigns { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User Author { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = null!;
    }
}