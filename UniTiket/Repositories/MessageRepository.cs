using Microsoft.EntityFrameworkCore;
using UniTiKet_Data.Data;
using UniTiKet_Model;

namespace UniTiket.Repositories
{
    public class MessageRepository : CrudRepository<Message>, IMessageRepository
    {
        public MessageRepository(DBContext context) : base(context)
        {
        }

        public async Task<List<Message>> GetMessages(int tiketId) => await _context.Messages.Where(c => c.TiketId == tiketId).OrderBy(c=> c.CreatedTime).ToListAsync();
    }
}
