using ODC.Appointment.Model;
using System.Threading.Tasks;

namespace ODC.Appointment.API.Services
{
    public interface IPrescriptionService
    {
        /// <summary>
        /// Add Prescription
        /// </summary>
        /// <param name="request"></param>
        Task<PrescriptionResponse> AddPrescription(MedicalPrescription request);

        /// <summary>
        /// Get Prescription By Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>
        Task<PrescriptionResponse> GetPrescriptionByAppointmentId(int appointmentId);

        /// <summary>
        /// Get Prescription By UserId
        /// </summary>
        /// <param name="userId"></param>
        Task<PrescriptionListResponse> GetPrescriptionsByUserId(int userId);
    }
}
