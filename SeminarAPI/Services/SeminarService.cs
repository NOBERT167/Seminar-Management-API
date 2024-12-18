using Newtonsoft.Json;
using SeminarAPI.Helpers;
using SeminarAPI.Interfaces;
using SeminarAPI.Models;
using System.Text.Json;

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
        public async Task<IEnumerable<SeminarData>> GetAllSeminarsAsync()
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
                    return System.Text.Json.JsonSerializer.Deserialize<List<SeminarData>>(seminars.ToString())!;
                }
            }
            else
            {
                var errorResponse = await ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }

            return new List<SeminarData>();
        }

        //GetSeminar By Doc No.
        public async Task<SeminarData> GetSeminarDataAsync(string No)
        {
            var url = $"{_baseUrl}/SeminarList?$filter=No eq '{No}'";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);
                var seminar = jsonData.RootElement.GetProperty("value").EnumerateArray().FirstOrDefault();

                return System.Text.Json.JsonSerializer.Deserialize<SeminarData>(seminar.ToString())!;
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
        public async Task<bool> InsertSeminarRegDataAsync(string seminarNo, DateTime startingDate, string personNo, string roomNo)
        {
            var client = _credentials.ObjNav();
            // Pass parameters directly to the SOAP method
            await client.InsertSeminarRegDataAsync(seminarNo, startingDate, personNo, roomNo);
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

        private RoomData? ParseRoomData(string rawData)
        {
            try
            {
                return JsonConvert.DeserializeObject<RoomData>(rawData);
            }
            catch
            {
                return null;
            }
        } 
        
        private SeminarRegistrationData? ParseSeminarRegistrationData(string rawData)
        {
            try
            {
                return JsonConvert.DeserializeObject<SeminarRegistrationData>(rawData);
            }
            catch
            {
                return null;
            }
        }

        //Get all rooms
        public async Task<IEnumerable<RoomData>> GetAllRoomsAsync()
        {
            var url = $"{_baseUrl}/RoomList";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);

                if (jsonData.RootElement.TryGetProperty("value", out var rooms))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<List<RoomData>>(rooms.ToString())!;
                }
            }
            else
            {
                var errorResponse = await ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }

            return new List<RoomData>();
        }

        //Get room by id
        public async Task<RoomData?> GetAllRoomsByIdAsync(string roomNo)
        {
            var url = $"{_baseUrl}/RoomList?$filter=No eq '{roomNo}'";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);
                var room = jsonData.RootElement.GetProperty("value").EnumerateArray().FirstOrDefault();

                return System.Text.Json.JsonSerializer.Deserialize<RoomData>(room.ToString())!;
            }
            else
            {

                var errorResponse = await Helpers.ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }
        }

        //AddSeminarRoom 
        public async Task<bool> AddSeminarRoomAsync(string name, int maxParticipants, bool isInternal)
        {
            var client = _credentials.ObjNav();
            // Pass parameters directly to the SOAP method
            await client.AddSeminarRoomAsync(name, maxParticipants, isInternal);
            return true; // Assuming no exception indicates success
        }

        //UpdateSeminar room
        public async Task<bool> UpdateSeminarRoomAsync(string No, string name, int maxParticipants, bool isInternal)
        {
            var client = _credentials.ObjNav();
            await client.UpdateSeminarRoomAsync(No, name, maxParticipants, isInternal);
            return true;
        }

        //DeleteSeminarRoom
        public async Task<bool> DeleteSeminarRoomAsync(string No)
        {
            var client = _credentials.ObjNav();
            await client.DeleteSeminarRoomAsync(No);
            return true;
        }

        //Seminar registration
        //GetAllSeminarsregistrations
        public async Task<IEnumerable<SeminarRegistrationData>> GetAllSeminarRegistrationAsync()
        {
            var url = $"{_baseUrl}/SeminarRegistrationList";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);

                if (jsonData.RootElement.TryGetProperty("value", out var registration))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<List<SeminarRegistrationData>>(registration.ToString())!;
                }
            }
            else
            {
                var errorResponse = await ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }

            return new List<SeminarRegistrationData>();
        }

        //Get seminar registration by id
        public async Task<SeminarRegistrationData?> GetAllSeminarRegistrationByIdAsync(string regNo)
        {
            var url = $"{_baseUrl}/SeminarRegistrationList?$filter=No eq '{regNo}'";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonString);
                var registration = jsonData.RootElement.GetProperty("value").EnumerateArray().FirstOrDefault();

                return System.Text.Json.JsonSerializer.Deserialize<SeminarRegistrationData>(registration.ToString())!;
            }
            else
            {

                var errorResponse = await Helpers.ErrorHelper.HandleErrorResponse(response);
                throw new Exception(errorResponse.Message);
            }
        }



    }
    // SeminarData Class
    public class SeminarData
    {
        public string No { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Seminar_Duration { get; set; }
        public decimal Seminar_Price { get; set; }
        public int Minimum_Participants { get; set; }
        public int Maximum_Participants { get; set; }
    }

    public class SeminarRegData
    {
        public string SeminarNo { get; set; }
        public DateTime StartingDate { get; set; }
        public string PersonNo { get; set; }
        public string RoomNo { get; set; }
    }

    public class SeminarParticipant
    {
        public string SeminarNo { get; set; }
        public string CompanyNo { get; set; }
        public string ParticipantNo { get; set; }
    }

    public class RoomData
    {
        public string No { get; set; }
        public string Name { get; set; }
        public int Max_Participants { get; set; }
        public bool Internal {get; set;}
    }

    public class SeminarRegistrationData
    {
        public string No { get; set; }
        public string Seminar_Name { get; set; }
        //public string Seminar_No { get; set; }
        public DateOnly Starting_Date { get; set; }
        public string Status { get; set; }
        public string Room_Resource_No { get; set; }
        public int Duration { get; set; }
        public int Registered_Participants { get; set; }
        public int Maximum_Participants { get; set; }
    }

    //public enum Status
    //{
    //    Planning,
    //    Registration,
    //    Closed,
    //    Cancelled
    //}

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
