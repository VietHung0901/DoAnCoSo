using DoAnCoSo.Models;

namespace DoAnCoSo.Repositories
{
    public interface IChiTietQuyDinhRepository
    {
        Task<IEnumerable<tbChiTietQuyDinh>> GetAllAsync();
        Task<tbChiTietQuyDinh> GetByIdAsync(int id);
        Task AddAsync(tbChiTietQuyDinh chiTietQuyDinh);
        Task UpdateAsync(tbChiTietQuyDinh chiTietQuyDinh);
        Task DeleteAsync(int id);
    }
}
