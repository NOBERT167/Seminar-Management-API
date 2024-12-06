using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeminarAPI.Services;

namespace SeminarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeminarController : ControllerBase
    {
        private readonly SeminarService _seminarService;

        public SeminarController(SeminarService seminarService)
        {
            _seminarService = seminarService;
        }

        // GET: api/Seminar/{docNo}
        [HttpGet("{docNo}")]
        public async Task<IActionResult> GetSeminarData(string docNo)
        {
            var seminar = await _seminarService.GetSeminarDataAsync(docNo);
            if (seminar == null)
            {
                return NotFound($"Seminar with Document No '{docNo}' not found.");
            }
            return Ok(seminar);
        }

        // POST: api/Seminar
        [HttpPost]
        public async Task<IActionResult> InsertSeminarData([FromBody] SeminarData seminar)
        {
            if (seminar == null)
            {
                return BadRequest("Seminar data cannot be null.");
            }

            var success = await _seminarService.InsertSeminarDataAsync(
                seminar.Name,
                seminar.SeminarDuration,
                seminar.MinimumParticipants,
                seminar.MaximumParticipants);

            if (!success)
            {
                return StatusCode(500, "An error occurred while inserting seminar data.");
            }

            return Ok("Seminar data inserted successfully.");
        }

        // PUT: api/Seminar/{docNo}
        [HttpPut("{docNo}")]
        public async Task<IActionResult> UpdateSeminarData(string docNo, [FromBody] SeminarData seminar)
        {
            if (seminar == null)
            {
                return BadRequest("Seminar data cannot be null.");
            }

            var success = await _seminarService.UpdateSeminarDataAsync(docNo, seminar.Name, seminar.SeminarDuration);

            if (!success)
            {
                return StatusCode(500, "An error occurred while updating seminar data.");
            }

            return Ok("Seminar data updated successfully.");
        }

        // DELETE: api/Seminar/{docNo}
        [HttpDelete("{docNo}")]
        public async Task<IActionResult> DeleteSeminarData(string docNo)
        {
            var success = await _seminarService.DeleteSeminarDataAsync(docNo);

            if (!success)
            {
                return StatusCode(500, "An error occurred while deleting seminar data.");
            }

            return Ok("Seminar data deleted successfully.");
        }
    }
}
