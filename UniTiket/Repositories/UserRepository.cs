using Microsoft.EntityFrameworkCore;
using UniTiKet_Data.Data;
using UniTiKet_Model;

namespace UniTiket.Repositories
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(DBContext context) : base(context)
        {
        }

        public async Task<User?> GetUserAsync(string username, string password) => await _context.Users.Where(c => c.UserName == username && c.Password == password).FirstOrDefaultAsync();
        public async Task<bool> IsExist(string username) => await _context.Users.AnyAsync(c => c.UserName == username);
    }
}
