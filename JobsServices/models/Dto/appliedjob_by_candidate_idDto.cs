namespace JobsServices.models.Dto
{
    public class appliedjob_by_candidate_idDto
    {
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public string? JD { get; set; }
        public string? Role { get; set; }
        public int CandidateId { get; set; }
        public string? LatestStatus { get; set; }
        public string? button_value { get; set; }


    }
}
