using fakeLook_models.Models;
using fakeLook_starter.Filters;
using fakeLook_starter.Interfaces;
using fakeLook_starter.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;
        private readonly IUserRepository _userRepository;

        public PostController(IPostRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        // GET: api/<PostController>
        [HttpGet]
        [Route("GetById")]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }
        [HttpGet]
        [Route("GetAll")]
        public JsonResult GetAll()
        {


            return new JsonResult(_repository.GetAll());
        }

        // POST api/<PostController>

        [HttpPost]
        [Route("Add")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<JsonResult> Post([FromBody] Post post)
        {
            try
            {
                var dbPost = await _repository.Add(post);
                var dtoPost = new Post()
                {
                    Id = dbPost.Id,
                    UserId = dbPost.UserId,
                    Date = dbPost.Date,
                    Description = dbPost.Description,
                    ImageSorce = dbPost.ImageSorce,
                    X_Position = dbPost.X_Position,
                    Y_Position = dbPost.Y_Position,
                    Z_Position = dbPost.Z_Position
                };
                return new JsonResult(dtoPost);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);

            }


        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]

        public async Task<JsonResult> Put(int id, [FromBody] Post post)
        {
            return new JsonResult(await _repository.Edit(post));

        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<JsonResult> Delete(int id)
        {
            return new JsonResult(await _repository.Delete(id));

        }

        [HttpPost]
        [Route("Filter")]
        [TypeFilter(typeof(GetUserActionFilter))]
        //[Authorize]
        public async Task<JsonResult> Filter([FromBody] Filter filter)

        {

            var res = _repository.GetByPredicate(post =>
           {
               bool date = FilterByDate(post.Date, filter.DateFrom, filter.DateTo);
               bool publishers = FilterByPublisher(post.UserId, filter.Publishers);
               bool tags = FilterByTag(post.Tags, filter.Tags);
               bool userTags = FilterByUserTagged(post.UserTaggedPost, filter.UsersTaggedInPost);

               return date && publishers && tags && userTags;

           });

            return new JsonResult(res);
        }
        bool FilterByDate(DateTime postDate, DateTime? dateFrom, DateTime? dateTo)
        {
            bool date;
            if (dateFrom == null)
                dateFrom = DateTime.MinValue;
            if (dateTo == null)
                dateTo = DateTime.Now;

            date = postDate >= dateFrom && postDate <= dateTo;

            return date;
        }
        bool FilterByPublisher(int userId, ICollection<string> userNames)
        {
            if (userNames.Count == 1)
                if (userNames.Contains(""))
                    return true;

            var userName = _repository.GetUserNameByUserId(userId);
            return userNames.Contains(userName);
        }

        bool FilterByTag(ICollection<Tag> postTags, ICollection<string> tags)
        {
            if (tags.Count == 1)
                if (tags.Contains(""))
                    return true;

            foreach (Tag tag in postTags)
            {
                foreach (string tagName in tags)
                {
                    if (tag.Content.Contains(tagName))
                        return true;

                }
            }
            return false;
        }

        bool FilterByUserTagged(ICollection<UserTaggedPost> users, ICollection<string> tags)
        {
            if (tags.Count == 1)
                if (tags.Contains(""))
                    return true;
            foreach (UserTaggedPost userTaggedPost in users)
            {
                
                var userName = _repository.GetUserNameByUserId(userTaggedPost.UserId);

                foreach (string tagName in tags)
                {
                    if (userName.Contains(tagName))
                        return true;

                }
            }
            return false;
        }

    }
}
