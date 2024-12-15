using Microsoft.AspNetCore.Mvc;
using JobServices.data;
using AutoMapper;
using JobsServices.models.Dto;

namespace JobsServices.Controllers
{
    [Route("api/Jobs/[controller]")]
    [ApiController]
    public class LatestStatusController : ControllerBase
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        public LatestStatusController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        // Get all latest statuses
        [HttpGet("jobs/allcandidates")]
        public ResponseDto Get()
        {
            try
            {
                var statuses = _db.latest_Statuses.ToList();
                _response.Result = _mapper.Map<IEnumerable<latest_statusDto>>(statuses); // Map to DTO
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message; // Handle exceptions
            }
            return _response;
        }
            
        

        // Get latest statuses by JobId
        [HttpGet("ShortListed/{jobId:int}")]
        public ResponseDto GetLatestStatusesByJobId(int jobId)
        {
            try
            {
                var statuses = _db.latest_Statuses
                                  .Where(s => s.JobId == jobId)
                                  .ToList();

                if (statuses == null || !statuses.Any())
                {
                  
                    _response.Message = $"No statuses found for JobId {jobId}";
                   
                }

                _response.Result = _mapper.Map<List<latest_statusDto>>(statuses);
          
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message; // Handle exceptions
            }
            return _response;
        }

    
    
        [HttpGet("jobs/applied_candidates/{jobId:int}")]
        public ResponseDto GetDetailsCandidateApplied(int jobId)
        {
            try
            {
                var details = _db.candidate_Applieds // Replace `AnotherViewDetails` with the actual DbSet for your view
                                 .Where(d => d.JobId == jobId)
                                 .ToList();

                if (details == null || !details.Any())
                {
                    
                    _response.Message = $"No details found for JobId {jobId}";
                }
                else
                {
                    _response.Result = _mapper.Map<List<candidate_appliedDto>>(details); // Replace `AnotherViewDto` with your actual DTO


                   
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message; // Handle exceptions
            }
            return _response;
        }


    }
}
