namespace JobsServices.models.Dto
{
    public class latest_statusDto
{
    public int JobId { get; set; } // Example column, replace with actual columns
    public int CandidateId { get; set; }
    public string? first_name { get; set; } // Example column, replace with actual columns
                                          // Add properties for all columns in the view
    public string? middle_name { get; set; }
    public string? last_name { get; set; }
    public string? latestrole { get; set; }
    public string? education { get; set; }
    public string? Star { get; set; }
    public string? experience { get; set; }
    public string? skills { get; set; }
    public string? LatestStatus { get; set; }
}
}

