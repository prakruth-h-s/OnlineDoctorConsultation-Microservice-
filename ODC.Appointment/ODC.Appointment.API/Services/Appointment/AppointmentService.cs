using ODC.Appointment.Model;
using ODC.Appointment.Repository;
using System;
using System.Threading.Tasks;

namespace ODC.Appointment.API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        /// <summary>
        /// Book Appointment
        /// </summary>
        /// <param name="request"></param>
        public async Task<AppointmentResponse> SaveAppointment(Model.Appointment request)
        {
            AppointmentResponse result = new AppointmentResponse();
            try
            {
                result = await _appointmentRepository.SaveAppointment(request);
                if (result.Appointment != null)
                {
                    result.Message = "Appointment Booked";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in AppointmentService SaveAppointment : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Appointment By Id
        /// </summary>
        /// <param name="appointmentId"></param>
        public async Task<AppointmentResponse> GetAppointmentByAppointmentId(int appointmentId)
        {
            AppointmentResponse response = new AppointmentResponse();
            try
            {
                response = await _appointmentRepository.GetAppointmentByAppointmentId(appointmentId);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in AppointmentService GetAppointmentByAppointmentId : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Get Appointments By UserId
        /// </summary>
        /// <param name="userId"></param>
        ///  <param name="role"></param>
        public async Task<AppointmentListResponse> GetAppointmentsByUserId(int userId, string role)
        {
            AppointmentListResponse response = new AppointmentListResponse();
            try
            {
                response = await _appointmentRepository.GetAppointmentsByUserId(userId, role);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in AppointmentService GetAppointmentsByUserId : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Update Appointment Status
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="status"></param>
        public async Task<RequestResponse> UpdateAppointmentStatus(int appointmentId, string status)
        {
            RequestResponse result = new RequestResponse();
            result.IsAffected = false;
            try
            {
                result = await _appointmentRepository.UpdateAppointmentStatus(appointmentId, status);
                if (result.IsAffected == true)
                        result.Message = "Appointment Status Updated";
                
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in AppointmentService UpdateAppointmentStatus : " + ex.Message;
                return result;
            }
        }

    }
}
