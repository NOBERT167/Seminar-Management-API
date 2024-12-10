using System.Net;

namespace SeminarAPI.Helpers
{
    public static class ErrorHelper
    {
        public static async Task<ApiErrorResponse> HandleErrorResponse(HttpResponseMessage response)
        {
            var statusCode = response.StatusCode;
            var errorContent = await response.Content.ReadAsStringAsync();

            return new ApiErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = statusCode switch
                {
                    HttpStatusCode.Unauthorized => "You are not authorized to perform this action!",
                    HttpStatusCode.Forbidden => "Access Denied",
                    HttpStatusCode.BadRequest => "Invalid Request. Please check your input",
                    HttpStatusCode.NotFound => "The requested resource could not be found",
                    HttpStatusCode.InternalServerError => "The server encountered an error. Please try again later.",
                    _ => "An unexpected error occurred."
                },
                Details = errorContent

            };

        }
        public class ApiErrorResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public string? Details { get; set; } // Include server-provided error details, if available
        }

    }
}