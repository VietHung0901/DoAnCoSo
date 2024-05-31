using DoAnCoSo.Models;

namespace DoAnCoSo.Repositories
{
    public interface IMonThiRepository
    {
        Task<IEnumerable<tbMonThi>> GetAllAsync();
    }
}
