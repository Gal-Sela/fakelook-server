﻿using fakeLook_models.Models;

namespace fakeLook_starter.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetByEmailAndPassword(string email, string password);

    }
}
