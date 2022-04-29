using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ODC.Appointment.Model;

namespace ODC.Appointment.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly DapperContext _context;

        public PrescriptionRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add Prescription
        /// </summary>
        /// <param name="request"></param>
        public async Task<PrescriptionResponse> AddPrescription(MedicalPrescription request)
        {
            PrescriptionResponse response = new PrescriptionResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"INSERT INTO Prescription(AppointmentId, Prescription) Values(@AppointmentId, @Prescription)
                                SELECT * FROM Prescription Where Id =  (SELECT CAST( SCOPE_IDENTITY() AS int ))";
                    response.Prescription = await connection.QueryFirstOrDefaultAsync<MedicalPrescription>(query, new
                    {
                        AppointmentId = request.AppointmentId,
                        Prescription = request.Prescription
                    });
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in PrescriptionRepository AddPrescription : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Get Prescription By Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>
        public async Task<PrescriptionResponse> GetPrescriptionByAppointmentId(int appointmentId)
        {
            PrescriptionResponse result = new PrescriptionResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT * FROM Prescription WHERE AppointmentId = @AppointmentId";
                    result.Prescription = await connection.QueryFirstOrDefaultAsync<MedicalPrescription>(query, new { AppointmentId = appointmentId });
                    if (result.Prescription != null)
                        result.Message = "Prescription Found";
                    else
                        result.Message = "Prescription Not Found With This AppointmentId";
                }
                return result;

            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in PrescriptionRepository GetPrescriptionByAppointmentId : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Prescription By UserId
        /// </summary>
        /// <param name="userId"></param>
        public async Task<PrescriptionListResponse> GetPrescriptionsByUserId(int userId)
        {
            PrescriptionListResponse result = new PrescriptionListResponse();
            try
            {
                string query = "";
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT AppointmentId FROM Appointment WHERE PatientUserId = @UserId";
                    var appointmentId = (await connection.QueryAsync<int>(query, new { UserId = userId })).ToList();

                    query = @"SELECT * FROM Prescription WHERE AppointmentId in @AppointmentId";
                    result.Prescription = (await connection.QueryAsync<MedicalPrescription>(query, new { AppointmentId = appointmentId })).ToList();

                    if (result.Prescription?.Count > 0)
                        result.Message = result.Prescription.Count + " Prescription Found";
                    else
                        result.Message = "Prescription Not Found for This UserId";
                }
                return result;

            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in PrescriptionRepository GetPrescriptionsByUserId : " + ex.Message;
                return result;
            }
        }
    }
}
