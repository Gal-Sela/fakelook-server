using fakeLook_models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace fakeLook_starter.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
    
        public string GetUserById(int id);
        
    }
}

