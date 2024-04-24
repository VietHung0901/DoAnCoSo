using DoAnCoSo.Models;

namespace DoAnCoSo.Repositories
{
    public interface ITruongRepository
    {
        Task<IEnumerable<tbTruong>> GetAllAsync();
    }
}
