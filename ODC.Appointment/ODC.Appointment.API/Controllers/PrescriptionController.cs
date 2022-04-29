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
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        /// <summary>
        /// Add Prescription
        /// </summary>
        /// <param name="request"></param>

        [HttpPost("AddPrescription")]
        [ProducesResponseType(typeof(PrescriptionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(MedicalPrescription request)
        {
            try
            {
                var result = await _prescriptionService.AddPrescription(request);
                if (result.Prescription == null)
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
        /// Get Prescription By Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>

        [HttpGet("AppointmentId")]
        [ProducesResponseType(typeof(PrescriptionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionByAppointmentId(int appointmentId)
        {
            try
            {
                var result = await _prescriptionService.GetPrescriptionByAppointmentId(appointmentId);
                if (result.Prescription == null)
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
        /// Get Prescription By UserId
        /// </summary>
        /// <param name="userId"></param>

        [HttpGet("UserId")]
        [ProducesResponseType(typeof(PrescriptionListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionByUserId(int userId)
        {
            try
            {
                var result = await _prescriptionService.GetPrescriptionsByUserId(userId);
                if (result.Prescription?.Count == 0)
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

    }
}
