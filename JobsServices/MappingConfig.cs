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
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
