using ODC.Patient.Model;
using System.Threading.Tasks;

namespace ODC.Patient.API
{
    public interface IPatientService
    {
        /// <summary>
        /// Register Patient
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RegisterResponse> RegisterPatient(User user);

        /// <summary>
        /// Remove Patient User By Id
        /// </summary>
        /// <param name="userId"></param>
        Task<RequestResponse> RemovePatientById(int userId);


        /// <summary>
        /// Get Patient User By Id
        /// </summary>
        /// <param name="userId"></param>
        Task<PatientResponse> GetPatientById(int userId);

        /// <summary>
        /// Update Patient User Detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RegisterResponse> UpdatePatientUserDetail(User user);
    }
}
