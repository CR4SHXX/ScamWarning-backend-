using Microsoft.AspNetCore.Mvc;
using ScamWarning.DTOs;
using ScamWarning.Interfaces;

namespace ScamWarning.Controllers
{
    [Route("api/[controller]")]
    public class WarningsController : BaseApiController
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
        /// Get all pending warnings (public for demo)
        /// </summary>
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var warnings = await _warningService.GetPendingAsync();
            return Ok(warnings);
        }

        /// <summary>
        /// Create a new warning (public for demo)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarningDto dto)
        {
            try
            {
                var warning = await _warningService.CreateAsync(dto, dto.UserId);
                return CreatedAtAction(nameof(GetById), new { id = warning.Id }, warning);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Approve a pending warning (public for demo)
        /// </summary>
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
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
        /// Reject a pending warning (public for demo)
        /// </summary>
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
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
