using System.Collections.Generic;

namespace ODC.Appointment.Model
{
    public class PrescriptionListResponse : BaseResponse
    {
        public List<MedicalPrescription> Prescription { get; set; }
    }
}
