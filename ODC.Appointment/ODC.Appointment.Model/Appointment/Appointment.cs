using System;

namespace ODC.Appointment.Model
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientUserId { get; set; }
        public int DoctorUserId { get; set; }
        public DateTime Date { get; set; }
        public string HealthIssue { get; set; }
        public string Status { get; set; }

    }
}
