using System;
using System.Collections.Generic;
using System.Text;

namespace ODC.Doctor.Model
{
    public class Doctor
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public long Contact { get; set; }
        public string Speciality { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public short Age { get; set; }
        public string Qualification { get; set; }
    }
}
