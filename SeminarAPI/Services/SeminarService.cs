using NAVWS;
using SeminarAPI.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using SeminarAPI.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text.Json;
using SeminarAPI.Helpers;

namespace SeminarAPI.Services
{
    public class SeminarService : ISeminar
    {
        private readonly Credentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _baseUrl => _configuration.GetValue<string>("PortalBaseUrl")!;

        public SeminarService(Credentials credentials, HttpClient httpClient, IConfiguration configuration)
        {
            _credentials = credentials;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Get All Seminars
        public async Task<IEnumerable<SeminarDataGet>> GetAllSeminarsAsync()
        {
            //var filterQuery = string.IsNullOrWhiteSpace(string.Empty) ? string.Empty : $"?$filter=contains(Name, '{string.Empty}')";
            var url = $"{_baseUrl}/SeminarList";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);

                if (jsonData.RootElement.TryGetProperty("value", out var seminars))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<List<SeminarDataGet>>(seminars.ToString())!;
                }
            }
            else
            {
                var errorResponse = await ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }

            return new List<SeminarDataGet>();
        }

        //GetSeminar By Doc No.
        public async Task<SeminarDataGet> GetSeminarDataAsync(string docNo)
        {
            var url = $"{_baseUrl}/SeminarList?$filter=No eq '{docNo}'";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);
                var seminar = jsonData.RootElement.GetProperty("value").EnumerateArray().FirstOrDefault();

                return System.Text.Json.JsonSerializer.Deserialize<SeminarDataGet>(seminar.ToString())!;
            }
            else
            {

                var errorResponse = await Helpers.ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }


        }

        //InsertSeminarData
        public async Task<bool> InsertSeminarDataAsync(string name, decimal seminarDuration, int minParticipants, int maxParticipants, decimal seminarPrice)
        {
            var client = _credentials.ObjNav();
            // Pass parameters directly to the SOAP method
            await client.InsertSeminarDataAsync(name, seminarDuration, minParticipants, maxParticipants, seminarPrice);
            return true; // Assuming no exception indicates success
        }

        public async Task<bool> UpdateSeminarDataAsync(string docNo, string name, decimal seminarDuration, decimal seminarPrice)
        {
            var client = _credentials.ObjNav();
            // Pass parameters directly to the SOAP method
            await client.UpdateSeminarDataAsync(docNo, name, seminarDuration, seminarPrice);
            return true; // Assuming no exception indicates success
        }

        //DeleteSeminar
        public async Task<bool> DeleteSeminarDataAsync(string docNo)
        {
            var client = _credentials.ObjNav();
            // Pass docNo directly as a string
            await client.DeleteSeminarDataAsync(docNo);
            return true; // Assuming no exception indicates success
        }

        //Register Seminar
        public async Task<bool> InsertSeminarRegDataAsync(string seminarNo, string personNo, string roomNo)
        {
            var client = _credentials.ObjNav();
            // Pass parameters directly to the SOAP method
            await client.InsertSeminarRegDataAsync(seminarNo, personNo, roomNo);
            return true; // Assuming no exception indicates success
        }

        //Register participant
        public async Task<bool> RegisterParticipantAsync(string docNo, string companyNo, string participantNo)
        {
            var client = _credentials.ObjNav();
            await client.RegisterParticipantAsync(docNo, companyNo, participantNo);
            return true; // Assuming no exception indicates success
        }

        private SeminarData? ParseSeminarData(string rawData)
        {
            try
            {
                // Deserialize the raw data into SeminarData
                return JsonConvert.DeserializeObject<SeminarData>(rawData);
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
        public decimal SeminarPrice { get; set; }
        public int MinimumParticipants { get; set; }
        public int MaximumParticipants { get; set; }
    }

    public class SeminarDataGet
    {
        public string No { get; set; }
        public string Name { get; set; }
        public int Seminar_Duration { get; set; }
        public int Seminar_Price { get; set; }

    }

    public class SeminarRegData
    {
        public string SeminarNo { get; set; }
        public string PersonNo { get; set; }
        public string RoomNo { get; set; }
    }

    public class SeminarParticipant
    {
        public string SeminarNo { get; set; }
        public string CompanyNo { get; set; }
        public string ParticipantNo { get; set; }
    }

    //public class SeminarData
    //{
    //    public string No { get; set; } = string.Empty;
    //    public string Name { get; set; } = string.Empty;
    //    public decimal Seminar_Duration { get; set; }
    //    public decimal Seminar_Price { get; set; }
    //    public string Gen_Prod_Posting_Group { get; set; } = string.Empty;
    //    public string VAT_Prod_Posting_Group { get; set; } = string.Empty;
    //}

}
