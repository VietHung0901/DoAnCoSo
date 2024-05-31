using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repositories
{
    public class EFLoaiTruongRepository : ILoaiTruongRepository
    {
        private readonly ApplicationDbContext _context;
        public EFLoaiTruongRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tbLoaiTruong>> GetAllAsync() 
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbLoaiTruong.Include(p => p.Truongs) // Include thông tin về tbTruong
            .ToListAsync();
        }
    }
}
