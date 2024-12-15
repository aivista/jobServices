using Microsoft.AspNetCore.Mvc;
using JobServices.data;
using AutoMapper;
using JobsServices.models.Dto;
using Microsoft.EntityFrameworkCore;

namespace JobsServices.Controllers
{
    [Route("api/")]
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
        [HttpGet("jobs/shortlisted_candidates/{jobId:int}")]
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
        [HttpGet("upcoming_interview/{jobId}/{hiringManagerId}")]
        public async Task<ResponseDto> GetCandidateByJobIdAndHiringManagerId(int jobId, string hiringManagerId)
        {
            try
            {
                // Filter by JobId and HiringManagerId
                var candidate = await _db.upcoming_Interviews
                                .Where(c => c.JobId == jobId && c.HiringManagerId == hiringManagerId)
                                .FirstOrDefaultAsync();

                if (candidate == null)
                {
                    
                    _response.Message = "Data Not Found";
                   
                }

               
                _response.Result = _mapper.Map<upcoming_interviewDto>(candidate);
               
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
        [HttpGet("jobs/appliedjobs_by_candidate/{jobId:int}")]
        public ResponseDto GetJobsCandidateApplied(int jobId)
        {
            try
            {
                var details = _db.appliedjob_By_Candidate_Ids // Replace `AnotherViewDetails` with the actual DbSet for your view
                                 .Where(d => d.JobId == jobId)
                                 .ToList();

                if (details == null || !details.Any())
                {

                    _response.Message = $"No details found for JobId {jobId}";
                }
                else
                {
                    _response.Result = _mapper.Map<List<appliedjob_by_candidate_idDto>>(details); // Replace `AnotherViewDto` with your actual DTO



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
