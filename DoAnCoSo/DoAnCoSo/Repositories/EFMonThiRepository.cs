using DoAnCoSo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repositories
{
    public class EFMonThiRepository : IMonThiRepository
    {
        private readonly ApplicationDbContext _context;
        public EFMonThiRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tbMonThi>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbMonThi.Include(p => p.CuocThi) // Include thông tin về tbTruong
            .ToListAsync();
        }
    }
}
