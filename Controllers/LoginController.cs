using Inzynierka_API.Entities;
using Inzynierka_API.Models;
using Inzynierka_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inzynierka_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            
            _loginService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _loginService.GenerateJwt(dto);
            return Ok(token);
        }

        //[HttpGet("confirm")]
        //public ActionResult Confirm([FromQuery] ConfirmUserDto dto)
        //{
        //    _loginService.ConfirmUser(dto);
        //    return Content("Dziękujemy za aktywację konta");
        //}
    }
}
