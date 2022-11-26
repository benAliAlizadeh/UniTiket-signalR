using Microsoft.EntityFrameworkCore;
using UniTiKet_Data.Data;
using UniTiKet_Model;

namespace UniTiket.Repositories
{
    public class CategoryRepository : CrudRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DBContext context) : base(context)
        {
        }

        //public async Task<Group?> GetGroup(int sId, int rId) => await _context.Groups
        //    .Where(c=> (c.FirstUserId == sId && c.SecondUserId == rId) || (c.FirstUserId == rId && c.SecondUserId == sId))
        //    .FirstOrDefaultAsync();

        //public async Task<bool> HasAnyChat(int id) => await _context.Groups.AnyAsync(c => c.FirstUserId == id || c.SecondUserId == id);
    }
}
