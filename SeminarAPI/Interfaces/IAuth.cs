using System.Threading.Tasks;

namespace SeminarAPI.Interfaces
{
    public interface IAuth
    {
        // User specific methods
        Task<string> RegisterUserAsync(string username, string password, string email, string name);
        Task<string> LoginUserAsync(string username, string password);
        Task<string> UpdateUserAsync(string username, string name);

        // Admin-specific methods
        Task<string> RegisterAdminAsync(string username, string password, string email, string name);
        Task<string> LoginAdminAsync(string username, string password);
    }
}
