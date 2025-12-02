using ScamWarning.Models;

namespace ScamWarning.Interfaces;

public interface IWarningRepository
{
    /// <summary>
    /// Get all APPROVED warnings (for homepage + all warnings list)
    /// </summary>
    Task<IEnumerable<Warning>> GetAllApprovedAsync();

    /// <summary>
    /// Get all warnings regardless of status (for admin)
    /// </summary>
    Task<IEnumerable<Warning>> GetAllAsync();

    /// <summary>
    /// Get one warning with Author, Category, and all Comments (for details page)
    /// </summary>
    Task<Warning?> GetByIdWithDetailsAsync(int id);

    /// <summary>
    /// Get all PENDING warnings (for admin panel)
    /// </summary>
    Task<IEnumerable<Warning>> GetPendingAsync();

    /// <summary>
    /// Add new warning (status = Pending)
    /// </summary>
    Task AddAsync(Warning warning);

    /// <summary>
    /// Update warning (for approve/reject status change)
    /// </summary>
    Task UpdateAsync(Warning warning);

    /// <summary>
    /// Delete a warning (admin only)
    /// </summary>
    Task DeleteAsync(int id);
}