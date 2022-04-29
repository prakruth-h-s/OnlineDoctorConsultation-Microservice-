using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ODC.Doctor.Model
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}
