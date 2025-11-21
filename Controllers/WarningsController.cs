using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScamWarning.DTOs;
using ScamWarning.Interfaces;
using System.Security.Claims;

namespace ScamWarning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarningsController : ControllerBase
    {
        private readonly IWarningService _warningService;

        public WarningsController(IWarningService warningService)
        {
            _warningService = warningService;
        }

        /// <summary>
        /// Get all approved warnings
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetApproved()
        {
            var warnings = await _warningService.GetAllApprovedAsync();
            return Ok(warnings);
        }

        /// <summary>
        /// Get a specific warning by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var warning = await _warningService.GetByIdAsync(id);
            if (warning == null)
            {
                return NotFound(new { error = "Warning not found" });
            }
            return Ok(warning);
        }

        /// <summary>
        /// Search and filter warnings
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? searchTerm, [FromQuery] int? categoryId)
        {
            var warnings = await _warningService.SearchAndFilterAsync(searchTerm, categoryId);
            return Ok(warnings);
        }

        /// <summary>
        /// Get all pending warnings (admin only)
        /// </summary>
        [HttpGet("pending")]
        [Authorize]
        public async Task<IActionResult> GetPending()
        {
            // Check if user is admin
            var isAdmin = User.FindFirst("isAdmin")?.Value == "True";
            if (!isAdmin)
            {
                return Forbid();
            }

            var warnings = await _warningService.GetPendingAsync();
            return Ok(warnings);
        }

        /// <summary>
        /// Create a new warning (authenticated users only)
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateWarningDto dto)
        {
            try
            {
                // Get user ID from JWT token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized(new { error = "Invalid token" });
                }

                var warning = await _warningService.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = warning.Id }, warning);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Approve a pending warning (admin only)
        /// </summary>
        [HttpPut("{id}/approve")]
        [Authorize]
        public async Task<IActionResult> Approve(int id)
        {
            // Check if user is admin
            var isAdmin = User.FindFirst("isAdmin")?.Value == "True";
            if (!isAdmin)
            {
                return Forbid();
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
        /// Reject a pending warning (admin only)
        /// </summary>
        [HttpPut("{id}/reject")]
        [Authorize]
        public async Task<IActionResult> Reject(int id)
        {
            // Check if user is admin
            var isAdmin = User.FindFirst("isAdmin")?.Value == "True";
            if (!isAdmin)
            {
                return Forbid();
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
