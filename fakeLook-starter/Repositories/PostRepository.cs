using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using fakeLook_starter.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class PostRepository : IPostRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _dtoConverter;
        readonly private ITagRepository _tagRepository;
        readonly private IUserRepository _userRepository;
        readonly private IUserTaggedPostRepository _userTaggedPostRepository;

        public PostRepository(DataContext context,IUserRepository userRepository, IDtoConverter dtoConverter, ITagRepository tagRepository, IUserTaggedPostRepository userTaggedPostRepository)
        {
            _context = context;
            _dtoConverter = dtoConverter;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _userTaggedPostRepository = userTaggedPostRepository;
        }

        private Post dtoLogic(Post p)
        {
            var dtoPost = _dtoConverter.DtoPost(p);
            dtoPost.User = _dtoConverter.DtoUser(p.User);
            dtoPost.Comments = p.Comments!=null?p.Comments.Select(c =>
            {
                var dtoComment = _dtoConverter.DtoComment(c);
                dtoComment.User = _dtoConverter.DtoUser(c.User);
                return dtoComment;
            }).ToList():new List<Comment>();
            dtoPost.Likes = p.Likes != null ? p.Likes.Select(l =>
            {
                var dtoLike = _dtoConverter.DtoLike(l);
                dtoLike.User = _dtoConverter.DtoUser(l.User);
                return dtoLike;
            }).ToList() : new List<Like>();
            dtoPost.Tags = p.Tags != null ?p.Tags.Select(t =>
            {
                var dtoTag = _dtoConverter.DtoTag(t);

                return dtoTag;
            }).ToList(): new List<Tag>();
            dtoPost.UserTaggedPost = p.Tags != null ? p.UserTaggedPost.Select(t =>
            {
                var dtoTaggedPost = _dtoConverter.DtoUserTaggedPost(t);
                dtoTaggedPost.User = _dtoConverter.DtoUser(t.User);
                return dtoTaggedPost;
            }).ToList() : new List<UserTaggedPost>();
            return dtoPost;
        }

        public async Task<Post> Add(Post item)
        {
            string tagPattern = @"#\S+";
            string utpPattern = @"@\S+";
            var description = item.Description;
            //Regex tagRegex = new Regex(tagPattern);
            ICollection<Tag> tags =new List<Tag>();
            ICollection<UserTaggedPost> userTaggedPosts = new List<UserTaggedPost>();
            var matchTag = Regex.Matches(description, tagPattern);

            var matchUtp = Regex.Matches(description, utpPattern);
            for (int i = 0; i < matchTag.Count; i++)
            {
               Tag tag =new Tag();
                tag.Content = matchTag[i].Value.Remove(0,1);
                tags.Add(tag);
            }
            for (int i = 0; i < matchUtp.Count; i++)
            {
                User u = _userRepository.GetUserByUserName(matchUtp[i].Value.Remove(0, 1));
                if (u == null)
                    continue;

             //   UserTaggedPost utp = new UserTaggedPost();
                  //  utp.Id = _userTaggedPostRepository.GetIdByUserName(matchUtp[i].Value.Remove(0,1));
                //  tags.Add(await _tagRepository.Add(tag));
                UserTaggedPost userTaggedPost = new UserTaggedPost();
                //userTaggedPost = _userTaggedPostRepository.GetById(u.Id);
                userTaggedPost.UserId = u.Id;
                userTaggedPosts.Add(userTaggedPost);
             

            }
            item.Tags = tags;
            item.UserTaggedPost = userTaggedPosts;
            var res = _context.Posts.Add(item);
            
            
           
            await _context.SaveChangesAsync();
            return _dtoConverter.DtoPost(res.Entity);
        }

        public async Task<Post> Edit(Post item)
        {
            var temp = _context.Posts.FirstOrDefault(u => u.Id == item.Id);
            if (temp == null)
            {
                return null;//TODO
            }



            _context.Entry<Post>(temp).CurrentValues.SetValues(item);

            await _context.SaveChangesAsync();
            return _dtoConverter.DtoPost(item);
        }

        public string GetUserNameByUserId(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault().UserName;
        }
      
        public Post GetById(int id)
        {
            return _context.Posts.Include(p => p.User)
                   .Include(p => p.Comments).ThenInclude(c => c.User)
                   .Include(p => p.Likes).ThenInclude(l => l.User)
                   .Include(p => p.Tags)
                   .Include(p => p.UserTaggedPost).ThenInclude(u => u.User)
                  // .SingleOrDefault(p => p.Id == id);
                  .Select(dtoLogic).FirstOrDefault(p => p.Id == id);
        }
        public ICollection<Post> GetAll()
        {
            return _context.Posts.Include(p => p.User)
                   .Include(p => p.Comments).ThenInclude(c => c.User)
                   .Include(l => l.Likes.Where(l => l.IsActive)).ThenInclude(u => u.User)
                   .Include(p => p.Tags)
                   .Include(p => p.UserTaggedPost).ThenInclude(u => u.User)
                   .Select(dtoLogic).ToList();
        }

        public ICollection<Post> GetByPredicate(Func<Post, bool> predicate)
        {
            return _context.Posts.Include(p=>p.UserTaggedPost).Include(p=>p.User).Include(p=>p.Tags).Where(predicate).Select(dtoLogic).ToList();
        }

        public async Task<Post> Delete(int id)
        {
            var res = _context.Posts.SingleOrDefault(p => p.Id == id);
            if (res == null)
                return null;
            _context.Posts.Remove(res);
            await _context.SaveChangesAsync();
            return _dtoConverter.DtoPost(res);
        }

    }
}
