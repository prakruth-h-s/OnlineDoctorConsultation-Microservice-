using ODC.Patient.Model;
using System;
using System.Threading.Tasks;
using Dapper;

namespace ODC.Patient.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DapperContext _context;

        public PatientRepository(DapperContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Register Patient
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterResponse> RegisterPatient(User user)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"INSERT INTO PatientDetail(UserId, Name, Contact, Address, Gender, Age, Qualification) Values(@UserId, @Name, @Contact, @Address, @Gender, @Age, @Qualification)
                                SELECT * FROM PatientDetail Where UserId = @UserId";
                    response.UserDetail = await connection.QueryFirstOrDefaultAsync<User>(query, new
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Contact = user.Contact,
                        Address = user.Address,
                        Gender = user.Gender,
                        Age = user.Age,
                        Qualification = user.Qualification
                    });
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in PatientRepository RegisterPatient : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Remove Patient user By Id
        /// </summary>
        /// <param name="userId"></param>
        public async Task<RequestResponse> DeletePatientById(int userId)
        {
            RequestResponse result = new RequestResponse();
            result.IsAffected = false;
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = "SELECT * from PatientDetail WHERE UserId = @UserID";
                    var doctor = await connection.QueryFirstOrDefaultAsync<User>(query, new { UserId = userId });
                    if (doctor != null)
                    {
                        query = @"DELETE FROM PatientDetail WHERE UserId = @UserId";

                        await connection.QueryAsync(query, new { UserId = userId });
                        result.IsAffected = true;
                        result.Message = "Patient User Removed";
                    }
                    else
                    {
                        result.Message = "No Patient User Available with this UserId to Remove";
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in PatientRepository DeletePatientById : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Patient User By Id
        /// </summary>
        /// <param name="userId"></param>
        public async Task<PatientResponse> GetPatientById(int userId)
        {
            PatientResponse result = new PatientResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT * FROM PatientDetail WHERE UserId = @UserId";
                    result.Patient = await connection.QueryFirstOrDefaultAsync<User>(query, new { UserId = userId });
                    if (result.Patient != null)
                        result.Message = "User Found";
                    else
                        result.Message = "User Not Found With This UserId";
                }
                return result;
                
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in PatientRepository GetPatientById : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Update Patient User Detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterResponse> UpdatePatientUserDetail(User user)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"UPDATE PatientDetail SET Name = @Name, Contact = @Contact, Address = @Address, Gender = @Gender, Age = @Age, Qualification = @Qualification Where UserId = @UserId;
                                SELECT * FROM PatientDetail Where UserId = @UserId";
                    response.UserDetail = await connection.QueryFirstOrDefaultAsync<User>(query, new
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Contact = user.Contact,
                        Address = user.Address,
                        Gender = user.Gender,
                        Age = user.Age,
                        Qualification = user.Qualification
                    });
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in PatientRepository UpdatePatientUserDetail : " + ex.Message;
                return response;
            }
        }

    }
}
