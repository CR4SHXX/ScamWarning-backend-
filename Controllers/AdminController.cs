using Microsoft.AspNetCore.Mvc;
using ScamWarning.DTOs;
using ScamWarning.Interfaces;

namespace ScamWarning.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : BaseApiController
    {
        private readonly IWarningService _warningService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public AdminController(
            IWarningService warningService, 
            ICommentService commentService,
            IUserService userService)
        {
            _warningService = warningService;
            _commentService = commentService;
            _userService = userService;
        }

        /// <summary>
        /// Verify if a user is an admin (simplified check for demo)
        /// </summary>
        [HttpGet("verify/{userId}")]
        public async Task<IActionResult> VerifyAdmin(int userId)
        {
            var isAdmin = await _userService.IsAdminAsync(userId);
            return Ok(new { isAdmin });
        }

        /// <summary>
        /// Get all warnings (including pending and rejected) - Admin only
        /// </summary>
        [HttpGet("warnings")]
        public async Task<IActionResult> GetAllWarnings([FromQuery] int userId)
        {
            if (!await _userService.IsAdminAsync(userId))
            {
                return Unauthorized(new { error = "Admin access required" });
            }

            var warnings = await _warningService.GetAllAsync();
            return Ok(warnings);
        }

        /// <summary>
        /// Update a warning - Admin only
        /// </summary>
        [HttpPut("warnings/{id}")]
        public async Task<IActionResult> UpdateWarning(int id, [FromBody] UpdateWarningDto dto, [FromQuery] int userId)
        {
            if (!await _userService.IsAdminAsync(userId))
            {
                return Unauthorized(new { error = "Admin access required" });
            }

            try
            {
                var warning = await _warningService.UpdateAsync(id, dto);
                return Ok(warning);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a warning - Admin only
        /// </summary>
        [HttpDelete("warnings/{id}")]
        public async Task<IActionResult> DeleteWarning(int id, [FromQuery] int userId)
        {
            if (!await _userService.IsAdminAsync(userId))
            {
                return Unauthorized(new { error = "Admin access required" });
            }

            try
            {
                await _warningService.DeleteAsync(id);
                return Ok(new { message = "Warning deleted successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a comment - Admin only
        /// </summary>
        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(int id, [FromQuery] int userId)
        {
            if (!await _userService.IsAdminAsync(userId))
            {
                return Unauthorized(new { error = "Admin access required" });
            }

            try
            {
                await _commentService.DeleteAsync(id);
                return Ok(new { message = "Comment deleted successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Approve a warning - Admin only
        /// </summary>
        [HttpPut("warnings/{id}/approve")]
        public async Task<IActionResult> ApproveWarning(int id, [FromQuery] int userId)
        {
            if (!await _userService.IsAdminAsync(userId))
            {
                return Unauthorized(new { error = "Admin access required" });
            }

            try
            {
                var warning = await _warningService.ApproveAsync(id);
                return Ok(warning);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Reject a warning - Admin only
        /// </summary>
        [HttpPut("warnings/{id}/reject")]
        public async Task<IActionResult> RejectWarning(int id, [FromQuery] int userId)
        {
            if (!await _userService.IsAdminAsync(userId))
            {
                return Unauthorized(new { error = "Admin access required" });
            }

            try
            {
                var warning = await _warningService.RejectAsync(id);
                return Ok(warning);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
