using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODC.Doctor.Model;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ODC.Doctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// Register Doctor
        /// </summary>
        /// <param name="user"></param>

        [HttpPost("RegisterDoctor")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                var result = await _doctorService.RegisterDoctor(user);
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
        /// Get Doctors By Speciality
        /// </summary>
        /// <param name="speciality"></param>

        [HttpGet("Speciality")]
        [ProducesResponseType(typeof(DoctorsListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorsBySpeciality(string speciality)
        {
            try
            {
                var result = await _doctorService.GetDoctorsBySpeciality(speciality);
                if (result.Doctors?.Count == 0)
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
        /// Get All Doctors
        /// </summary>

        [HttpGet("All Doctors")]
        [ProducesResponseType(typeof(DoctorsListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var result = await _doctorService.GetAllDoctors();
                if (result.Doctors?.Count == 0)
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
        /// Get Doctors By Id
        /// </summary>
        /// <param name="userId"></param>

        [HttpGet("Id")]
        [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorsById(int userId)
        {
            try
            {
                var result = await _doctorService.GetDoctorsById(userId);
                if (result.Doctor == null)
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
        /// Remove Doctors By Id
        /// </summary>
        /// <param name="userId"></param>

        [HttpDelete("Id")]
        [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveDoctor(int userId)
        {
            try
            {
                var result = await _doctorService.RemoveDoctorById(userId);
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
        /// Update Doctor User Detail
        /// </summary>
        /// <param name="user"></param>

        [HttpPut("UpdateDoctorUser")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDoctorUser(User user)
        {
            try
            {
                var result = await _doctorService.UpdateDoctorUserDetail(user);
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
