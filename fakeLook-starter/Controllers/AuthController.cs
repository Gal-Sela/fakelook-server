using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fakeLook_starter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IUserRepository _repository;
        private ITokenService _tokenService { get; }

        public AuthController(IUserRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User user)
        {
            var currentUser = _repository.GetByEmailAndPassword(user.Email,user.Password);
            if (currentUser == null) return Problem("user not in system");
            var token = _tokenService.CreateToken(currentUser);
            return Ok(new AuthResponse (){Token=token, Id=currentUser.Id, UserName=currentUser.UserName });
        }


        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            //TODO: check if user already exist
            var currentUser = await _repository.Add(user);
            var token = _tokenService.CreateToken(currentUser);
            return Ok(new AuthResponse() { Token = token, Id = currentUser.Id });
        }



        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] User user)
        {
            //TODO: check if user already exist

            var msg =   _repository.ForgotPassword(user.Email, user.Name);
            return Ok(new {msg = msg});

        }




        //[Authorize]
        //[HttpGet]
        //[Route("TestAll")]

        //public IActionResult TestAll()
        //{
        //    return Ok();
        //}
        //[Authorize(Roles = "admin")]
        //[HttpGet]
        //[Route("TestAdmin")]
        //public IActionResult TestAdmin()
        //{
        //    return Ok();
        //}


        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
