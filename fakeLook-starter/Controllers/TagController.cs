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
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _repository;

        public TagController(ITagRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<TagController>
        [HttpGet]
        public JsonResult Get()
        {
            //return new string[] { "value1", "value2" };
            return new JsonResult(_repository.GetAll());
               return new JsonResult(_repository.GetAll());
        }
        [HttpGet]
        [Route("GetById")]
        public JsonResult Get(int id)
        {
            // return new string[] { "value1", "value2" };
            return new JsonResult(_repository.GetById(id));
        }
        // GET api/<TagController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<TagController>
        [HttpPost]
        [Route("Add")]
        public async Task<JsonResult> Post([FromBody] Tag tag)
        {
            var dbTag = await _repository.Add(tag);
            var dtoTag = new Tag() { Id = dbTag.Id,Content=dbTag.Content };
            return new JsonResult(dtoTag);
        }

        // PUT api/<TagController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TagController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
