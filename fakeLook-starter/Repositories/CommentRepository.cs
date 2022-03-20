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
    public class CommentRepository : ICommentRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _dtoConverter;

        public CommentRepository(DataContext context, IDtoConverter dtoConverter)
        {
            _context = context;
            _dtoConverter = dtoConverter;
        }
        private Comment dtoLogic(Comment c)
        {
            var dtoComment = _dtoConverter.DtoComment(c);
            dtoComment.User= _dtoConverter.DtoUser(c.User);
            dtoComment.Post=_dtoConverter.DtoPost(c.Post);
            dtoComment.Tags=c.Tags.Select(t =>
            {
                var dtoTag = _dtoConverter.DtoTag(t);

                return dtoTag;
            }).ToList();
            dtoComment.UserTaggedComment = c.UserTaggedComment.Select(t =>
            {
                var UserTaggedComment = _dtoConverter.DtoUserTaggedComment(t);
                UserTaggedComment.User = _dtoConverter.DtoUser(t.User);
                return UserTaggedComment;
            }).ToList();
            return dtoComment;
        }
            public async Task<Comment> Add(Comment item)
        {
            var res = _context.Comments.Add(item);
            await _context.SaveChangesAsync();
            return _dtoConverter.DtoComment(res.Entity);
        }

        public Task<Comment> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> Edit(Comment item)
        {
            throw new NotImplementedException();
        }

        public ICollection<Comment> GetAll()
        {
            return _context.Comments.Include(u => u.User)
                   .Include(p=>p.Post).ThenInclude(u=>u.User)
                  .Include(t => t.Tags)
                  .Include(utc => utc.UserTaggedComment).ThenInclude(u => u.User)
                  .Select(dtoLogic).ToList();
        }

        public Comment GetById(int id)
        {
            return _context.Comments.Include(u => u.User)
                   .Include(p => p.Post).ThenInclude(u => u.User)
                  .Include(t => t.Tags)
                  .Include(utc => utc.UserTaggedComment).ThenInclude(u => u.User)
                  .Select(dtoLogic).FirstOrDefault(c=>c.Id== id);
        }

        public ICollection<Comment> GetByPredicate(Func<Comment, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ICollection<Comment> GetAllComentsByPostId(int postId)
        {
            return _context.Comments.Where(p=>p.PostId== postId).Include(u => u.User)
                             .Include(p=>p.Post)
                             .Include(t => t.Tags)
                             .Include(utc => utc.UserTaggedComment).ThenInclude(u => u.User)
                             .Select(dtoLogic).ToList();
        }
    }
}
