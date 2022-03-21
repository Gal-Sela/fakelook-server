using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaggedPostController : ControllerBase
    {
        private readonly IUserTaggedPostRepository _repository;

        public UserTaggedPostController(IUserTaggedPostRepository repository)
        { 
            _repository = repository;
        }

            // GET: api/<UserTaggedPostController>
            [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_repository.GetAll());
        }

        // GET api/<UserTaggedPostController>/5
        [HttpGet]
        [Route("GetById")]
        public JsonResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        // POST api/<UserTaggedPostController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserTaggedPostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserTaggedPostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
