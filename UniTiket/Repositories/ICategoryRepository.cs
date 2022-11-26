using UniTiKet_Model;

namespace UniTiket.Repositories
{
    public interface ICategoryRepository : ICrudRepository<Category>
    {
        //Task<bool> HasAnyChat(int id);
        //Task<Group?> GetGroup(int sId, int rId);
    }
}
