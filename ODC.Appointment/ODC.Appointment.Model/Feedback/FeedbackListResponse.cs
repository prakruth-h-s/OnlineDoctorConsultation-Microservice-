using System.Collections.Generic;

namespace ODC.Appointment.Model
{
    public class FeedbackListResponse : BaseResponse
    {
        public List<Model.Feedback> Feedback { get; set; }
        public float AverageRating { get; set; }
    }
}
