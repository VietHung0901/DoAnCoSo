using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repositories
{
    public class EFTruongRepository : ITruongRepository
    {
        private readonly ApplicationDbContext _context;
        public EFTruongRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tbTruong>> GetAllAsync() 
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbTruong.Include(p => p.LoaiTruong) // Include thông tin về category
            .ToListAsync();
        }
    }
}
