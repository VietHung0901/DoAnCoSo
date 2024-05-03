using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repositories
{
    public class EFChiTietQuyDinhRepository : IChiTietQuyDinhRepository
    {
        private readonly ApplicationDbContext _context;

        public EFChiTietQuyDinhRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbChiTietQuyDinh>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbChiTietQuyDinh
            .Include(p => p.Id) // Include thông tin về category
            .ToListAsync();
        }

        public async Task<tbChiTietQuyDinh> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.tbChiTietQuyDinh.Include(p =>
            p.Id).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(tbChiTietQuyDinh chiTietQuyDinh)
        {
            _context.tbChiTietQuyDinh.Add(chiTietQuyDinh);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(tbChiTietQuyDinh chiTietQuyDinh)
        {
            _context.tbChiTietQuyDinh.Update(chiTietQuyDinh);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chiTietQuyDinh = await _context.tbChiTietQuyDinh.FindAsync(id);
            _context.tbChiTietQuyDinh.Remove(chiTietQuyDinh);
            await _context.SaveChangesAsync();
        }
    }
}
