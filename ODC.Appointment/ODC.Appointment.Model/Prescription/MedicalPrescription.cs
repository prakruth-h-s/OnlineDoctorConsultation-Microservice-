namespace ODC.Appointment.Model
{
    public class MedicalPrescription
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Prescription { get; set; }
    }
}
