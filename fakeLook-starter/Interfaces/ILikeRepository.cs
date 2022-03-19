﻿using fakeLook_models.Models;
using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface ILikeRepository : IRepository<Like>
    {
        public bool IsCurrentUserLiked(int userId, int postId);

        public Task<Like> DeleteByPost(int likeId, int postId);


    }
}
