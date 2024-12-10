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

        [HttpGet]
        public async Task<IActionResult> GetAllSeminars()
        {
            var seminars = await _seminarService.GetAllSeminarsAsync();
            return Ok(seminars);
        }

        [HttpGet("{docNo}")]
        public async Task<IActionResult> GetSeminarData(string docNo)
        {
            var seminar = await _seminarService.GetSeminarDataAsync(docNo);
            if (seminar == null)
                return NotFound($"Seminar with Document No '{docNo}' not found.");
            return Ok(seminar);
        }

        [HttpPost]
        public async Task<IActionResult> InsertSeminarData([FromBody] SeminarData seminar)
        {
            if (seminar == null)
                return BadRequest("Seminar data cannot be null.");

            await _seminarService.InsertSeminarDataAsync(
                seminar.Name,
                seminar.SeminarDuration,
                seminar.MinimumParticipants,
                seminar.MaximumParticipants,
                seminar.SeminarPrice);

            return Ok("Seminar data inserted successfully.");
        }

        [HttpPut("{docNo}")]
        public async Task<IActionResult> UpdateSeminarData(string docNo, [FromBody] SeminarData seminar)
        {
            if (seminar == null)
                return BadRequest("Seminar data cannot be null.");

            await _seminarService.UpdateSeminarDataAsync(docNo, seminar.Name, seminar.SeminarDuration, seminar.SeminarPrice);

            return Ok("Seminar data updated successfully.");
        }

        [HttpDelete("{docNo}")]
        public async Task<IActionResult> DeleteSeminarData(string docNo)
        {
            await _seminarService.DeleteSeminarDataAsync(docNo);
            return Ok("Seminar data deleted successfully.");
        }

        //Register seminar endpoint
        [HttpPost("register")]
        public async Task<ActionResult> RegisterSeminar([FromBody] SeminarRegData seminar) 
        {
            if (seminar == null) 
                return BadRequest("Seminar registration data cannot be null.");
            await _seminarService.InsertSeminarRegDataAsync(
                seminar.SeminarNo,
                seminar.PersonNo,
                seminar.RoomNo
                ); 
            return Ok("Seminar registered successfully.");
        }

        //Register participant endpoint
        [HttpPost("registerParticipant")]
        public async Task<ActionResult> RegisterParticipantAsync([FromBody] SeminarParticipant participant)
        {
            if (participant == null)
                return BadRequest("Participant registration data cannot be null.");
            await _seminarService.RegisterParticipantAsync(
                participant.SeminarNo,
                participant.CompanyNo,
                participant.ParticipantNo
                );
            return Ok("Participant registered successfully.");
        }
    }
}
