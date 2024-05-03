using DoAnCoSo.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repositories
{
    public class EFNoiDungRepository : INoiDungRepository
    {
        private readonly ApplicationDbContext _context;

        public EFNoiDungRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbNoiDung>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbNoiDung
            .Include(p => p.Id) // Include thông tin về category
            .ToListAsync();
        }
        public async Task<tbNoiDung> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.tbNoiDung.Include(p =>
            p.Id).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(tbNoiDung noiDung)
        {
            _context.tbNoiDung.Add(noiDung);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(tbNoiDung noiDung)
        {
            _context.tbNoiDung.Update(noiDung);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var noiDung = await _context.tbNoiDung.FindAsync(id);
            _context.tbNoiDung.Remove(noiDung);
            await _context.SaveChangesAsync();
        }
    }
}
