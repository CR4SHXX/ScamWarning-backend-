using Microsoft.EntityFrameworkCore;
using ScamWarning.Data;
using ScamWarning.Interfaces;
using ScamWarning.Models;

namespace ScamWarning.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
}