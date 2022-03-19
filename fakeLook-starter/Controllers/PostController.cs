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

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<PostController>
        [HttpGet]
        [Route("GetById")]
        public JsonResult Get(int id)
        {
            // return new string[] { "value1", "value2" };
            return new JsonResult(_repository.GetById(id));
        }
        [HttpGet]
        [Route("GetAll")]
        public JsonResult GetAll()
        {
            // return new string[] { "value1", "value2" };
            return new JsonResult(_repository.GetAll());
        }


        //// GET api/<PostController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<PostController>

        [HttpPost]
        [Route("Add")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<JsonResult> Post([FromBody] Post post)
        {
            var dbPost = await _repository.Add(post);
            var dtoPost = new Post() {Id = dbPost.Id};
                return new JsonResult(dtoPost);

        }
        //[HttpPost]
        //[Route("FilterByPublisher")]
        //public JsonResult FilterByPublisher([FromBody] int id)
        //{
        //    return new JsonResult(_repository.FilterByPublisher(id));
        //}

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {

            return new JsonResult(await _repository.Delete(id));
                 
        }

        [HttpPost]
        [Route("Filter")]
        public async Task<JsonResult> Filter([FromBody] Filter filter)
        {

            var res = _repository.GetByPredicate(post =>
            {
                bool date = FilterByDate(post.Date, filter.DateFrom, filter.DateTo);
                bool publishers = FilterByPublisher(post.UserId, filter.Publishers);
                bool tags = FilterByTag(post.Tags, filter.Tags);
                bool userTags = FilterByUserTagged(post.UserTaggedPost, filter.UsersTaggedInPostId);

                return date && publishers && tags && userTags;

            });
            return new JsonResult(res);
        }
        bool FilterByDate(DateTime postDate, DateTime dateFrom, DateTime dateTo)
        {
            bool date;
            if (dateFrom == null && dateTo == null)
            {
                date = true;
            }
            else if (dateFrom != null && dateTo == null)
            {
                date = postDate > dateFrom;
            }
            else if (dateFrom == null && dateTo != null)
            {
                date = postDate < dateTo;
            }
            else
            {
                date = postDate > dateFrom && postDate < dateTo;
            }
            return date;
        }
        bool FilterByPublisher(int userId, ICollection<string> userNames)
        {
            if (userNames.Count == 0)
                return true;

            var userName = _repository.GetUserById(userId);
            return userNames.Contains(userName);
        }

        bool FilterByTag(ICollection<Tag> postTags, ICollection<string> tags)
        {
            if (tags.Count == 0)
                return true;

            foreach (Tag tag in postTags)
            {
                foreach (string tagName in tags)
                {
                    if (tag.Content == tagName)
                        return true;

                }
            }
            return false;
        }

        bool FilterByUserTagged(ICollection<UserTaggedPost> users, ICollection<string> tags)
        {
            if (tags.Count == 0)
                return true;
            foreach (UserTaggedPost user in users)
            {
                foreach (string tagName in tags)
                {
                    if (user.User.Name == tagName)
                        return true;

                }
            }
            return false;
        }

    }
}
