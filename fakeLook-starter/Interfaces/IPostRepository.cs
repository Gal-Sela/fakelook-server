using fakeLook_models.Models;
using System.Collections.Generic;

namespace fakeLook_starter.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        public ICollection<Post> FilterByPublisher(int id);

    }
}

