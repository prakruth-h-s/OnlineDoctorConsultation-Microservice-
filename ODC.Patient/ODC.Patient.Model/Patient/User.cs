using System.Collections.Generic;

namespace ODC.Patient.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public long Contact { get; set; }
        public string RoleName { get; set; }
        public List<string> Speciality { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public short Age { get; set; }
        public string Qualification { get; set; }
    }
}
