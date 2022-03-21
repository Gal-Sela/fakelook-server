using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using fakeLook_starter.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class PostRepository : IPostRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _dtoConverter;
        public PostRepository(DataContext context, IDtoConverter dtoConverter)
        {
            _context = context;
            _dtoConverter = dtoConverter;
        }

        private Post dtoLogic(Post p)
        {
            var dtoPost = _dtoConverter.DtoPost(p);
            dtoPost.User = _dtoConverter.DtoUser(p.User);
            dtoPost.Comments = p.Comments.Select(c =>
            {
                var dtoComment = _dtoConverter.DtoComment(c);
                dtoComment.User = _dtoConverter.DtoUser(c.User);
                return dtoComment;
            }).ToList();
            dtoPost.Likes = p.Likes.Select(l =>
            {
                var dtoLike = _dtoConverter.DtoLike(l);
                dtoLike.User = _dtoConverter.DtoUser(l.User);
                return dtoLike;
            }).ToList();
            dtoPost.Tags = p.Tags.Select(t =>
            {
                var dtoTag = _dtoConverter.DtoTag(t);

                return dtoTag;
            }).ToList();
            dtoPost.UserTaggedPost = p.UserTaggedPost.Select(t =>
            {
                var dtoTaggedPost = _dtoConverter.DtoUserTaggedPost(t);
                dtoTaggedPost.User = _dtoConverter.DtoUser(t.User);
                return dtoTaggedPost;
            }).ToList();
            return dtoPost;
        }

        public async Task<Post> Add(Post item)
        {
            var res = _context.Posts.Add(item);
            await _context.SaveChangesAsync();
            return _dtoConverter.DtoPost(res.Entity);
        }

        public async Task<Post> Edit(Post item)
        {
            var temp = _context.Posts.FirstOrDefault(u => u.Id == item.Id);
            if (temp == null)
            {
                return null;//TODO
            }
            _context.Entry<Post>(temp).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return _dtoConverter.DtoPost(item);
        }

        public string GetUserNameByUserId(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault().UserName;
        }
      
        public Post GetById(int id)
        {
            return _context.Posts.Include(p => p.User)
                   .Include(p => p.Comments).ThenInclude(c => c.User)
                   .Include(p => p.Likes).ThenInclude(l => l.User)
                   .Include(p => p.Tags)
                   .Include(p => p.UserTaggedPost).ThenInclude(u => u.User)
                  // .SingleOrDefault(p => p.Id == id);
                  .Select(dtoLogic).FirstOrDefault(p => p.Id == id);
        }
        public ICollection<Post> GetAll()
        {
            return _context.Posts.Include(p => p.User)
                   .Include(p => p.Comments).ThenInclude(c => c.User)
                   .Include(l => l.Likes).ThenInclude(u => u.User)
                   .Include(p => p.Tags)
                   .Include(p => p.UserTaggedPost).ThenInclude(u => u.User)
                   .Select(dtoLogic).ToList();
        }

        public ICollection<Post> GetByPredicate(Func<Post, bool> predicate)
        {
            return _context.Posts.Include(p=>p.UserTaggedPost).Include(p=>p.User).Include(p=>p.Tags).Where(predicate).ToList();
        }

        public async Task<Post> Delete(int id)
        {
            var res = _context.Posts.SingleOrDefault(p => p.Id == id);
            if (res == null)
                return null;
            _context.Posts.Remove(res);
            await _context.SaveChangesAsync();
            return _dtoConverter.DtoPost(res);
        }

    }
}
