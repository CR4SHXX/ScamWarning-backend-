using System.ComponentModel.DataAnnotations;

namespace ScamWarning.DTOs;

public class CreateWarningDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(2000, MinimumLength = 20, ErrorMessage = "Description must be between 20 and 2000 characters")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Warning signs are required")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Warning signs must be between 10 and 1000 characters")]
    public string WarningSigns { get; set; } = null!;

    [Url(ErrorMessage = "Invalid URL format")]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Category ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "User ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number")]
    public int UserId { get; set; }
}