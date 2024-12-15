using System.ComponentModel.DataAnnotations.Schema;

namespace JobsServices.models.Entity
{
    [Table("V_HM_CANDIDATE_APPLIED", Schema = "ADANI_TALENT")] // Replace with the actual schema and view name    
    public class candidate_applied
    {
        
        public int JobId { get; set; } // Example column, replace with actual columns
        public int CandidateId { get; set; } // Example column, replace with actual columns

        public string? first_name { get; set; } // Example column, replace with actual columns
                                                // Add properties for all columns in the view
        public string? middle_name { get; set; }
        public string? last_name { get; set; }
        public string? experience { get; set; }
        public string? skills { get; set; }
        
    }
}

