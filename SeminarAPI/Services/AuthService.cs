using System.Threading.Tasks;
using SeminarAPI.Interfaces;
using SeminarAPI.Models;

namespace SeminarAPI.Services
{
    public class AuthService : IAuth
    {
        private readonly Credentials _credentials;

        public AuthService(Credentials credentials)
        {
            _credentials = credentials;
        }

        public async Task<string> RegisterUserAsync(string username, string password, string email, string name)
        {
            var client = _credentials.ObjNav(); // Business Central SOAP client

            try
            {
                var response = await client.CreateUserAsync(username, password, email, name);
                return response.return_value; // Assuming the JSON response is in `return_value`
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                throw new Exception($"Failed to register user: {ex.Message}");
            }
        }

        public async Task<string> LoginUserAsync(string username, string password)
        {
            var client = _credentials.ObjNav();

            try
            {
                var response = await client.LoginUserAsync(username, password);
                return response.return_value; // Assuming the JSON response is in `return_value`
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed: {ex.Message}");
            }
        }

        public async Task<string> UpdateUserAsync(string username, string name)
        {
            var client = _credentials.ObjNav();

            try
            {
                var response = await client.UpdateUserAsync(username, name);
                return response.return_value; // Assuming the JSON response is in `return_value`
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update user: {ex.Message}");
            }
        }
        // Admin Register
        public async Task<string> RegisterAdminAsync(string username, string password, string email, string name)
        {
            var client = _credentials.ObjNav();

            try
            {
                // Call Business Central to register admin
                var response = await client.CreateUserAsync(username, password, email, name);
                // Additional logic to mark user as admin in Business Central (if needed)
                return response.return_value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to register admin: {ex.Message}");
            }
        }

        // Admin Login
        public async Task<string> LoginAdminAsync(string username, string password)
        {
            var client = _credentials.ObjNav();

            try
            {
                // Call Business Central to login as admin
                var response = await client.LoginUserAsync(username, password);
                // Additional logic to ensure admin privileges (if needed)
                return response.return_value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Admin login failed: {ex.Message}");
            }
        }
    }
}
