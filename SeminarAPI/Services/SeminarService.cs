using NAVWS;
using SeminarAPI.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using SeminarAPI.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SeminarAPI.Services
{
    public class SeminarService : ISeminar
    {
        private readonly Credentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SeminarService(Credentials credentials, HttpClient httpClient, IConfiguration configuration)
        {
            _credentials = credentials;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<SeminarData?> GetSeminarDataAsync(string docNo)
        {
            var client = _credentials.ObjNav();
            // Directly pass the docNo to the generated method
            var response = await client.GetSeminarDataAsync(docNo);

            // Assume `response.return_value` contains the raw data
            if (string.IsNullOrEmpty(response.return_value))
                return null;

            // Parse the response into SeminarData
            var seminarData = ParseSeminarData(response.return_value);
            return seminarData;
        }

        public async Task<bool> InsertSeminarDataAsync(string name, decimal seminarDuration, int minParticipants, int maxParticipants)
        {
            var client = _credentials.ObjNav();
            var request = new InsertSeminarData(name, seminarDuration, minParticipants, maxParticipants);
            await client.InsertSeminarDataAsync(request.name, request.seminar_Duration, request.minimum_Participants, request.maximum_Participants);
            return true; // Assuming no exception indicates success
        }

        public async Task<bool> UpdateSeminarDataAsync(string docNo, string name, decimal seminarDuration)
        {
            var client = _credentials.ObjNav();
            var request = new UpdateSeminarData(docNo, name, seminarDuration);
            await client.UpdateSeminarDataAsync(request.docNo, request.name, request.seminar_Duration);
            return true; // Assuming no exception indicates success
        }

        public async Task<bool> DeleteSeminarDataAsync(string docNo)
        {
            var client = _credentials.ObjNav();
            // Directly pass the docNo to the generated method
            await client.DeleteSeminarDataAsync(docNo);
            return true; // Assuming no exception indicates success
        }


        private SeminarData? ParseSeminarData(string rawData)
        {
            try
            {
                // Use your preferred parsing logic here. For example:
                var data = JsonConvert.DeserializeObject<SeminarData>(rawData);
                return data;
            }
            catch
            {
                return null; // Handle parsing errors gracefully
            }
        }
    }

    // SeminarData Class
    public class SeminarData
    {
        public string DocumentNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal SeminarDuration { get; set; }
        public int MinimumParticipants { get; set; }
        public int MaximumParticipants { get; set; }
    }
}
