﻿using System.Net;

namespace ODC.Patient.Model
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}
