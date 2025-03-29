using System.Collections.Generic;
using AutoMapper;
using JobServices.data;
using JobsServices.models.Dto;
using JobsServices.models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobServices.Controllers
{
    [Route("api/[controller]/jobs")]
    [ApiController]
    public class JobsServicesController : ControllerBase
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public JobsServicesController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet("{hiringManagerId}")]
        public IActionResult GetJobsByHiringManager(string hiringManagerId)
        {
            try
            {
                var jobs = _db.Jobs
                    .Where(job => job.HiringManagerId == hiringManagerId)
                    .ToList();

                if (jobs == null || jobs.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No jobs found for the specified HiringManagerId.";
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<List<JobDto>>(jobs);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }
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
                    return _response;
                }

                // Define custom ordering for statuses
                var statusOrder = new Dictionary<string, int>
        {
            { "Selected", 0 },
            { "Interview Scheduled", 1 },
            { "Assessment Pending", 2 },
            { "AI Screening Pending", 3 },
            { "Not Selected in Interview", 4 }
        };

                var orderedStatuses = statuses
                    .OrderBy(s => statusOrder.ContainsKey(s.LatestStatus) ? statusOrder[s.LatestStatus] : 3) // Sort by status order
                    .ThenByDescending(s => s.Star) // Sort by star rating descending
                    .ToList();

                _response.Result = _mapper.Map<List<latest_statusDto>>(orderedStatuses);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message; // Handle exceptions
            }
            return _response;
        }
        [HttpGet("appliedJobs/{jobId:int}")]
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
        [HttpGet("upcomingInterview/{jobId}/{hiringManagerId}")]
        public async Task<ResponseDto> GetCandidateByJobIdAndHiringManagerId(int jobId, string hiringManagerId)
        {
            try
            {
                // Filter by JobId and HiringManagerId


                IEnumerable<upcoming_interviews> candidate = await _db.upcoming_Interviews
                                .Where(c => c.JobId == jobId && c.HiringManagerId == hiringManagerId).ToListAsync();

                if (candidate == null)
                {

                    _response.Message = "Data Not Found";

                }


                _response.Result = _mapper.Map<IEnumerable<upcoming_interviewDto>>(candidate);


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet("appliedjobsByCandidate/{candidateID:int}")]
        public ResponseDto GetJobsCandidateApplied(int candidateID)
        {
            try
            {
                var details = _db.appliedjob_By_Candidate_Ids // Replace `AnotherViewDetails` with the actual DbSet for your view
                                 .Where(d => d.CandidateId == candidateID)
                                 .ToList();

                if (details == null || !details.Any())
                {

                    _response.Message = $"No details found for JobId {candidateID}";
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

        [HttpPost("getInterViewSechdule")]
        public async Task<ResponseDto> GetInterviewSechdule([FromBody] RequestDTO request)
        {
            try {

                if (request == null || request.jobId == 0 || request.CandidateId == 0)
                {
                    _response.Message = "Invalid input data.";
                    _response.IsSuccess = false;
                    return _response;
                }

                IEnumerable<upcoming_interviews> candidate = await _db.upcoming_Interviews
                                .Where(c => c.JobId == request.jobId && c.CandidateId == request.CandidateId).ToListAsync();
                if (candidate == null)
            {

                _response.Message = "Data Not Found";

            }


            _response.Result = _mapper.Map<IEnumerable<upcoming_interviewDto>>(candidate);


        }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

    }
}