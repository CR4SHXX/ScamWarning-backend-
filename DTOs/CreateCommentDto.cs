using System.ComponentModel.DataAnnotations;

namespace ScamWarning.DTOs;

public class CreateCommentDto
{
    [Required(ErrorMessage = "Comment text is required")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "Comment must be between 1 and 500 characters")]
    public string Text { get; set; } = null!;
}