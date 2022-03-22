using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly private DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User item)
        {
            var res = _context.Users.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public Task<User> Edit(User item)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            return _context.Users.SingleOrDefault(p => p.Id == id);
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return _context.Users.SingleOrDefault(p => p.Email == email && p.Password==password);
        }


        public ICollection<User> GetByPredicate(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u=>u.UserName == userName);
        }
    }
}
