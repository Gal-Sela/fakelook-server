using fakeLook_models.Models;

namespace fakeLook_starter.Interfaces
{
    public interface ITagRepository:IRepository<Tag>
    {
        public Tag isTagExist(string content);
        //public Tag GetTagByContent(string content);
    }
}
