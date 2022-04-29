using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ODC.Appointment.Model;

namespace ODC.Appointment.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DapperContext _context;

        public AppointmentRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Book Appointment
        /// </summary>
        /// <param name="request"></param>

        public async Task<AppointmentResponse> SaveAppointment(Model.Appointment request)
        {
            AppointmentResponse response = new AppointmentResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"INSERT INTO Appointment(PatientUserId, DoctorUserId, Date, HealthIssue, Status) Values(@PatientUserId, @DoctorUserId, @Date, @HealthIssue, @Status)
                                SELECT * FROM Appointment Where AppointmentId =  (SELECT CAST( SCOPE_IDENTITY() AS int ))";
                    response.Appointment = await connection.QueryFirstOrDefaultAsync<Model.Appointment>(query, new
                    {
                        PatientUserId = request.PatientUserId,
                        DoctorUserId = request.DoctorUserId,
                        Date = request.Date,
                        HealthIssue = request.HealthIssue,
                        Status = "Pending"
                    });
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in AppointmentRepository SaveAppointment : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Get Appointment By Id
        /// </summary>
        /// <param name="appointmentId"></param>
        public async Task<AppointmentResponse> GetAppointmentByAppointmentId(int appointmentId)
        {
            AppointmentResponse result = new AppointmentResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT * FROM Appointment WHERE AppointmentId = @AppointmentId";
                    result.Appointment = await connection.QueryFirstOrDefaultAsync<Model.Appointment>(query, new { AppointmentId = appointmentId });
                    if (result.Appointment != null)
                        result.Message = "Appointment Found";
                    else
                        result.Message = "Appointment Not Found With This AppointmentId";
                }
                return result;
                
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in AppointmentRepository GetAppointmentByAppointmentId : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Appointments By UserId
        /// </summary>
        /// <param name="userId"></param>
        ///  <param name="role"></param>
        public async Task<AppointmentListResponse> GetAppointmentsByUserId(int userId, string role)
        {
            AppointmentListResponse result = new AppointmentListResponse();
            try
            {
                string query = "";
                using (var connection = _context.CreateConnection())
                {
                    if(string.Equals(role,"Patient", StringComparison.OrdinalIgnoreCase))
                        query = @"SELECT * FROM Appointment WHERE PatientUserId = @UserId";
                    if (string.Equals(role, "Doctor", StringComparison.OrdinalIgnoreCase))
                        query = @"SELECT * FROM Appointment WHERE DoctorUserId = @UserId";
                    result.Appointment = (await connection.QueryAsync<Model.Appointment>(query, new { UserId = userId })).ToList();
                    if (result.Appointment?.Count > 0)
                        result.Message = result.Appointment.Count + " Appointments Found";
                    else
                        result.Message = "Appointment Not Found for This UserId";
                }
                return result;

            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in AppointmentRepository GetAppointmentsByUserId : " + ex.Message;
                return result;
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
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "UPDATE Appointment SET Status = @Status WHERE AppointmentId = @AppointmentId";
                    await connection.QueryAsync(query, new { AppointmentId = appointmentId, Status = status });
                    result.IsAffected = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in AppointmentRepository UpdateAppointmentStatus : " + ex.Message;
                return result;
            }
        }

    }
}
