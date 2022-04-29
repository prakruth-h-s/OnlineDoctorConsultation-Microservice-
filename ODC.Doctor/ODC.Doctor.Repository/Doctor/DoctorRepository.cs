using ODC.Doctor.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ODC.Doctor.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DapperContext _context;

        public DoctorRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register Doctor
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterResponse> RegisterDoctor(User user)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"INSERT INTO DoctorDetail(UserId, Name, Contact, Address, Gender, Age, Qualification) Values(@UserId, @Name, @Contact, @Address, @Gender, @Age, @Qualification)
                                SELECT * FROM DoctorDetail Where UserId = @UserId";
                    response.UserDetail = await connection.QueryFirstOrDefaultAsync<User>(query, new
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Contact = user.Contact,
                        Address = user.Address,
                        Gender = user.Gender,
                        Age = user.Age,
                        Qualification = user.Qualification
                    });
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in DoctorRepository RegisterDoctor : " + ex.Message;
                return response;
            }
        }


        /// <summary>
        /// Get Speciality Ids
        /// </summary>
        /// <param name="speciality"></param>
        /// <returns></returns>
        public async Task<List<int>> GetSpecialityIds(List<string> speciality)
        {
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"Select SpecialityId FROM Speciality Where SpecialityName in @SpecialityName";
                    var result = (await connection.QueryAsync<int>(query, new { SpecialityName = speciality })).ToList();
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save Doctor Speciality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="specialityIds"></param>
        /// <returns></returns>
        public async Task<List<string>> SaveDoctorSpeciality(int userId, List<int> specialityIds)
        {
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"INSERT INTO DoctorSpecialities(UserId,SpecialityId) Values";
                    foreach (var value in specialityIds)
                    {
                        query = query + "(@UserId, " + value + " ),";
                    }
                    query = query.Substring(0, query.Length - 1) + "; SELECT SpecialityName FROM Speciality Where SpecialityId in @Ids";
                    var result = (await connection.QueryAsync<string>(query, new { UserId = userId, Ids = specialityIds })).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT DD.* FROM DoctorDetail DD LEFT JOIN DoctorSpecialities DS ON DD.UserId = DS.UserId INNER JOIN Speciality S ON DS.SpecialityId = S.SpecialityId and S.SpecialityName = @SpecialityName";

                    result.Doctors = (await connection.QueryAsync<User>(query, new { SpecialityName = speciality })).ToList();
                    if (result.Doctors?.Count == 0)
                        result.Message = "Doctors With this speciality Not Found";
                    else
                        result.Message = result.Doctors.Count + " Doctors With speciality " + speciality + " Found";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in DoctorRepository GetDoctorsBySpeciality : " + ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Get All Doctors
        /// </summary>
        public async Task<List<Model.Doctor>> GetAllDoctors()
        {
            List<Model.Doctor> result = new List<Model.Doctor>();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT 
                                DD.*, STUFF((
                                SELECT ', ' + SpecialityName  
                                FROM Speciality S Inner Join DoctorSpecialities DS on S.SpecialityId = DS.SpecialityId
                                WHERE (DS.UserId = DD.UserID) 
                                FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
                                ,1,2,'') AS Speciality
                                FROM DoctorDetail DD left join DoctorSpecialities DS on DD.UserId = DS.UserId 
                                GROUP BY DD.UserID, DD.Name, DD.Contact, Address, Gender, Age, Qualification";

                    result = (await connection.QueryAsync<Model.Doctor>(query,null)).ToList();
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Doctors By Id
        /// </summary>
        /// <param name="userId"></param>
        public async Task<Model.Doctor> GetDoctorsById(int userId)
        {
            Model.Doctor result = new Model.Doctor();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT 
                                DD.*, STUFF((
                                SELECT ', ' + SpecialityName  
                                FROM Speciality S Inner Join DoctorSpecialities DS on S.SpecialityId = DS.SpecialityId
                                WHERE (DS.UserId = DD.UserID) 
                                FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
                                ,1,2,'') AS Speciality
                                FROM DoctorDetail DD left join DoctorSpecialities DS on DD.UserId = DS.UserId WHERE DD.UserId = @UserId
                                GROUP BY DD.UserID, DD.Name, DD.Contact, Address, Gender, Age, Qualification";

                    result = await connection.QueryFirstOrDefaultAsync<Model.Doctor>(query, new { UserId = userId });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove Doctors By Id
        /// </summary>
        /// <param name="userId"></param>
        public async Task<RequestResponse> DeleteDoctorById(int userId)
        {
            RequestResponse result = new RequestResponse();
            result.IsAffected = false;
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = "SELECT * from DoctorDetail WHERE UserId = @UserID";
                    var doctor = await connection.QueryFirstOrDefaultAsync<Model.Doctor>(query, new { UserId = userId });
                    if(doctor != null)
                    {
                        query = @"DELETE FROM DoctorSpecialities WHERE UserId = @UserId;
                              DELETE FROM DoctorDetail WHERE UserId = @UserId";

                        await connection.QueryAsync(query, new { UserId = userId });
                        result.IsAffected = true;
                        result.Message = "Doctor Removed";
                    }
                    else
                    {
                        result.Message = "No Doctor Available with this UserId to Remove";
                    }
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Exception occured in DoctorRepository DeleteDoctorById : " + ex.Message;
                return result;
            }
        }
        /// <summary>
        /// Update Doctor User Detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterResponse> UpdateDoctorUserDetail(User user)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"UPDATE DoctorDetail SET Name = @Name, Contact = @Contact, Address = @Address, Gender = @Gender, Age = @Age, Qualification = @Qualification Where UserId = @UserId;
                                SELECT * FROM DoctorDetail Where UserId = @UserId";
                    response.UserDetail = await connection.QueryFirstOrDefaultAsync<User>(query, new
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Contact = user.Contact,
                        Address = user.Address,
                        Gender = user.Gender,
                        Age = user.Age,
                        Qualification = user.Qualification
                    });
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "Exception occured in DoctorRepository UpdateDoctorUserDetail : " + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Update Doctor User Speciality
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="specialityIds"></param>
        /// <returns></returns>
        public async Task<List<string>> UpdateDoctorUserSpeciality(int userId, List<int> specialityIds)
        {
            try
            {
                string query;
                using (var connection = _context.CreateConnection())
                {
                    query = @"SELECT SpecialityId FROM DoctorSpecialities WHERE UserId = @UserId;";
                    var oldSpecialityIds = (await connection.QueryAsync<int>(query, new { UserId = userId})).ToList();
                    List<int> MatchIds = new List<int>();
                    foreach (var oldId in oldSpecialityIds)
                    {
                        foreach(var newId in specialityIds)
                        {
                            if(newId == oldId)
                            {
                                MatchIds.Add(newId);
                            }
                        }
                    }
                    var insertIds = specialityIds.Where(x => !MatchIds.Contains(x)).ToList();
                    var deleteIds = oldSpecialityIds.Where(x => !MatchIds.Contains(x)).ToList();
                    query = @"INSERT INTO DoctorSpecialities(UserId,SpecialityId) Values";
                    foreach (var value in insertIds)
                    {
                        query = query + "(@UserId, " + value + " ),";
                    }
                    query = query.Substring(0, query.Length - 1) + "; DELETE FROM DoctorSpecialities WHERE UserId = @UserId and SpecialityId IN @DeleteIds;" +
                        "SELECT SpecialityName FROM Speciality Where SpecialityId in @Ids";
                    var result = (await connection.QueryAsync<string>(query, new { UserId = userId, Ids = specialityIds , DeleteIds  = deleteIds})).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
