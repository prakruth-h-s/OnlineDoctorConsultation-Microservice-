using ODC.Appointment.Model;
using System.Threading.Tasks;

namespace ODC.Appointment.Repository
{
    public interface IFeedbackRepository
    {
        /// <summary>
        /// Add Feedback
        /// </summary>
        /// <param name="request"></param>
        Task<FeedbackResponse> AddFeedback(Model.Feedback request);

        /// <summary>
        /// Get Feedback By Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>
        Task<FeedbackResponse> GetFeedbackByAppointmentId(int appointmentId);

        /// <summary>
        /// Get Feedback By UserId
        /// </summary>
        /// <param name="userId"></param>
        Task<FeedbackListResponse> GetFeedbacksByUserId(int userId);
    }
}
