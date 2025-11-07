using ScamWarning.Models;

namespace ScamWarning.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Get user by email (for login)
    /// </summary>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Get user by id (for JWT token claims or future use)
    /// </summary>
    Task<User?> GetByIdAsync(int id);

    /// <summary>
    /// Add new user (for register)
    /// </summary>
    Task AddAsync(User user);
}