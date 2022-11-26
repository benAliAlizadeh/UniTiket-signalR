using UniTiKet_Model;

namespace UniTiket.Repositories
{
    public interface IMessageRepository : ICrudRepository<Message>
    {
        Task<List<Message>> GetMessages(int tiketId);
    }
}
