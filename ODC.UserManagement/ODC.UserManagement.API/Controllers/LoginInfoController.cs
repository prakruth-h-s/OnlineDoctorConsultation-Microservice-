using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ODC.UserManagement.Model;

namespace ODC.UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginInfoController : ControllerBase
    {
        private readonly ILoginInfoService _loginInfoService;
        public LoginInfoController(ILoginInfoService service)
        {
            _loginInfoService = service;
        }
         
        /// <summary>
        /// verify Login credentials
        /// </summary>
        /// <param name="userEmail"></param> 
        /// <param name="password"></param> 

        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromHeader(Name = "UserEmail")] string userEmail, [FromHeader(Name = "Password")] string password)
        {
            try
            {
                var user = await _loginInfoService.Login(userEmail, password);
                if (user.UserDetails == null)
                {
                    user.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(user);
                }
                user.HttpStatusCode = System.Net.HttpStatusCode.OK;
                return Ok(user);
            }
            catch (Exception ex)
            {
                var result = new UnprocessableEntityObjectResult(ex.Message);
                return result;
            }
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <param name="userDetail"></param> 

        [HttpPost("SignUp")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignUp([FromHeader(Name = "UserEmail")] string userEmail, [FromHeader(Name = "Password")] string password, User userDetail)
        {
            try
            {
                var user = await _loginInfoService.SignUp(userEmail, password, userDetail);
                if (user.UserDetails== null)
                {
                    user.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(user);
                }
                user.HttpStatusCode = System.Net.HttpStatusCode.OK;
                return Ok(user);
            }
            catch (Exception ex)
            {
                var result = new UnprocessableEntityObjectResult(ex.Message);
                return result;
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>

        [HttpDelete()]
        [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser([FromHeader(Name = "UserEmail")] string userEmail, [FromHeader(Name = "Password")] string password)
        {
            try
            {
                var result = await _loginInfoService.DeleteUser(userEmail, password);
                if (result.IsAffected == false)
                {
                    result.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(result);
                }
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = new UnprocessableEntityObjectResult(ex.Message);
                return result;
            }
        }

        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>

        [HttpPatch()]
        [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePassword([FromHeader(Name = "UserEmail")] string userEmail, [FromHeader(Name = "Password")] string password, string newPassword)
        {
            try
            {
                var result = await _loginInfoService.UpdatePassword(userEmail, password, newPassword);
                if (result.IsAffected == false)
                {
                    result.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(result);
                }
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = new UnprocessableEntityObjectResult(ex.Message);
                return result;
            }
        }

    }
}
