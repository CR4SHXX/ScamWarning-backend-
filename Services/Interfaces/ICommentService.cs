using ScamWarning.DTOs;

namespace ScamWarning.Interfaces
{
    /// <summary>
    /// Service interface for comment management operations
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Retrieves all comments for a specific warning
        /// </summary>
        /// <param name="warningId">Warning ID to get comments for</param>
        /// <returns>List of comments</returns>
        Task<IEnumerable<CommentDto>> GetByWarningIdAsync(int warningId);

        /// <summary>
        /// Adds a new comment to a warning
        /// </summary>
        /// <param name="dto">Comment creation data</param>
        /// <param name="userId">ID of the user creating the comment</param>
        /// <param name="warningId">ID of the warning to comment on</param>
        /// <returns>Created comment DTO</returns>
        /// <exception cref="KeyNotFoundException">Thrown when warning doesn't exist</exception>
        Task<CommentDto> AddAsync(CreateCommentDto dto, int userId, int warningId);

        /// <summary>
        /// Deletes a comment (admin only)
        /// </summary>
        /// <param name="commentId">Comment ID to delete</param>
        /// <exception cref="KeyNotFoundException">Thrown when comment doesn't exist</exception>
        Task DeleteAsync(int commentId);
    }
}