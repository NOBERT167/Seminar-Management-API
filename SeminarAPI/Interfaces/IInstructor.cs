using SeminarAPI.Services;

namespace SeminarAPI.Interfaces
{
    public interface IInstructor
    {
        Task<IEnumerable<InstructorData>> GetAllInstructorsAsync();
        Task<InstructorData?> GetInstructorDataAsync(string instructorNo);
        Task<bool> AddInstructorAsync(InstructorData instructor);
        Task<bool> UpdateInstructorAsync(InstructorData instructor);
        Task<bool> DeleteInstructorAsync(string instructorNo);
    }
}
