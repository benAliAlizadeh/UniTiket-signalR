using Microsoft.EntityFrameworkCore;
using UniTiket.Models;
using UniTiKet_Data.Data;
using UniTiKet_Model;

namespace UniTiket.Repositories
{
    public class TiketRepository : CrudRepository<Tiket>, ITiketRepository
    {
        public TiketRepository(DBContext context) : base(context)
        {
        }

        public async Task<List<TiketViewModel>> GetTiketsAsync(int userId) => await _context.Tikets
            .Include(c=> c.Messages)
            .Where(c => c.UserId == userId)
            .Select(c=> new TiketViewModel()
            {
                TiketId = c.TiketId,
                CategoryId = c.CategoryId,
                CreatedTime = c.CreatedTime,
                Title = c.Title,
                UserId = userId,
                LastMessage = c.Messages.OrderByDescending(c=> c.CreatedTime).FirstOrDefault().Text,
                MessageCount = c.Messages.Count()
            })
            .OrderBy(c=> c.CreatedTime)
            .ToListAsync();

        public async Task<List<TiketViewModel>> GetAllTiketsAsync() => await _context.Tikets
           .Include(c => c.Messages)
           .Select(c => new TiketViewModel()
           {
               TiketId = c.TiketId,
               CategoryId = c.CategoryId,
               CreatedTime = c.CreatedTime,
               Title = c.Title,
               UserId = c.UserId,
               LastMessage = c.Messages.OrderByDescending(c => c.CreatedTime).FirstOrDefault().Text,
               MessageCount = c.Messages.Count()
           })
           .OrderByDescending(c => c.CreatedTime)
           .ToListAsync();

        public async Task<Tiket?> GetTiketById(int id) => await _context.Tikets
            .Include(c=> c.User)
            .FirstOrDefaultAsync(c=> c.TiketId == id);
    }
}
