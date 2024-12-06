using SeminarAPI.Services;
using System.Threading.Tasks;

namespace SeminarAPI.Interfaces
{
    public interface ISeminar
    {
        //Task<string> GetSeminarDataAsync(string docNo);
        Task<SeminarData?> GetSeminarDataAsync(string docNo);
        Task<bool> InsertSeminarDataAsync(string name, decimal seminarDuration, int minParticipants, int maxParticipants);
        Task<bool> UpdateSeminarDataAsync(string docNo, string name, decimal seminarDuration);
        Task<bool> DeleteSeminarDataAsync(string docNo);
    }
}
