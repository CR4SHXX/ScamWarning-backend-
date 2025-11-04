namespace ScamWarning.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int WarningId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Warning Warning { get; set; }
        public User User { get; set; }
    }
}