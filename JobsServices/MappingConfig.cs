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
        }
    }
}
