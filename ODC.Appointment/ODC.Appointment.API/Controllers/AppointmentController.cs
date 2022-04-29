using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODC.Appointment.API.Services;
using ODC.Appointment.Model;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ODC.Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Book Appointment
        /// </summary>
        /// <param name="request"></param>

        [HttpPost("BookAppointment")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Model.Appointment request)
        {
            try
            {
                var result = await _appointmentService.SaveAppointment(request);
                if (result.Appointment == null)
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
        /// Get Appointment By Id
        /// </summary>
        /// <param name="appointmentId"></param>

        [HttpGet("AppointmentId")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppointmentByAppointmentId(int appointmentId)
        {
            try
            {
                var result = await _appointmentService.GetAppointmentByAppointmentId(appointmentId);
                if (result.Appointment == null)
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
        /// Get Appointments By UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>

        [HttpGet("UserId")]
        [ProducesResponseType(typeof(AppointmentListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppointmentsByUserId(int userId, string role)
        {
            try
            {
                var result = await _appointmentService.GetAppointmentsByUserId(userId, role);
                if (result.Appointment?.Count == 0)
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
        /// Update Appointment Status
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="status"></param>

        [HttpPatch("Update Appointment Status")]
        [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAppointmentStatus(int appointmentId, string status)
        {
            try
            {
                var result = await _appointmentService.UpdateAppointmentStatus(appointmentId, status);
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
