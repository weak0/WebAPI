using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Serivces;

namespace WebAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/account")]
    [ApiController]
    public class AccountContorler : ControllerBase
    {
        private readonly IAccountServices _accountService;

        public AccountContorler(IAccountServices accountServices)
        {
            _accountService  = accountServices;
        }
        [HttpPost("register")]
        public ActionResult Register([FromBody]AccountDto dto )
        {
            var id = _accountService.CreateUser(dto);
            return Created($"/api/restuarant/{id}", "ok pomyslnie dodoano");
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            var token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
