using ODC.Appointment.Model;
using ODC.Appointment.Repository;
using System;
using System.Threading.Tasks;

namespace ODC.Appointment.API.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        /// <summary>
        /// Add Prescription
        /// </summary>
        /// <param name="request"></param>
        public async Task<PrescriptionResponse> AddPrescription(MedicalPrescription request)
        {
            PrescriptionResponse result = new PrescriptionResponse();
            try
            {
                result = await _prescriptionRepository.AddPrescription(request);
                if (result.Prescription != null)
                {
                    result.Message = "Prescription Added";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in PrescriptionService AddPrescription : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Prescription By Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>
        public async Task<PrescriptionResponse> GetPrescriptionByAppointmentId(int appointmentId)
        {
            PrescriptionResponse response = new PrescriptionResponse();
            try
            {
                response = await _prescriptionRepository.GetPrescriptionByAppointmentId(appointmentId);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in PrescriptionService GetPrescriptionByAppointmentId : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Get Prescription By UserId
        /// </summary>
        /// <param name="userId"></param>
        public async Task<PrescriptionListResponse> GetPrescriptionsByUserId(int userId)
        {
            PrescriptionListResponse response = new PrescriptionListResponse();
            try
            {
                response = await _prescriptionRepository.GetPrescriptionsByUserId(userId);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in PrescriptionService GetPrescriptionByAppointmentId : " + ex.Message;
                return response;
            }
        }
    }
}
