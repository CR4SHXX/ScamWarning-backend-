namespace ScamWarning.Interfaces
{
    /// <summary>
    /// Service interface for authentication operations including JWT token generation and password management
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Generates a JWT token for authenticated user with 1 week expiration
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <param name="email">The email address of the user</param>
        /// <param name="isAdmin">Whether the user has admin privileges</param>
        /// <returns>JWT token string</returns>
        string GenerateJwtToken(int userId, string email, bool isAdmin);

        /// <summary>
        /// Hashes a password using BCrypt with secure salt generation
        /// </summary>
        /// <param name="password">Plain text password to hash</param>
        /// <returns>BCrypt hashed password</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifies a password against its BCrypt hash
        /// </summary>
        /// <param name="password">Plain text password to verify</param>
        /// <param name="passwordHash">BCrypt hash to compare against</param>
        /// <returns>True if password matches hash, false otherwise</returns>
        bool VerifyPassword(string password, string passwordHash);

        /// <summary>
        /// Validates password meets minimum security requirements
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>True if password meets requirements, false otherwise</returns>
        bool ValidatePasswordStrength(string password);
    }
}