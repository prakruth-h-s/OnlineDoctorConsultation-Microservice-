using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ODC.Appointment.Model;

namespace ODC.Appointment.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DapperContext _context;

        public FeedbackRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add Feedback
        /// </summary>
        /// <param name="request"></param>
        public async Task<FeedbackResponse> AddFeedback(Model.Feedback request)
        {
            FeedbackResponse response = new FeedbackResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"INSERT INTO Feedback(AppointmentId, Rating, Review) Values(@AppointmentId, @Rating, @Review)
                                SELECT * FROM Feedback Where Id =  (SELECT CAST( SCOPE_IDENTITY() AS int ))";
                    response.Feedback = await connection.QueryFirstOrDefaultAsync<Model.Feedback>(query, new
                    {
                        AppointmentId = request.AppointmentId,
                        Rating = request.Rating,
                        Review = request.Review
                    });
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in FeedbackRepository AddFeedback : " + ex.Message;
                return response;
            }

        }

        /// <summary>
        /// Get Feedback By Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>
        public async Task<FeedbackResponse> GetFeedbackByAppointmentId(int appointmentId)
        {
            FeedbackResponse result = new FeedbackResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT * FROM Feedback WHERE AppointmentId = @AppointmentId";
                    result.Feedback = await connection.QueryFirstOrDefaultAsync<Model.Feedback>(query, new { AppointmentId = appointmentId });
                    if (result.Feedback != null)
                        result.Message = "Feedback Found";
                    else
                        result.Message = "Feedback Not Found With This AppointmentId";
                }
                return result;

            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in FeedbackRepository GetFeedbackByAppointmentId : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Feedback By UserId
        /// </summary>
        /// <param name="userId"></param>
        public async Task<FeedbackListResponse> GetFeedbacksByUserId(int userId)
        {
            FeedbackListResponse result = new FeedbackListResponse();
            try
            {
                string query = "";
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT AppointmentId FROM Appointment WHERE DoctorUserId = @UserId";
                    var appointmentId = (await connection.QueryAsync<int>(query, new { UserId = userId })).ToList();

                    query = @"SELECT * FROM Feedback WHERE AppointmentId in @AppointmentId";
                    result.Feedback = (await connection.QueryAsync<Model.Feedback>(query, new { AppointmentId = appointmentId })).ToList();

                    if (result.Feedback?.Count > 0)
                        result.Message = result.Feedback.Count + " Feedback Found";
                    else
                        result.Message = "Feedback Not Found for This UserId";
                }
                return result;

            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in FeedbackRepository GetFeedbacksByUserId : " + ex.Message;
                return result;
            }
        }
    }
}
