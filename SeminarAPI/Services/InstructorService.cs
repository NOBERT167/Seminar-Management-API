using Newtonsoft.Json;
using SeminarAPI.Helpers;
using SeminarAPI.Interfaces;
using SeminarAPI.Models;
using System.Text.Json;

namespace SeminarAPI.Services
{
    public class InstructorService : IInstructor
    {
        private readonly Credentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _baseUrl => _configuration.GetValue<string>("PortalBaseUrl")!;

        public InstructorService(Credentials credentials, HttpClient httpClient, IConfiguration configuration)
        {
            _credentials = credentials;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> AddInstructorAsync(InstructorData instructor)
        {
            var client = _credentials.ObjNav();
            await client.AddInstructorAsync(
                instructor.No,
                instructor.Name,
                instructor.Internal,
                instructor.Booked_till
            );
            return true;
        }

        public async Task<bool> UpdateInstructorAsync(InstructorData instructor)
        {
            var client = _credentials.ObjNav();
            await client.UpdateInstructorAsync(
                instructor.No,
                instructor.Name,
                instructor.Internal,
                instructor.Booked_till
            );
            return true;
        }

        public async Task<bool> DeleteInstructorAsync(string instructorNo)
        {
            var client = _credentials.ObjNav();
            await client.DeleteInstructorAsync(instructorNo);
            return true;
        }

        //Get all instructors
        public async Task<IEnumerable<InstructorData>> GetAllInstructorsAsync()
        {
            var url = $"{_baseUrl}/InstructorList?$filter=Type eq 'Person'";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);
                if (jsonData.RootElement.TryGetProperty("value", out var instructor))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<List<InstructorData>>(instructor.ToString())!;
                }
            }
            else
            {
                var errorResponse = await ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }
            return new List<InstructorData>();
        }

        //Get instructor by id
        public async Task<InstructorData?> GetInstructorDataAsync(string instructorNo)
        {

            var url = $"{_baseUrl}/InstructorList?$filter=No eq '{instructorNo}'";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);

                var instructor = jsonData.RootElement.GetProperty("value").EnumerateArray().FirstOrDefault();

                return System.Text.Json.JsonSerializer.Deserialize<InstructorData>(instructor.ToString())!;
            }
            else
            {
                var errorResponse = await ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }
        }

        private InstructorData? ParseInstructorData(string rawData)
        {
            try
            {
                // Deserialize the raw data into InstructorData
                return JsonConvert.DeserializeObject<InstructorData>(rawData);
            }
            catch
            {
                return null; // Handle parsing errors gracefully
            }
        }
    }
    public class InstructorData
    {
        public string No { get; set; }
        public string Name { get; set; } 
        public bool Internal { get; set; }
        public DateTime Booked_till { get; set; }
    }


}
