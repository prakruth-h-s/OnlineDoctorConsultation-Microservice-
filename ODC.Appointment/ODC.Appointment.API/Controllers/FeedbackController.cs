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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// Add Feedback
        /// </summary>
        /// <param name="request"></param>

        [HttpPost("AddFeedback")]
        [ProducesResponseType(typeof(FeedbackResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Model.Feedback request)
        {
            try
            {
                var result = await _feedbackService.AddFeedback(request);
                if (result.Feedback == null)
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
        /// Get Feedback By Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>

        [HttpGet("AppointmentId")]
        [ProducesResponseType(typeof(FeedbackResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedbackByAppointmentId(int appointmentId)
        {
            try
            {
                var result = await _feedbackService.GetFeedbackByAppointmentId(appointmentId);
                if (result.Feedback == null)
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
        /// Get Feedback By UserId
        /// </summary>
        /// <param name="userId"></param>

        [HttpGet("UserId")]
        [ProducesResponseType(typeof(FeedbackListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedbacksByUserId(int userId)
        {
            try
            {
                var result = await _feedbackService.GetFeedbacksByUserId(userId);
                if (result.Feedback?.Count == 0)
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
