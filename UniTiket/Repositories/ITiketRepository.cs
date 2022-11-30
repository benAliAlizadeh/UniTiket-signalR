using UniTiket.Models;
using UniTiKet_Model;

namespace UniTiket.Repositories
{
    public interface ITiketRepository : ICrudRepository<Tiket>
    {
        Task<List<TiketViewModel>> GetTiketsAsync(int userId);
        Task<List<TiketViewModel>> GetAllTiketsAsync();

        Task<Tiket?> GetTiketById(int id);

        Task<List<TiketViewModel>> GetAdminTiketsAsync(int categoryId);
    }
}
