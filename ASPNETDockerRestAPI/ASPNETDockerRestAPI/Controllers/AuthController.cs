using Asp.Versioning;
using ASPNETDockerRestAPI.Business;
using ASPNETDockerRestAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETDockerRestAPI.Controllers
{
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1")]
    [ApiController]
    public class AuthController(ILoginBusiness loginBusiness) : ControllerBase
    {
        [HttpPost]
        [Route("sign-in")]
        public IActionResult SignIn([FromBody] UserDto userDto)
        {
            if (userDto is null)
            {
                return BadRequest("Invalid client request");
            }

            var token = loginBusiness.ValidateCredentials(userDto);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenDto tokenDto)
        {
            if (tokenDto is null)
            {
                return BadRequest("Invalid client request");
            }

            var token = loginBusiness.ValidateCredentials(tokenDto);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity!.Name;
            var result = loginBusiness.RevokeToken(username);

            if (!result)
            {
                return BadRequest("Invalid client request");
            }

            return NoContent();
        }
    }
}
