using ODC.Doctor.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODC.Doctor.Repository
{
    public interface IDoctorRepository
    {
        /// <summary>
        /// Register Doctor
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<RegisterResponse> RegisterDoctor(User user);

        /// <summary>
        /// Get Speciality Ids
        /// </summary>
        /// <param name="speciality"></param>
        /// <returns></returns>
        public Task<List<int>> GetSpecialityIds(List<String> speciality);

        /// <summary>
        /// Save Doctor Speciality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="specialityIds"></param>
        /// <returns></returns>
        public Task<List<string>> SaveDoctorSpeciality(int userId, List<int> specialityIds);

        /// <summary>
        /// Get Doctors By Speciality
        /// </summary>
        /// <param name="speciality"></param>
        public Task<DoctorsListResponse> GetDoctorsBySpeciality(string speciality);

        /// <summary>
        /// Get All Doctors
        /// </summary>
        public Task<List<Model.Doctor>> GetAllDoctors();

        /// <summary>
        /// Get Doctors By Id
        /// </summary>
        /// <param name="userId"></param>
        public Task<Model.Doctor> GetDoctorsById(int userId);

        /// <summary>
        /// Remove Doctors By Id
        /// </summary>
        /// <param name="userId"></param>
        public Task<RequestResponse> DeleteDoctorById(int userId);

        /// <summary>
        /// Update Doctor User Detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<RegisterResponse> UpdateDoctorUserDetail(User user);

        /// <summary>
        /// Update Doctor User Speciality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="specialityIds"></param>
        /// <returns></returns>
        public Task<List<string>> UpdateDoctorUserSpeciality(int userId, List<int> specialityIds);

    }
}
