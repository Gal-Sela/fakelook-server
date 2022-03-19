﻿using fakeLook_dal.Data;
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
            var res = _context.Likes.Add(item);
            await _context.SaveChangesAsync();
            return _dtoConverter.DtoLike(res.Entity);
        }

        private Like dtoLogic(Like l)
        {
            var dtoLike = _dtoConverter.DtoLike(l);
            dtoLike.User = _dtoConverter.DtoUser(l.User);
            dtoLike.Post = _dtoConverter.DtoPost(l.Post);
            return dtoLike;
        }

        public bool IsCurrentUserLiked(int userId, int postId)
        {
            if (_context.Likes.FirstOrDefault(l => l.UserId == userId && l.PostId == postId) != null)
                return true;
            return false;
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
            throw new NotImplementedException();
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
    }
}

