using ScamWarning.DTOs;

namespace ScamWarning.Interfaces
{
    /// <summary>
    /// Service interface for warning management operations including CRUD and approval workflow
    /// </summary>
    public interface IWarningService
    {
        /// <summary>
        /// Retrieves all approved warnings
        /// </summary>
        /// <returns>List of approved warnings</returns>
        Task<IEnumerable<WarningDto>> GetAllApprovedAsync();

        /// <summary>
        /// Retrieves a warning by its unique identifier
        /// </summary>
        /// <param name="id">Warning ID</param>
        /// <returns>Warning DTO or null if not found</returns>
        Task<WarningDto?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all pending warnings awaiting moderation
        /// </summary>
        /// <returns>List of pending warnings</returns>
        Task<IEnumerable<WarningDto>> GetPendingAsync();

        /// <summary>
        /// Creates a new warning with pending status
        /// </summary>
        /// <param name="dto">Warning creation data</param>
        /// <param name="authorId">ID of the user creating the warning</param>
        /// <returns>Created warning DTO</returns>
        /// <exception cref="InvalidOperationException">Thrown when category is invalid</exception>
        Task<WarningDto> CreateAsync(CreateWarningDto dto, int authorId);

        /// <summary>
        /// Approves a pending warning (admin only)
        /// </summary>
        /// <param name="warningId">Warning ID to approve</param>
        /// <returns>Updated warning DTO</returns>
        /// <exception cref="KeyNotFoundException">Thrown when warning not found</exception>
        Task<WarningDto> ApproveAsync(int warningId);

        /// <summary>
        /// Rejects a pending warning (admin only)
        /// </summary>
        /// <param name="warningId">Warning ID to reject</param>
        /// <returns>Updated warning DTO</returns>
        /// <exception cref="KeyNotFoundException">Thrown when warning not found</exception>
        Task<WarningDto> RejectAsync(int warningId);

        /// <summary>
        /// Searches and filters approved warnings
        /// </summary>
        /// <param name="searchTerm">Search term for title and description</param>
        /// <param name="categoryId">Filter by category ID (optional)</param>
        /// <returns>List of filtered warnings</returns>
        Task<IEnumerable<WarningDto>> SearchAndFilterAsync(
            string? searchTerm = null,
            int? categoryId = null);

        /// <summary>
        /// Gets all warnings regardless of status (admin only)
        /// </summary>
        /// <returns>List of all warnings</returns>
        Task<IEnumerable<WarningDto>> GetAllAsync();

        /// <summary>
        /// Deletes a warning (admin only)
        /// </summary>
        /// <param name="warningId">Warning ID to delete</param>
        /// <exception cref="KeyNotFoundException">Thrown when warning not found</exception>
        Task DeleteAsync(int warningId);

        /// <summary>
        /// Updates a warning (admin only)
        /// </summary>
        /// <param name="warningId">Warning ID to update</param>
        /// <param name="dto">Updated warning data</param>
        /// <returns>Updated warning DTO</returns>
        /// <exception cref="KeyNotFoundException">Thrown when warning not found</exception>
        Task<WarningDto> UpdateAsync(int warningId, UpdateWarningDto dto);
    }
}