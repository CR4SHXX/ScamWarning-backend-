using ScamWarning.Models;

namespace ScamWarning.Interfaces;

public interface ICommentRepository
{
    /// <summary>
    /// Get all comments for a specific warning (with User info)
    /// </summary>
    Task<IEnumerable<Comment>> GetByWarningIdAsync(int warningId);

    /// <summary>
    /// Get a comment by ID
    /// </summary>
    Task<Comment?> GetByIdAsync(int id);

    /// <summary>
    /// Add a new comment
    /// </summary>
    Task AddAsync(Comment comment);

    /// <summary>
    /// Delete a comment (admin only)
    /// </summary>
    Task DeleteAsync(int id);
}