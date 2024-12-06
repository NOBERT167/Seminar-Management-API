using Newtonsoft.Json;

namespace SeminarAPI.DTOs
{
    public class seminar
    {
    }
    public class SeminarRegistrationDto
    {
        public string SemNo { get; set; }
        public string CompanyNo { get; set; }
        public string ParticipantContactNo { get; set; }
        public bool Confirmed { get; set; }
    }

    public class SeminarUpdateDto
    {
        public string SemHeaderNo { get; set; }
        public int LineNo { get; set; }
        public bool Confirmed { get; set; }
    }

    public class SemRegistrationODataItem
    {
        [JsonProperty("Header_No")]
        public string Header_No { get; set; }
        [JsonProperty("Line_No")]
        public int Line_No { get; set; }
        [JsonProperty("Bill_to_Customer_No")]
        public string Bill_to_Customer_No { get; set; }
        [JsonProperty("Participant_Contact_No")]
        public string Participant_Contact_No { get; set; }
        [JsonProperty("Participant_Name")]
        public string Participant_Name { get; set; }
        [JsonProperty("To_Invoice")]
        public bool To_Invoice { get; set; }
        [JsonProperty("Registration_Date")]
        public string Registration_Date { get; set; }
        [JsonProperty("Amount")]
        public decimal Amount { get; set; }
        [JsonProperty("Confirmation_Status")]
        public string Confirmation_Status { get; set; }
        [JsonProperty("Discount_Amount")]
        public decimal Discount_Amount { get; set; }

        // Seminar deets
        [JsonProperty("Seminar_No")]
        public string Seminar_No { get; set; }
        [JsonProperty("Seminar_Name")]
        public string Seminar_Name { get; set; }
        [JsonProperty("Starting_Date")]
        public string Starting_Date { get; set; }
    }

    public class SeminarRegistrationRespItem
    {
        public string HeaderNo { get; set; }
        public int LineNo { get; set; }
        public string SeminarNo { get; set; }
        public string SeminarName { get; set; }
        public string StartingDate { get; set; }
        public string CompanyNo { get; set; }
        public string ParticipantContactNo { get; set; }
        public string ParticipantName { get; set; }
        public bool ToInvoice { get; set; }
        public string RegistrationDate { get; set; }
        public decimal Amount { get; set; }
        public string ConfirmationStatus { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
