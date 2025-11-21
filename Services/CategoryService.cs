using ScamWarning.Interfaces;
using ScamWarning.Models;

namespace ScamWarning.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Any(c => c.Id == categoryId);
        }
    }
}
