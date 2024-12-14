using AutoMapper;
using JobServices.data;

using JobsServices.models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JobServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public JobsController(AppDBContext db, IMapper mapper)
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
    }
}