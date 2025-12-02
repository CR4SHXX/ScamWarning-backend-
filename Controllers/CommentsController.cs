using Microsoft.AspNetCore.Mvc;
using ScamWarning.DTOs;
using ScamWarning.Interfaces;

namespace ScamWarning.Controllers
{
    [Route("api/warnings/{warningId}/comments")]
    public class CommentsController : BaseApiController
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
        /// Add a comment to a warning (public for demo)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add(int warningId, [FromBody] CreateCommentDto dto)
        {
            try
            {
                var comment = await _commentService.AddAsync(dto, dto.UserId, warningId);
                return CreatedAtAction(nameof(GetByWarningId), new { warningId }, comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
