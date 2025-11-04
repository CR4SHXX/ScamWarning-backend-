namespace ScamWarning.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Warning> Warnings { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}