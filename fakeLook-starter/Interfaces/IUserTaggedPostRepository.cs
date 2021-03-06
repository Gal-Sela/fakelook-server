using fakeLook_models.Models;

namespace fakeLook_starter.Interfaces
{
    public interface IUserTaggedPostRepository: IRepository<UserTaggedPost>
    {
        public int GetIdByUserName(string userName);
    }
}
