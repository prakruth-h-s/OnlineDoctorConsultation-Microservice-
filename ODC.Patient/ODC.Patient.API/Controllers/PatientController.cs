using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODC.Patient.Model;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ODC.Patient.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        /// <summary>
        /// Register Patient
        /// </summary>
        /// <param name="user"></param>

        [HttpPost("RegisterPatient")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                var result = await _patientService.RegisterPatient(user);
                if (result.UserDetail == null)
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
        /// Remove Patient User By Id
        /// </summary>
        /// <param name="userId"></param>

        [HttpDelete("Id")]
        [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemovePatient(int userId)
        {
            try
            {
                var result = await _patientService.RemovePatientById(userId);
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
        /// Get Patient User By Id
        /// </summary>
        /// <param name="userId"></param>

        [HttpGet("Id")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatientById(int userId)
        {
            try
            {
                var result = await _patientService.GetPatientById(userId);
                if (result.Patient == null)
                {
                    result.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(result);
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
        /// Update Patient User Detail
        /// </summary>
        /// <param name="user"></param>

        [HttpPut("UpdatePatientUser")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePatientUser(User user)
        {
            try
            {
                var result = await _patientService.UpdatePatientUserDetail(user);
                if (result.UserDetail == null)
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
