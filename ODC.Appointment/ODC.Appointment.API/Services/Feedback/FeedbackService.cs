using ODC.Appointment.Model;
using ODC.Appointment.Repository;
using System;
using System.Threading.Tasks;

namespace ODC.Appointment.API.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        /// <summary>
        /// Add Feedback
        /// </summary>
        /// <param name="request"></param>
        public async Task<FeedbackResponse> AddFeedback(Model.Feedback request)
        {
            FeedbackResponse result = new FeedbackResponse();
            try
            {
                result = await _feedbackRepository.AddFeedback(request);
                if (result.Feedback != null)
                {
                    result.Message = "Feedback Added";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in FeedbackService AddFeedback : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Feedback By Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>
        public async Task<FeedbackResponse> GetFeedbackByAppointmentId(int appointmentId)
        {
            FeedbackResponse response = new FeedbackResponse();
            try
            {
                response = await _feedbackRepository.GetFeedbackByAppointmentId(appointmentId);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in FeedbackService GetFeedbackByAppointmentId : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Get Feedback By UserId
        /// </summary>
        /// <param name="userId"></param>
        public async Task<FeedbackListResponse> GetFeedbacksByUserId(int userId)
        {
            FeedbackListResponse response = new FeedbackListResponse();
            try
            {
                response = await _feedbackRepository.GetFeedbacksByUserId(userId);
                foreach(var value in response.Feedback)
                {
                    response.AverageRating = response.AverageRating + value.Rating;
                }
                if (response.AverageRating > 0)
                    response.AverageRating = response.AverageRating / response.Feedback.Count;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in FeedbackService GetFeedbacksByUserId : " + ex.Message;
                return response;
            }
        }
    }
}
