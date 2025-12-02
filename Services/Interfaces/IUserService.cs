using ScamWarning.DTOs;
using ScamWarning.Models;

namespace ScamWarning.Interfaces
{
    /// <summary>
    /// Service interface for user management and authentication operations
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user account
        /// </summary>
        /// <param name="registerDto">Registration information</param>
        /// <returns>Created user entity</returns>
        /// <exception cref="InvalidOperationException">Thrown when email already exists or password is weak</exception>
        Task<User> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Authenticates user and returns user info
        /// </summary>
        /// <param name="loginDto">Login credentials</param>
        /// <returns>User entity</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when credentials are invalid</exception>
        Task<User> LoginAsync(LoginDto loginDto);

        /// <summary>
        /// Retrieves user by their unique identifier
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User entity or null if not found</returns>
        Task<User?> GetByIdAsync(int id);

        /// <summary>
        /// Checks if an email address is already registered
        /// </summary>
        /// <param name="email">Email address to check</param>
        /// <returns>True if email exists, false otherwise</returns>
        Task<bool> EmailExistsAsync(string email);
    }
}