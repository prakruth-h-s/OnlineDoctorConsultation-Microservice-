using ODC.Patient.Model;
using System.Threading.Tasks;

namespace ODC.Patient.Repository
{
    public interface IPatientRepository
    {
        /// <summary>
        /// Register Patient
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<RegisterResponse> RegisterPatient(User user);

        /// <summary>
        /// Remove patient user By Id
        /// </summary>
        /// <param name="userId"></param>
        public Task<RequestResponse> DeletePatientById(int userId);

        /// <summary>
        /// Get Patient User By Id
        /// </summary>
        /// <param name="userId"></param>
        public Task<PatientResponse> GetPatientById(int userId);

        /// <summary>
        /// Update Patient User Detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<RegisterResponse> UpdatePatientUserDetail(User user);
    }
}
