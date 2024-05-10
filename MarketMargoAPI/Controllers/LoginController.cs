using MarketMargoAPI.Models;
using MarketMargoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketMargoAPI.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginController : ControllerBase
    {
        private readonly ConnectionDB _dbContext;

        public LoginController(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            LoginService loginService = new LoginService(_dbContext);
            Usuario? usuario = loginService.ValidarCredenciais(login.Email, login.Senha);

            if (usuario != null)
            {
                usuario.Senha = string.Empty;
                usuario.Token = loginService.GerarTokenJWT(login.Email);

                return Ok(usuario);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
