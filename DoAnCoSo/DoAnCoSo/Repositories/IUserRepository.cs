using DoAnCoSo.Models;

namespace DoAnCoSo.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task DeleteAsync(string id);

        Task<IEnumerable<ApplicationUser>> GetList();
    }
}
