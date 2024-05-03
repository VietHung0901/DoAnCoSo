using DoAnCoSo.Models;

namespace DoAnCoSo.Repositories
{
    public interface INoiDungRepository
    {
        Task<IEnumerable<tbNoiDung>> GetAllAsync();
        Task<tbNoiDung> GetByIdAsync(int id);
        Task AddAsync(tbNoiDung noiDung);
        Task UpdateAsync(tbNoiDung noiDung);
        Task DeleteAsync(int id);
    }
}
