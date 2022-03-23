using fakeLook_models.Models;
using fakeLook_starter.Filters;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _repository;

        public LikeController(ILikeRepository repository)
        {
            _repository = repository;
        }

    
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        [Route("GetNumberOfLikesByPostId")]
        public int GetNumberOfLikesByPostId(int postId)
        {
            return _repository.GetNumberOfLikesByPostId(postId);
        }
        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("Add")]
        [TypeFilter(typeof(GetUserActionFilter))]
        public async Task<JsonResult> Post([FromBody] Like like)
        {
            var dbLike = await _repository.Add(like);
            var dtoLike = new Like() { UserId = dbLike.UserId, PostId = dbLike.PostId, IsActive = dbLike.IsActive };
            return new JsonResult(dtoLike);
        }
        [HttpPost]
        [Route("RemoveLike")]
        public async Task<JsonResult> RemoveLike([FromBody] Like like)
        {

            return new JsonResult(await _repository.ToggleIsActiveLike(like.PostId, like.UserId));
        }

        [HttpPost]
        [Route("IsCurrentUserLiked")]
        //[TypeFilter(typeof(GetUserActionFilter))]

        public async Task<JsonResult> Post( int userId,  int postId)
        {
            return new JsonResult(await _repository.IsCurrentUserLiked(userId, postId));
        }

        // PUT api/<LikeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }



        // DELETE api/<LikeController>/5
        [HttpDelete()]
        //[TypeFilter(typeof(GetUserActionFilter))]

        public async Task<JsonResult> Delete( int userId,int postId)
        {
            return new JsonResult(await _repository.DeleteByPost(userId, postId));
        }

    }
}
