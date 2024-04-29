using DoAnCoSo.Models;
namespace DoAnCoSo.Repositories
{
    public interface ICuocThiRepository
    {
        Task<IEnumerable<tbCuocThi>> GetAllAsync();
        Task<tbCuocThi> GetByIdAsync(int id);
        Task AddAsync(tbCuocThi cuocThi);
        Task UpdateAsync(tbCuocThi cuocThi);
        Task DeleteAsync(int id);
    }
}
