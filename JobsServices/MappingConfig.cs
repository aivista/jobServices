using JobsServices.models.Dto;
using JobsServices.models.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace JobsServices
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Mapping for Job and JobDto
            CreateMap<Job, JobDto>().ReverseMap();

            // Mapping for LatestStatus and LatestStatusDto
            CreateMap<latest_statuses, latest_statusDto >().ReverseMap();

            CreateMap<candidate_applied, candidate_appliedDto>().ReverseMap();

            CreateMap<upcoming_interviews, upcoming_interviewDto>().ReverseMap();

            CreateMap<appliedjob_by_candidate_id, appliedjob_by_candidate_idDto>().ReverseMap();

        }
    }
}
