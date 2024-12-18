using System.Threading.Tasks;

namespace SeminarAPI.Interfaces
{
    public interface IAuth
    {
        Task<string> RegisterUserAsync(string username, string password, string email, string name);
        Task<string> LoginUserAsync(string username, string password);
        Task<string> UpdateUserAsync(string username, string name);
    }
}
