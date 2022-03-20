using fakeLook_models.Models;
using System.Collections.Generic;

namespace fakeLook_starter.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public ICollection<Comment> GetAllComentsByPostId(int Postid);
    }
}
