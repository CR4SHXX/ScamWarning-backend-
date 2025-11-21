using ScamWarning.Models;

namespace ScamWarning.Interfaces
{
    /// <summary>
    /// Service interface for category management operations
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves all available categories
        /// </summary>
        /// <returns>List of all categories</returns>
        Task<IEnumerable<Category>> GetAllAsync();

        /// <summary>
        /// Checks if a category exists by its ID
        /// </summary>
        /// <param name="categoryId">Category ID to check</param>
        /// <returns>True if category exists, false otherwise</returns>
        Task<bool> CategoryExistsAsync(int categoryId);
    }
}