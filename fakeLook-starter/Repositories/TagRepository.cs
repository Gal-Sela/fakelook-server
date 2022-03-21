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

    public class TagRepository : ITagRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _dtoConverter;

        private Tag dtoLogic(Tag t)
        {
            var dtoTag = _dtoConverter.DtoTag(t);
            dtoTag.Posts = t.Posts.Select(p =>
            {
                var dtoPost= _dtoConverter.DtoPost(p);
                return dtoPost;
            }).ToList();
            dtoTag.Comments= t.Comments.Select(p => { 
            var dtoComment= _dtoConverter.DtoComment(p);
                return dtoComment;
            }).ToList();
            return dtoTag;
        
        }
        public TagRepository(DataContext context, IDtoConverter dtoConverter)
        {
            _context = context;
            _dtoConverter = dtoConverter;
        }
        public async Task<Tag> Add(Tag item)
        {
            //   Tag t = _context.Tags.FirstOrDefault(t => t.Content == item.Content);
            Tag tag = isTagExist(item.Content);
            if (tag != null)
                return tag;
            else
            {
                var res = _context.Tags.Add(item);
                await _context.SaveChangesAsync();
                return _dtoConverter.DtoTag(res.Entity);
            }
        }

        public Task<Tag> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> Edit(Tag item)
        {
            throw new NotImplementedException();
        }

        public ICollection<Tag> GetAll()
        {
            return _context.Tags.Include(t=>t.Posts)
                   .Include(t=>t.Comments)
                   .Select(dtoLogic).ToList();

        }

        public Tag GetById(int id)
        {
            return _context.Tags.Include(t => t.Posts)
                   .Include(t => t.Comments)
                   .Select(dtoLogic).FirstOrDefault(t=>t.Id==id);
        }

        public ICollection<Tag> GetByPredicate(Func<Tag, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Tag isTagExist(string content)
        {
            var tag = _context.Tags.Where(t=>t.Content == content).SingleOrDefault();
            return tag ;
        }
    }
}
