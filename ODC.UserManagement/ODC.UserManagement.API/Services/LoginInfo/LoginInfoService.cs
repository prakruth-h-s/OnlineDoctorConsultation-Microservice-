using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ODC.UserManagement.Model;
using ODC.UserManagement.Repository;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ODC.UserManagement.API
{
    public class LoginInfoService : ILoginInfoService
    {
        private readonly ILoginInfoRepository _loginInfoRepository;
        private HttpClient _client;
        private IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public LoginInfoService(ILoginInfoRepository loginInfoRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _loginInfoRepository = loginInfoRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// Verify Login Credentials
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<LoginResponse> Login(string userEmail, string password)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                LoginRequest loginRequest = new LoginRequest
                {
                    UserEmail = userEmail,
                    Password = SecurePassword.Encrypt(password)
                };

                var userDetails = await _loginInfoRepository.GetLoginDetails(loginRequest);
                return userDetails;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in LoginInfoService Login : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public async Task<LoginResponse> SignUp(string userEmail, string password, User userDetail)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                response = await _loginInfoRepository.RegisterUser(userEmail, SecurePassword.Encrypt(password), userDetail.RoleName);
                if(response.UserDetails != null)
                {
                    userDetail.UserId = response.UserDetails.UserId;

                    #region call Doctor microservice

                    if (string.Equals(userDetail.RoleName,"Doctor", StringComparison.OrdinalIgnoreCase))
                    {
                        var apiUrl = _configuration["DoctorMicroServiceURL"].ToString() + "api/Doctor/RegisterDoctor";
                        var result = await GetClient().PostAsJsonAsync(apiUrl, userDetail);
                        RegisterResponse apiResponse = JsonConvert.DeserializeObject<RegisterResponse>(await result.Content.ReadAsStringAsync());
                        if (result.IsSuccessStatusCode)
                            response.Message = apiResponse.Message;
                        else
                            response.Message = apiResponse?.Message;
                    }
                    #endregion

                    #region call Patient microservice
                    else if (string.Equals(userDetail.RoleName, "Patient", StringComparison.OrdinalIgnoreCase))
                    {
                        var apiUrl = _configuration["PatientMicroServiceURL"].ToString() + "api/Patient/RegisterPatient";
                        var result = await GetClient().PostAsJsonAsync(apiUrl, userDetail);
                        RegisterResponse apiResponse = JsonConvert.DeserializeObject<RegisterResponse>(await result.Content.ReadAsStringAsync());
                        if (result.IsSuccessStatusCode)
                            response.Message = apiResponse.Message;
                        else
                            response.Message = apiResponse?.Message;
                    }
                    #endregion

                    else if (string.Equals(userDetail.RoleName, "Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        response.Message = "User Registration Successfull";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in LoginInfoService SignUp : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<RequestResponse> DeleteUser(string userEmail, string password)
        {
            RequestResponse result = new RequestResponse();
            result.IsAffected = false;
            try
            {
                LoginRequest loginRequest = new LoginRequest
                {
                    UserEmail = userEmail,
                    Password = SecurePassword.Encrypt(password)
                };
                var response = await _loginInfoRepository.GetLoginDetails(loginRequest);
                result.Message = response.Message;
                
                if(string.Equals(result.Message, "Login Success", StringComparison.OrdinalIgnoreCase))
                {
                    result = await _loginInfoRepository.DeleteUserCredential(response.UserDetails.UserId);
                    if(result.IsAffected)
                    {
                        #region call to doctor microservice

                        if (string.Equals(response.UserDetails.RoleName, "Doctor", StringComparison.OrdinalIgnoreCase))
                        {
                            var apiUrl = _configuration["DoctorMicroServiceURL"].ToString() + "api/Doctor/Id?userId=" + response.UserDetails.UserId;
                            var microserviceResponse = await GetClient().DeleteAsync(apiUrl);
                            RequestResponse apiResponse = JsonConvert.DeserializeObject<RequestResponse>(await microserviceResponse.Content.ReadAsStringAsync());
                            if (microserviceResponse.IsSuccessStatusCode)
                                result.Message = apiResponse.Message;
                            else
                                result.Message = apiResponse?.Message;
                        }

                        #endregion

                        #region call to patient microservice

                        else if (string.Equals(response.UserDetails.RoleName, "Patient", StringComparison.OrdinalIgnoreCase))
                        {
                            result.Message = "User Credential Deleted";
                            var apiUrl = _configuration["PatientMicroServiceURL"].ToString() + "api/Patient/Id?userId=" + response.UserDetails.UserId;
                            var microserviceResponse = await GetClient().DeleteAsync(apiUrl);
                            RequestResponse apiResponse = JsonConvert.DeserializeObject<RequestResponse>(await microserviceResponse.Content.ReadAsStringAsync());
                            if (microserviceResponse.IsSuccessStatusCode)
                                result.Message = apiResponse.Message;
                            else
                                result.Message = apiResponse?.Message;
                        }

                        #endregion

                        else if(string.Equals(response.UserDetails.RoleName, "Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            result.Message = "User Credential Deleted";
                        }
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                result.Message = "Exception occured in LoginInfoService DeleteUser : "+ ex.Message;
                return result;   
            }
        }
        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<RequestResponse> UpdatePassword(string userEmail, string password, string newPassword)
        {
            RequestResponse result = new RequestResponse();
            result.IsAffected = false;
            try
            {
                LoginRequest loginRequest = new LoginRequest
                {
                    UserEmail = userEmail,
                    Password = SecurePassword.Encrypt(password)
                };
                var response = await _loginInfoRepository.GetLoginDetails(loginRequest);
                result.Message = response.Message;

                if (string.Equals(result.Message, "Login Success", StringComparison.OrdinalIgnoreCase))
                {
                    result = await _loginInfoRepository.UpdateUserPassword(response.UserDetails.UserId, SecurePassword.Encrypt(newPassword));
                    if(result.IsAffected == true)
                        result.Message = "Password Updated";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in LoginInfoService UpdatePassword : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Client 
        /// </summary>
        /// <returns></returns>
        private HttpClient GetClient()
        {
            try
            {
                _client = _httpClientFactory.CreateClient();
                _client.Timeout = TimeSpan.FromMinutes(1);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return _client;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
