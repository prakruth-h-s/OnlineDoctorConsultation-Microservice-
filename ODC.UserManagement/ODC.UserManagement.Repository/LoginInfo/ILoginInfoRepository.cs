using ODC.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ODC.UserManagement.Repository
{
    public interface ILoginInfoRepository
    {
        /// <summary>
        /// Verify Login Credentials
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<LoginResponse> GetLoginDetails(LoginRequest request);

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<LoginResponse> RegisterUser(string userEmail, string password, string roleName);

        /// <summary>
        /// Delete User Credential
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RequestResponse> DeleteUserCredential(int userId);

        /// <summary>
        /// Update User Password
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RequestResponse> UpdateUserPassword(int userId, string newPassword);
    }
}
