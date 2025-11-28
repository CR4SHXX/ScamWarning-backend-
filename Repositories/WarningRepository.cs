using Microsoft.EntityFrameworkCore;
using ScamWarning.Data;
using ScamWarning.Interfaces;
using ScamWarning.Models;
using ScamWarning.Repositories;

namespace ScamWarning.Repositories;

public class WarningRepository : IWarningRepository
{
    private readonly AppDbContext _context;

    public WarningRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Warning>> GetAllApprovedAsync()
    {
        return await _context.Warnings
            .Where(w => w.Status == "Approved")
            .Include(w => w.Author)
            .Include(w => w.Category)
            .Include(w => w.Comments!)
                .ThenInclude(c => c.User)
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<Warning?> GetByIdWithDetailsAsync(int id)
    {
        var warning = await _context.Warnings
            .Include(w => w.Author)
            .Include(w => w.Category)
            .Include(w => w.Comments!)
                .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (warning == null)
            throw new NotFoundException($"Warning with id {id} not found.");

        return warning;
    }

    public async Task<IEnumerable<Warning>> GetPendingAsync()
    {
        return await _context.Warnings
            .Where(w => w.Status == "Pending")
            .Include(w => w.Author)
            .Include(w => w.Category)
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Warning warning)
    {
        await _context.Warnings.AddAsync(warning);
        await _context.SaveChangesAsync();
        
        // Load the navigation properties after saving
        await _context.Entry(warning).Reference(w => w.Author).LoadAsync();
        await _context.Entry(warning).Reference(w => w.Category).LoadAsync();
        
        if (warning.Author == null || warning.Category == null)
        {
            throw new InvalidOperationException("Failed to load navigation properties for the warning.");
        }
    }

    public async Task UpdateAsync(Warning warning)
    {
        _context.Warnings.Update(warning);
        await _context.SaveChangesAsync();
    }
}