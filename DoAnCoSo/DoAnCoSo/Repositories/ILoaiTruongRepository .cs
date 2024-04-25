using DoAnCoSo.Models;

namespace DoAnCoSo.Repositories
{
    public interface ILoaiTruongRepository
    {
        Task<IEnumerable<tbLoaiTruong>> GetAllAsync();
    }
}
