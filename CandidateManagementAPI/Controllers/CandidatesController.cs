using CandidateManagementAPI.IServices;
using CandidateManagementAPI.Model;
using CandidateManagementAPI.NewFolder;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CandidateManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _repository;

        public CandidatesController(ICandidateRepository repository)
        {
            _repository = repository;
        }

        // POST: api/candidates/add
        [HttpPost("add")]
        public async Task<IActionResult> AddCandidateA([FromForm] CandidateDto candidate)
        {
            try
            {


                if (candidate == null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int result = await _repository.AddCandidateAsync(candidate);
                if (result == 0)
                    return BadRequest("Failed to add candidate.");
                return Ok($"Record Sucessfully Created , Created Record Id = {result}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred");
            }

        }

        // PUT: api/candidates/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCandidate([FromRoute] int id, [FromForm] CandidateDto candidate)
        {
            try
            {


                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int updatedCandidateid = await _repository.UpdateCandidateAsync(id, candidate);

                if (updatedCandidateid == 0)
                    return NotFound("Candidate not found.");

                return Ok($"Record with id = {updatedCandidateid} sucessfully Updated");// Return the updated candidate
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred");
            }
        }

        // GET: api/candidates/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById([FromRoute] int id)
        {
            var candidate = await _repository.GetCandidateByIdAsync(id);
            if (candidate == null)
                return NotFound("Candidate not found.");

            return Ok(candidate);
        }
    }
}
