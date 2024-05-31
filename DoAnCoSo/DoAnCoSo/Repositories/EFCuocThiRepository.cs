using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repositories
{
    public class EFCuocThiRepository : ICuocThiRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCuocThiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbCuocThi>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbCuocThi
            .Include(p => p.Id) // Include thông tin về category
            .ToListAsync();
        }
        public async Task<tbCuocThi> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.tbCuocThi.Include(p =>
            p.Id).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(tbCuocThi cuocThi)
        {
            _context.tbCuocThi.Add(cuocThi);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(tbCuocThi cuocThi)
        {
            _context.tbCuocThi.Update(cuocThi);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var cuocThi = await _context.tbCuocThi.FindAsync(id);
            _context.tbCuocThi.Remove(cuocThi);
            await _context.SaveChangesAsync();
        }
    }
}
