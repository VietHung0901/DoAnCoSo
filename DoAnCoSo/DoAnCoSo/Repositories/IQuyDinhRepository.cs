using DoAnCoSo.Models;

namespace DoAnCoSo.Repositories
{
    public interface IQuyDinhRepository
    {
        Task<IEnumerable<tbQuyDinh>> GetAllAsync();
        Task<tbQuyDinh> GetByIdAsync(int id);
        Task AddAsync(tbQuyDinh quyDinh);
        Task UpdateAsync(tbQuyDinh quyDinh);
        Task DeleteAsync(int id);
    }
}
