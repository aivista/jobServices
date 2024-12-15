namespace JobsServices.models.Entity
{
    public class upcoming_interviews
    {
        
    public int JobId { get; set; }
        public string first_name { get; set; }
        public string? middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string JobTitle { get; set; }
       
        public string HiringManagerId { get; set; }
        public int CandidateId { get; set; }


    }
}

