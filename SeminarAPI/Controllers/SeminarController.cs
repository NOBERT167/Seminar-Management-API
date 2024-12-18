using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NAVWS;
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
                seminar.Seminar_Duration,
                seminar.Minimum_Participants,
                seminar.Maximum_Participants,
                seminar.Seminar_Price);

            return Ok("Seminar data inserted successfully.");
        }

        //Update seminar
        [HttpPut("{docNo}")]
        public async Task<IActionResult> UpdateSeminarData(string docNo, [FromBody] SeminarData seminar)
        {
            if (seminar == null)
                return BadRequest("Seminar data cannot be null.");

            await _seminarService.UpdateSeminarDataAsync(docNo, seminar.Name, seminar.Seminar_Duration, seminar.Seminar_Price);

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
                seminar.StartingDate,
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

        //Get all Seminar registration
        [HttpGet("seminarRegistrations")]
        public async Task<IActionResult> GetSeminarRegistrations()
        {
            var seminarRegistrations = await _seminarService.GetAllSeminarRegistrationAsync();
            return Ok(seminarRegistrations);
        }

        //GetSeminar registration by id
        [HttpGet("SeminarRegistrations/{regNo}")]
        public async Task<IActionResult> GetAllSeminarRegistrationById(string regNo)
        {
            var registration = await _seminarService.GetAllSeminarRegistrationByIdAsync(regNo);
            if (registration == null)
                return NotFound($"registration with registration number: {regNo} not found");
            return Ok(registration);

        }

        //Get seminar rooms
        [HttpGet("seminarRooms")]
        public async Task<IActionResult> GetAllRooms() {
            var seminarRooms = await _seminarService.GetAllRoomsAsync();
            return Ok(seminarRooms);
        }

        //Get room by id
        [HttpGet("seminarRooms/{roomNo}")]
        public async Task<IActionResult> GetAllRoomsById(string roomNo)
        {
            var room = await _seminarService.GetAllRoomsByIdAsync(roomNo);
            if(room == null) 
                return NotFound($"Room with room number: {roomNo} not found");
            return Ok(room);
        }

        //Add room
        [HttpPost("addRoom")]
        public async Task<IActionResult> AddSeminarRoom([FromBody] RoomData room)
        {
            if (room == null)
                return BadRequest("Room data cannot be null.");
            await _seminarService.AddSeminarRoomAsync(
                room.Name,
                room.Max_Participants,
                room.Internal
                );
            return Ok("Room added successfully.");
        }

        //update room
        [HttpPut("updateRoom/{No}")]
        public async Task<IActionResult> UpdateSeminarRoom(string No, [FromBody] RoomData room)
        {
            if (room == null)
                return BadRequest("Room data cannot be null.");
            await _seminarService.UpdateSeminarRoomAsync(
                room.No,
                room.Name,
                room.Max_Participants,
                room.Internal
                );
            return Ok("Room updated successfully.");
        }

        //Delete room
        [HttpDelete("deleteRoom/{No}")]
        public async Task<IActionResult> DeleteSeminarRoom(string No)
        {
            await _seminarService.DeleteSeminarRoomAsync(No);
            return Ok("Room deleted successfully.");
        }
    }
}
