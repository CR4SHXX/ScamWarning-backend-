using ScamWarning.Interfaces;

namespace ScamWarning.Services
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {
        }

        public string GenerateJwtToken(int userId, string email, bool isAdmin)
        {
            // No JWT needed for demo - return empty string
            return string.Empty;
        }

        public string HashPassword(string password)
        {
            // Plain text password for demo - no hashing
            return password;
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            // Simple string comparison for demo
            return password == passwordHash;
        }

        public bool ValidatePasswordStrength(string password)
        {
            // Simplified validation for demo
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 3;
        }
    }
}
