using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class LikeRepository : ILikeRepository
    {

        readonly private DataContext _context;
        readonly private IDtoConverter _dtoConverter;

        public LikeRepository(DataContext context, IDtoConverter dtoConverter)
        {
            _context = context;
            _dtoConverter = dtoConverter;
        }

        public async Task<Like> Add(Like item)
        {
            //if (_context.Likes.FirstOrDefault(l => l.UserId == item.UserId && l.PostId == item.PostId) != null)
            //    return await RemoveLike(item.UserId, item.PostId);
            //else
            //{
            if (!IsLikeExist(item.UserId, item.PostId))
            {
                item.IsActive = true;
                var res = _context.Likes.Add(item);
                await _context.SaveChangesAsync();
                return _dtoConverter.DtoLike(res.Entity);
            }
            else
               return await ToggleIsActiveLike(item.PostId, item.UserId);

            //}
        }

        private Like dtoLogic(Like l)
        {
            var dtoLike = _dtoConverter.DtoLike(l);
            dtoLike.User = _dtoConverter.DtoUser(l.User);
            dtoLike.Post = _dtoConverter.DtoPost(l.Post);
            return dtoLike;
        }

        public Task<bool> IsCurrentUserLiked(int userId, int postId)
        {
            if (_context.Likes.FirstOrDefault(l => l.UserId == userId && l.PostId == postId && l.IsActive) != null)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }


        public Task<Like> Delete(int id)
        {
            throw new NotImplementedException();

        }

        public Task<Like> Edit(Like item)
        {
            throw new NotImplementedException();
        }

        public ICollection<Like> GetAll()
        {
            throw new NotImplementedException();
        }

        public Like GetById(int id)
        {
            return _context.Likes.SingleOrDefault(l=>l.Id == id);
        }

        public ICollection<Like> GetByPredicate(Func<Like, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Like> DeleteByPost(int userId, int postId)
        {
            var res = _context.Likes.SingleOrDefault(l => l.UserId == userId && l.PostId == postId);
            if (res == null)
                return null;
            _context.Likes.Remove(res);
            await _context.SaveChangesAsync();
            return _dtoConverter.DtoLike(res);
        }

        public int GetNumberOfLikesByPostId(int postId)
        {
            return _context.Likes.Where(p=>p.PostId == postId).Where(l=>l.IsActive).Count();
        }

        public async Task<Like> ToggleIsActiveLike( int postId, int userId)
        {
            var like = _context.Likes.Where(l=>l.UserId==userId && l.PostId==postId).SingleOrDefault();
          //  var like = GetById(likeId);
            like.IsActive = !like.IsActive;
            await _context.SaveChangesAsync();
            return like;
        }

        public bool IsLikeExist(int userId, int postId)
        {
            var x= (_context.Likes.SingleOrDefault(l=>l.UserId==userId && l.PostId==postId) != null);
            return x;
        }
    }
}

