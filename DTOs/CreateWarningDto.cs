namespace ScamWarning.DTOs;

public class CreateWarningDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string WarningSigns { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
}