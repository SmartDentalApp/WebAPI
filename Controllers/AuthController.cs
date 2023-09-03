using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smart_dental_webapi.Entity;
using smart_dental_webapi.Models.User;
using smart_dental_webapi.Repositories;
using smart_dental_webapi.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace smart_dental_webapi.Controllers
{
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserPostResponseModel>> Authenticate([FromBody]UserPostRequestModel userModel)
        {
            if (userModel == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = UserRepository.Get(userModel.Username, userModel.Password);

            if (user == null) 
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new UserPostResponseModel
            {
                User = user,
                Token = token
            };
        }
    }
}
