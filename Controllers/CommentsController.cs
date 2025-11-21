using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScamWarning.DTOs;
using ScamWarning.Interfaces;
using System.Security.Claims;

namespace ScamWarning.Controllers
{
    [ApiController]
    [Route("api/warnings/{warningId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Get all comments for a warning
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetByWarningId(int warningId)
        {
            var comments = await _commentService.GetByWarningIdAsync(warningId);
            return Ok(comments);
        }

        /// <summary>
        /// Add a comment to a warning (authenticated users only)
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(int warningId, [FromBody] CreateCommentDto dto)
        {
            try
            {
                // Get user ID from JWT token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized(new { error = "Invalid token" });
                }

                var comment = await _commentService.AddAsync(dto, userId, warningId);
                return CreatedAtAction(nameof(GetByWarningId), new { warningId }, comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
