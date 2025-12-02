using System.ComponentModel.DataAnnotations;

namespace ScamWarning.DTOs;

public class CreateCommentDto
{
    [Required(ErrorMessage = "Comment text is required")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "Comment must be between 1 and 500 characters")]
    public string Text { get; set; } = null!;

    [Required(ErrorMessage = "User ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number")]
    public int UserId { get; set; }
}