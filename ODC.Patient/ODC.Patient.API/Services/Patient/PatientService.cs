using ODC.Patient.Model;
using ODC.Patient.Repository;
using System;
using System.Threading.Tasks;

namespace ODC.Patient.API
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Register Patient
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterResponse> RegisterPatient(User user)
        {
            RegisterResponse result = new RegisterResponse();
            try
            {
                result = await _patientRepository.RegisterPatient(user);
                if (result.UserDetail != null)
                {
                        result.Message = "User Registered";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in PatientService RegisterPatient : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Remove Patient User By Id
        /// </summary>
        /// <param name="userId"></param>
        public async Task<RequestResponse> RemovePatientById(int userId)
        {
            RequestResponse response = new RequestResponse();
            try
            {
                response = await _patientRepository.DeletePatientById(userId);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in PatientService RemovePatientById : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Get Patient By Id
        /// </summary>
        /// <param name="userId"></param>
        public async Task<PatientResponse> GetPatientById(int userId)
        {
            PatientResponse response = new PatientResponse();
            try
            {
                response = await _patientRepository.GetPatientById(userId);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in PatientService GetPatientById : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Update Patient User Detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterResponse> UpdatePatientUserDetail(User user)
        {
            RegisterResponse result = new RegisterResponse();
            try
            {
                result = await _patientRepository.UpdatePatientUserDetail(user);
                if (result.UserDetail != null)
                {
                    result.Message = "User Details Updated";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in PatientService UpdatePatientUserDetail : " + ex.Message;
                return result;
            }
        }
    }
}
