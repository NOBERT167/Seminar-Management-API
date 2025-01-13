using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.WsTrust;
using NAVWS;
using SeminarAPI.Services;


namespace SeminarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _instructorService;

        public InstructorController(InstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        //Get all instructors
        [HttpGet("instructors")]
        public async Task<IActionResult> GetAllInstructors()
        {
            var allInstructors = await _instructorService.GetAllInstructorsAsync();
            return Ok(allInstructors);
        }

        //Get instructor by id
        [HttpGet("instructor/{instructorNo}")] 
        public async Task<IActionResult> GetInstructorDataAsync(string instructorNo)
        {
            var instructor = await _instructorService.GetInstructorDataAsync(instructorNo);
            if (instructor == null)
                return NotFound($"Instructor with id: {instructorNo} not found.");
            return Ok(instructor);
        }

        //Add instructor
        [HttpPost("add")]
        public async Task<IActionResult> AddInstructor([FromBody] InstructorData instructor)
        {
            if (instructor == null)
                return BadRequest("Instructor data cannot be null.");

            await _instructorService.AddInstructorAsync(instructor);
            return Ok("Instructor added successfully.");
        }

        //update instructor
        [HttpPut("update/{instructorNo}")]
        public async Task<IActionResult> UpdateInstructor(string instructorNo, [FromBody] InstructorData instructor)
        {
            if (instructor == null)
                return BadRequest("Instructor data cannot be null.");

            await _instructorService.UpdateInstructorAsync(instructor);
            return Ok("Instructor updated successfully.");
        }

        [HttpDelete("delete/{instructorNo}")]
        public async Task<IActionResult> DeleteInstructor(string instructorNo)
        {
            if (string.IsNullOrEmpty(instructorNo))
                return BadRequest("Resource No. cannot be null or empty.");

            await _instructorService.DeleteInstructorAsync(instructorNo);
            return Ok("Instructor deleted successfully.");
        }
    }
}
