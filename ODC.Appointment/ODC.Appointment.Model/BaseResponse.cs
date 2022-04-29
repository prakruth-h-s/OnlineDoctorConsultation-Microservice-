using System.Net;

namespace ODC.Appointment.Model
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}