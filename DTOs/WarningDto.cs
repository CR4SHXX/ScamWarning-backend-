using ScamWarning.Models;

namespace ScamWarning.DTOs;

public class WarningDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string WarningSigns { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string AuthorUsername { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}