using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        public CommentController(ICommentRepository repository)
        {
            _repository=repository;
        }
        // GET: api/<CommentController>
        [HttpGet]
        [Route("GetById")]
        public JsonResult Get(int id)
        {
            // return new string[] { "value1", "value2" };
            return new JsonResult(_repository.GetById(id));
        }
        [HttpGet]
        [Route("GetAllByPosyId")]
        public JsonResult GetAllByPosyId(int postId)
        {
            // return new string[] { "value1", "value2" };
            return new JsonResult(_repository.GetAllComentsByPostId(postId));
        }
        [HttpGet]
        [Route("GetAll")]
        public JsonResult Get()
        {
            // return new string[] { "value1", "value2" };
            return new JsonResult(_repository.GetAll());
        }



        // POST api/<CommentController>
        [HttpPost]
        [Route("Add")]
        // [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<JsonResult> Post([FromBody] Comment comment)
        {
            var dbComment = await _repository.Add(comment);
            var dtoComment = new Comment()
            {
                Id = dbComment.Id,
                UserId = dbComment.UserId,
                Content = dbComment.Content,
            };
            return new JsonResult(dtoComment);

        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
