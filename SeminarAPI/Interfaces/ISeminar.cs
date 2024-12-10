using SeminarAPI.Models;
using SeminarAPI.Services;
using System.Threading.Tasks;

namespace SeminarAPI.Interfaces
{
    public interface ISeminar
    {
        //Task<string> GetSeminarDataAsync(string docNo);
        Task<IEnumerable<SeminarDataGet>> GetAllSeminarsAsync();
        Task<SeminarDataGet?> GetSeminarDataAsync(string docNo);
        Task<bool> InsertSeminarDataAsync(string name, decimal seminarDuration, int minParticipants, int maxParticipants, decimal seminarPrice);
        Task<bool> UpdateSeminarDataAsync(string docNo, string name, decimal duration, decimal price);
        Task<bool> DeleteSeminarDataAsync(string docNo);
        //Task<AppResponse<List<AvailableSeminar>>.BaseResponse> GetAvailableSeminars();
        //Task<AppResponse<AvailableSeminar>.BaseResponse> GetAvailableSeminar(string seminarNo);
        //Task<AppResponse<SeminarRegistrationItem>.BaseResponse> CreateSeminarRegistration(string semNo, string companyNo, string participantContactNo, bool? confirmation);
        //Task<AppResponse<List<Contact>>.BaseResponse> GetContacts(string companyName);
        //Task<AppResponse<SeminarRegistrationItem>.BaseResponse> UpdateSeminarRegistration(string semHeaderNo, int lineNo, bool confirmed);
        //Task<AppResponse<List<SeminarRegistrationRespItem>>.BaseResponse> GetSeminarRegistrations(string participantContactNo, string? seminarNo = "");


    }
}
