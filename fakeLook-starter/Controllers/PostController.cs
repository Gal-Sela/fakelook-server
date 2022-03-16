using fakeLook_models.Models;
using fakeLook_starter.Filters;
using fakeLook_starter.Interfaces;
using fakeLook_starter.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        [Route("FilterByPublisher")]
        public JsonResult FilterByPublisher([FromBody] int id)
        {
            return new JsonResult(_repository.FilterByPublisher(id));
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

       
        //public async Task<JsonResult> Filter(Filter filter)
        //{
        //    var res = _repository.GetByPredicate(post => {




        //    });
        //}
    }
}
