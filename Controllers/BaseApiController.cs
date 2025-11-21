using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ScamWarning.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Gets the current user's ID from the JWT token
        /// </summary>
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid token");
            }
            return userId;
        }

        /// <summary>
        /// Checks if the current user is an admin
        /// </summary>
        protected bool IsAdmin()
        {
            return User.FindFirst("isAdmin")?.Value == "True";
        }

        /// <summary>
        /// Returns a Forbid result if the current user is not an admin
        /// </summary>
        protected IActionResult ForbidIfNotAdmin()
        {
            return !IsAdmin() ? Forbid() : null!;
        }
    }
}
