using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repositories
{
    public class EFQuyDinhRepository : IQuyDinhRepository
    {
        private readonly ApplicationDbContext _context;

        public EFQuyDinhRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tbQuyDinh>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbQuyDinh.Include(p => p.ChiTietQuyDinhs) // Include thông tin về category
            .ToListAsync();
        }
        public async Task<tbQuyDinh> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.tbQuyDinh.Include(p => p.ChiTietQuyDinhs).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(tbQuyDinh quyDinh)
        {
            _context.tbQuyDinh.Add(quyDinh);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(tbQuyDinh quyDinh)
        {
            _context.tbQuyDinh.Update(quyDinh);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var quyDinh = await _context.tbQuyDinh.FindAsync(id);
            _context.tbQuyDinh.Remove(quyDinh);
            await _context.SaveChangesAsync();
        }
    }
}
