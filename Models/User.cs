namespace ScamWarning.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Warning> Warnings { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = null!;
    }
}