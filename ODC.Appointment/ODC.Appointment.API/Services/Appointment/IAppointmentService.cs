using ODC.Appointment.Model;
using System.Threading.Tasks;

namespace ODC.Appointment.API.Services
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Book Appointment
        /// </summary>
        /// <param name="request"></param>
        Task<AppointmentResponse> SaveAppointment(Model.Appointment request);

        /// <summary>
        /// Get Appointment By Id
        /// </summary>
        /// <param name="appointmentId"></param>
        Task<AppointmentResponse> GetAppointmentByAppointmentId(int appointmentId);

        /// <summary>
        /// Get Appointments By UserId
        /// </summary>
        /// <param name="userId"></param>
        ///  <param name="role"></param>
        Task<AppointmentListResponse> GetAppointmentsByUserId(int userId, string role);

        /// <summary>
        /// Update Appointment Status
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="status"></param>
        Task<RequestResponse> UpdateAppointmentStatus(int appointmentId , string status);
    }
}
