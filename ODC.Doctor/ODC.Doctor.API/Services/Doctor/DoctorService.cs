using ODC.Doctor.Model;
using ODC.Doctor.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODC.Doctor.API
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        /// <summary>
        /// Register Doctor
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterResponse> RegisterDoctor(User user)
        {
            RegisterResponse result = new RegisterResponse();
            try
            {
                result = await _doctorRepository.RegisterDoctor(user);
                if(result.UserDetail != null && user.Speciality?.Count > 0)
                {
                    var specialityIds = await _doctorRepository.GetSpecialityIds(user.Speciality);
                    if(specialityIds?.Count > 0)
                        result.UserDetail.Speciality = await _doctorRepository.SaveDoctorSpeciality(result.UserDetail.UserId, specialityIds);
                    if(result.UserDetail.Speciality != null)
                    {
                        result.Message = "Doctor Registered";
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in DoctorService RegisterDoctor : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get Doctors By Speciality
        /// </summary>
        /// <param name="speciality"></param>
        public async Task<DoctorsListResponse> GetDoctorsBySpeciality(string speciality)
        {
            DoctorsListResponse result = new DoctorsListResponse();
            try
            {
                result = await _doctorRepository.GetDoctorsBySpeciality(speciality);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in DoctorService GetDoctorsBySpeciality : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get All Doctors
        /// </summary>
        public async Task<DoctorsListResponse> GetAllDoctors()
        {
            DoctorsListResponse response = new DoctorsListResponse();
            try
            {
                var result = await _doctorRepository.GetAllDoctors();
                response.Doctors = new List<User>();
                foreach(var item in result)
                {
                    var doctor = new User
                    {
                        Address = item.Address,
                        Age = item.Age,
                        Contact = item.Contact,
                        Gender = item.Gender,
                        Name = item.Name,
                        Qualification = item.Qualification,
                        Speciality = item.Speciality.Split(", ").ToList(),
                        UserId = item.UserId
                    };
                    response.Doctors.Add(doctor);

                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in DoctorService GetAllDoctors : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Get Doctors By Id
        /// </summary>
        /// <param name="userId"></param>
        public async Task<DoctorResponse> GetDoctorsById(int userId)
        {
            DoctorResponse response = new DoctorResponse();
            try
            {
                var result = await _doctorRepository.GetDoctorsById(userId);
                if (result != null)
                {
                    response.Doctor = new User()
                    {
                        Address = result.Address,
                        Age = result.Age,
                        Contact = result.Contact,
                        Gender = result.Gender,
                        Name = result.Name,
                        Qualification = result.Qualification,
                        Speciality = result.Speciality.Split(", ").ToList(),
                        UserId = result.UserId
                    };
                }
                else
                {
                    response.Message = "Doctor with this UserId Not Found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in DoctorService GetDoctorsById : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Remove Doctors By Id
        /// </summary>
        /// <param name="userId"></param>
        public async Task<RequestResponse> RemoveDoctorById(int userId)
        {
            RequestResponse response = new RequestResponse();
            try
            {
                response = await _doctorRepository.DeleteDoctorById(userId);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in DoctorService GetDoctorsById : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Update Doctor User Detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterResponse> UpdateDoctorUserDetail(User user)
        {
            RegisterResponse result = new RegisterResponse();
            try
            {
                result = await _doctorRepository.UpdateDoctorUserDetail(user);
                if (result.UserDetail != null && user.Speciality?.Count > 0)
                {
                    var specialityIds = await _doctorRepository.GetSpecialityIds(user.Speciality);
                    if(specialityIds?.Count > 0)
                        result.UserDetail.Speciality = await _doctorRepository.UpdateDoctorUserSpeciality(result.UserDetail.UserId, specialityIds);
                    if (result.UserDetail.Speciality != null)
                    {
                        result.Message = "Doctor User Detail Updated";
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in DoctorService UpdateDoctorUserDetail : " + ex.Message;
                return result;
            }
        }
    }
}
