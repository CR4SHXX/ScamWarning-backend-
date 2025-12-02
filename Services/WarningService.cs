using ScamWarning.DTOs;
using ScamWarning.Interfaces;
using ScamWarning.Models;

namespace ScamWarning.Services
{
    public class WarningService : IWarningService
    {
        private readonly IWarningRepository _warningRepository;
        private readonly ICategoryService _categoryService;

        public WarningService(IWarningRepository warningRepository, ICategoryService categoryService)
        {
            _warningRepository = warningRepository;
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<WarningDto>> GetAllApprovedAsync()
        {
            var warnings = await _warningRepository.GetAllApprovedAsync();
            return warnings.Select(MapToDto);
        }

        public async Task<IEnumerable<WarningDto>> GetAllAsync()
        {
            var warnings = await _warningRepository.GetAllAsync();
            return warnings.Select(MapToDto);
        }

        public async Task<WarningDto?> GetByIdAsync(int id)
        {
            var warning = await _warningRepository.GetByIdWithDetailsAsync(id);
            return warning != null ? MapToDto(warning) : null;
        }

        public async Task<IEnumerable<WarningDto>> GetPendingAsync()
        {
            var warnings = await _warningRepository.GetPendingAsync();
            return warnings.Select(MapToDto);
        }

        public async Task<WarningDto> CreateAsync(CreateWarningDto dto, int authorId)
        {
            // Validate category exists
            if (!await _categoryService.CategoryExistsAsync(dto.CategoryId))
            {
                throw new InvalidOperationException($"Category with id {dto.CategoryId} does not exist");
            }

            var warning = new Warning
            {
                Title = dto.Title,
                Description = dto.Description,
                WarningSigns = dto.WarningSigns,
                ImageUrl = dto.ImageUrl,
                AuthorId = authorId,
                CategoryId = dto.CategoryId,
                Status = "Pending", // New warnings need admin approval
                CreatedAt = DateTime.UtcNow
            };

            await _warningRepository.AddAsync(warning);

            // Navigation properties are now loaded by AddAsync, so we can map directly
            return MapToDto(warning);
        }

        public async Task<WarningDto> ApproveAsync(int warningId)
        {
            var warning = await _warningRepository.GetByIdWithDetailsAsync(warningId);
            if (warning == null)
            {
                throw new KeyNotFoundException($"Warning with id {warningId} not found");
            }

            warning.Status = "Approved";
            await _warningRepository.UpdateAsync(warning);

            return MapToDto(warning);
        }

        public async Task<WarningDto> RejectAsync(int warningId)
        {
            var warning = await _warningRepository.GetByIdWithDetailsAsync(warningId);
            if (warning == null)
            {
                throw new KeyNotFoundException($"Warning with id {warningId} not found");
            }

            warning.Status = "Rejected";
            await _warningRepository.UpdateAsync(warning);

            return MapToDto(warning);
        }

        public async Task DeleteAsync(int warningId)
        {
            var warning = await _warningRepository.GetByIdWithDetailsAsync(warningId);
            if (warning == null)
            {
                throw new KeyNotFoundException($"Warning with id {warningId} not found");
            }

            await _warningRepository.DeleteAsync(warningId);
        }

        public async Task<WarningDto> UpdateAsync(int warningId, UpdateWarningDto dto)
        {
            var warning = await _warningRepository.GetByIdWithDetailsAsync(warningId);
            if (warning == null)
            {
                throw new KeyNotFoundException($"Warning with id {warningId} not found");
            }

            // Validate category exists
            if (!await _categoryService.CategoryExistsAsync(dto.CategoryId))
            {
                throw new InvalidOperationException($"Category with id {dto.CategoryId} does not exist");
            }

            warning.Title = dto.Title;
            warning.Description = dto.Description;
            warning.WarningSigns = dto.WarningSigns;
            warning.ImageUrl = dto.ImageUrl;
            warning.CategoryId = dto.CategoryId;
            warning.Status = dto.Status;

            await _warningRepository.UpdateAsync(warning);

            return MapToDto(warning);
        }

        public async Task<IEnumerable<WarningDto>> SearchAndFilterAsync(string? searchTerm = null, int? categoryId = null)
        {
            var warnings = await _warningRepository.GetAllApprovedAsync();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                warnings = warnings.Where(w =>
                    w.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    w.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Apply category filter
            if (categoryId.HasValue)
            {
                warnings = warnings.Where(w => w.CategoryId == categoryId.Value);
            }

            return warnings.Select(MapToDto);
        }

        private static WarningDto MapToDto(Warning warning)
        {
            return new WarningDto
            {
                Id = warning.Id,
                Title = warning.Title,
                Description = warning.Description,
                WarningSigns = warning.WarningSigns,
                ImageUrl = warning.ImageUrl,
                Status = warning.Status,
                CreatedAt = warning.CreatedAt,
                AuthorUsername = warning.Author.Username,
                CategoryName = warning.Category.Name,
                CategoryEmoji = warning.Category.Emoji
            };
        }
    }
}
