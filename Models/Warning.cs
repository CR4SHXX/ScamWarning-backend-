namespace ScamWarning.Models
{
    public class Warning
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WarningSigns { get; set; }
        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User Author { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}