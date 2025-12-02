using ScamWarning.DTOs;
using ScamWarning.Interfaces;
using ScamWarning.Models;

namespace ScamWarning.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<User> RegisterAsync(RegisterDto registerDto)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already exists");
            }

            // Validate password strength
            if (!_authService.ValidatePasswordStrength(registerDto.Password))
            {
                throw new InvalidOperationException("Password must be at least 3 characters long");
            }

            // Create new user - plain text password for demo
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = registerDto.Password, // Plain text for demo
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<User> LoginAsync(LoginDto loginDto)
        {
            // Find user by email
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Verify password - simple string comparison for demo
            if (user.Password != loginDto.Password)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Return user info instead of JWT token
            return user;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null;
        }
    }
}
