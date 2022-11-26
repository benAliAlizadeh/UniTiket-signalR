using UniTiKet_Model;

namespace UniTiket.Repositories
{
    public interface IUserRepository : ICrudRepository<User>
    {
        Task<User?> GetUserAsync(string username, string password);

        Task<bool> IsExist(string username);
    }
}
