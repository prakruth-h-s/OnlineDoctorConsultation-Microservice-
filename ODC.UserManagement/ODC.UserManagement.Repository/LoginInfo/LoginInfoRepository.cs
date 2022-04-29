using Dapper;
using ODC.UserManagement.Model;
using System;
using System.Threading.Tasks;

namespace ODC.UserManagement.Repository
{
    public class LoginInfoRepository : ILoginInfoRepository
    {
        private readonly DapperContext _context;

        public LoginInfoRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Verify Login Credentials
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        public async Task<LoginResponse> GetLoginDetails(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            try
            {                
                string query;

                using (var connection = _context.CreateConnection())
                {
                    query = "SELECT UserId FROM UserDetail WHERE UserEmail=@UserEmail";
                    var userId = await connection.QueryFirstOrDefaultAsync<int>(query, new { UserEmail = request.UserEmail });
                    if (userId > 0)
                    {
                        query = "SELECT UD.UserId,UD.UserEmail,UR.RoleId,UR.RoleName FROM UserDetail UD LEFT JOIN UserRole UR on UD.RoleId = UR.RoleId WHERE UserEmail = @UserEmail and Password = @Password";
                        response.UserDetails = await connection.QueryFirstOrDefaultAsync<UserDetails>(query, new { UserEmail = request.UserEmail, Password = request.Password });
                        if (response.UserDetails == null)
                            response.Message = "Incorrect Password";
                        else
                            response.Message = "Login Success";
                    }
                    else
                    {
                        response.Message = "No User Found with this Email";
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in LoginInfoRepository GetLoginDetails : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>

        public async Task<LoginResponse> RegisterUser(string userEmail, string password, string roleName)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                string query;

                using (var connection = _context.CreateConnection())
                {
                    query = "SELECT UserId FROM UserDetail WHERE UserEmail=@UserEmail";
                    var userId = await connection.QueryFirstOrDefaultAsync<int>(query, new { UserEmail = userEmail });
                    query = "SELECT RoleId FROM UserRole WHERE RoleName=@RoleName";
                    var roleId = await connection.QueryFirstOrDefaultAsync<int>(query, new { RoleName = roleName });
                    if (userId > 0)
                        response.Message = "User Email Already Exist";
                    else if (roleId == 0)
                        response.Message = "Please Mention Valid Role Name";
                    else
                    {
                        query = @"Insert INTO UserDetail (UserEmail, Password, RoleId) VALUES (@UserEmail, @Password, @RoleId);
                                    SELECT UD.UserId,UD.UserEmail,UR.RoleId,UR.RoleName FROM UserDetail UD LEFT JOIN UserRole UR on UD.RoleId = UR.RoleId WHERE UserId = ( SELECT CAST( SCOPE_IDENTITY() AS int ))";
                        response.UserDetails = await connection.QueryFirstOrDefaultAsync<UserDetails>(query, new { UserEmail = userEmail, Password = password, RoleId = roleId });
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in LoginInfoRepository RegisterUser : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Delete User Credential
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public async Task<RequestResponse> DeleteUserCredential(int userId)
        {
            RequestResponse result = new RequestResponse();
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "DELETE FROM UserDetail WHERE UserId = @UserId";
                    await connection.QueryAsync(query, new { UserId = userId });
                    result.IsAffected = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in LoginInfoRepository DeleteUserCredential : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Update User Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public async Task<RequestResponse> UpdateUserPassword(int userId, string password)
        {
            RequestResponse result = new RequestResponse();
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "UPDATE UserDetail SET Password = @Password WHERE UserId = @UserId";
                    await connection.QueryAsync(query, new { UserId = userId, Password = password });
                    result.IsAffected = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in LoginInfoRepository UpdateUserPassword : " + ex.Message;
                return result;
            }
        }
    }
}
