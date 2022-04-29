namespace ODC.Appointment.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public short Rating { get; set; }
        public string Review { get; set; }
    }
}
