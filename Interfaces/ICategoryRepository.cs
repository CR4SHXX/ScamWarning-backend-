using ScamWarning.Models;

namespace ScamWarning.Interfaces;

public interface ICategoryRepository
{
    /// <summary>
    /// Get all categories (pre-populated: Phishing, Phone Scam, etc.)
    /// </summary>
    Task<IEnumerable<Category>> GetAllAsync();
}