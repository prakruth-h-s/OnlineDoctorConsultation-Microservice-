using ODC.Doctor.Model;
using System.Threading.Tasks;

namespace ODC.Doctor.API
{
    public interface IDoctorService
    {
        /// <summary>
        /// Register Doctor
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RegisterResponse> RegisterDoctor(User user);

        /// <summary>
        /// Get Doctors By Speciality
        /// </summary>
        /// <param name="speciality"></param>
        Task<DoctorsListResponse> GetDoctorsBySpeciality(string speciality);

        /// <summary>
        /// Get All Doctors
        /// </summary>
        Task<DoctorsListResponse> GetAllDoctors();

        /// <summary>
        /// Get Doctors By Id
        /// </summary>
        /// <param name="userId"></param>
        Task<DoctorResponse> GetDoctorsById(int userId);

        /// <summary>
        /// Remove Doctors By Id
        /// </summary>
        /// <param name="userId"></param>
        Task<RequestResponse> RemoveDoctorById(int userId);

        /// <summary>
        /// Update Doctor User Detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RegisterResponse> UpdateDoctorUserDetail(User user);
    }
}
