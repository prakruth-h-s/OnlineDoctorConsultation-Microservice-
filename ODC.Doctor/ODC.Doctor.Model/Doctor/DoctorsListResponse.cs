using System;
using System.Collections.Generic;
using System.Text;

namespace ODC.Doctor.Model
{
    public class DoctorsListResponse : BaseResponse
    {
        public List<User> Doctors { get; set; }
    }
}
