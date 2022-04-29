using ODC.UserManagement.Model;
using System.Threading.Tasks;

namespace ODC.UserManagement.API
{
    public interface ILoginInfoService
    {
        /// <summary>
        /// Verify Login Credentials
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<LoginResponse> Login(string userEmail, string password);

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        Task<LoginResponse> SignUp(string userEmail, string password, User userDetail);

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<RequestResponse> DeleteUser(string userEmail, string password);

        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<RequestResponse> UpdatePassword(string userEmail, string password, string newPassword);
    }
}
