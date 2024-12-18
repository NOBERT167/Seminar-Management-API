using SeminarAPI.Models;
using SeminarAPI.Services;
using System.Threading.Tasks;

namespace SeminarAPI.Interfaces
{
    public interface ISeminar
    {
        //Task<string> GetSeminarDataAsync(string docNo);
        Task<IEnumerable<SeminarData>> GetAllSeminarsAsync();
        Task<SeminarData?> GetSeminarDataAsync(string docNo);
        Task<bool> InsertSeminarDataAsync(string name, decimal seminarDuration, int minParticipants, int maxParticipants, decimal seminarPrice);
        Task<bool> UpdateSeminarDataAsync(string docNo, string name, decimal duration, decimal price);
        Task<bool> DeleteSeminarDataAsync(string docNo);

        //Seminar registration
        Task<IEnumerable<SeminarRegistrationData>> GetAllSeminarRegistrationAsync();
        Task<SeminarRegistrationData?> GetAllSeminarRegistrationByIdAsync(string regNo);

        //To be fixed
        Task<bool> InsertSeminarRegDataAsync(string seminarNo, DateTime startingDate, string personNo, string roomNo);
        Task<bool> RegisterParticipantAsync(string docNo, string companyNo, string participantNo);

        //Room interfaces
        Task<IEnumerable<RoomData>> GetAllRoomsAsync();
        Task<RoomData?> GetAllRoomsByIdAsync(string No);
        Task<bool> AddSeminarRoomAsync(string name, int maxParticipants, bool Internal);
        Task<bool> UpdateSeminarRoomAsync(string No, string name, int maxParticipants, bool Internal);
        Task<bool> DeleteSeminarRoomAsync(string No);
    }
}
