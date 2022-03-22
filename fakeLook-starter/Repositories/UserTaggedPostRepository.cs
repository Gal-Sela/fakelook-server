using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class UserTaggedPostRepository: IUserTaggedPostRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _dtoConverter;

        public UserTaggedPostRepository(DataContext context, IDtoConverter dtoConverter)
        {
            _context = context;
            _dtoConverter = dtoConverter;
        }
        private UserTaggedPost dtoLogic(UserTaggedPost u)
        {
            var dtoUserTaggedPost = _dtoConverter.DtoUserTaggedPost(u);
           // dtoUserTaggedPost.Post = _dtoConverter.DtoPost(u.Post);
            //dtoUserTaggedPost.User = _dtoConverter.DtoUser(u.User);
            return dtoUserTaggedPost;

        }

        public async Task<UserTaggedPost> Add(UserTaggedPost item)
        {
            var res = _context.UserTaggedPosts.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public Task<UserTaggedPost> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserTaggedPost> Edit(UserTaggedPost item)
        {
            throw new NotImplementedException();
        }

        public ICollection<UserTaggedPost> GetAll()
        {
            return _context.UserTaggedPosts.Include(u=>u.Post)
                   .Include(u=>u.User)
                   .ToList();
        }

        public UserTaggedPost GetById(int id)
        {
            return _context.UserTaggedPosts.Include(u => u.Post)
                   .Include(u => u.User)
                   .Select(dtoLogic)
                   .SingleOrDefault(p => p.Id == id);
        }

        public ICollection<UserTaggedPost> GetByPredicate(Func<UserTaggedPost, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public int GetIdByUserName(string userName)
        {
            return _context.UserTaggedPosts.SingleOrDefault(u=>u.User.UserName == userName).Id;
        }
    }
}
