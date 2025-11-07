using ScamWarning.Models;

namespace ScamWarning.Interfaces;

public interface ICommentRepository
{
    /// <summary>
    /// Get all comments for a specific warning (with User info)
    /// </summary>
    Task<IEnumerable<Comment>> GetByWarningIdAsync(int warningId);

    /// <summary>
    /// Add a new comment
    /// </summary>
    Task AddAsync(Comment comment);
}