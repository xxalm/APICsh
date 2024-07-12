using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Domain.Model;
using PrimeiraAPI.Application.Services;
using PrimeiraAPI.Domain.Model.EmployeeAggregate;

namespace PrimeiraAPI.Controllers
{

    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller {

        [HttpPost]
        public IActionResult Auth(string username, string password) {
            
            if (username == "lecksonricardodealmeidalessadefarias" &&  password == "12345678") {
                var token = TokenService.GenerateToken(new Employee());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}
