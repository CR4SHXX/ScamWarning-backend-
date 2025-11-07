namespace ScamWarning.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; } = null!;  // from Comment.User.Username
}