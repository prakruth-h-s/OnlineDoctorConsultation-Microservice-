using System.Collections.Generic;

namespace ODC.Appointment.Model
{
    public class AppointmentListResponse : BaseResponse
    {
        public List<Model.Appointment> Appointment { get; set; }
    }
}
