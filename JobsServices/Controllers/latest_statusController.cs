using Microsoft.AspNetCore.Mvc;
using JobServices.data;
using AutoMapper;
using JobsServices.models.Dto;

namespace JobsServices.Controllers
{
    [Route("api/shortlisted/[controller]")]
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
        [HttpGet]
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
        [HttpGet("job/{jobId:int}")]
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
       
        
    }
}
