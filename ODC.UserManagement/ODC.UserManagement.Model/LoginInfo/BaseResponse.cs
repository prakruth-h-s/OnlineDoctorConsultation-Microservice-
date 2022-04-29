using System.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODC.UserManagement.Model
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}