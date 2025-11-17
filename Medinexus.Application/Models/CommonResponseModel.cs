using System.Net;

namespace Medinexus.Application.Models
{
    /// <summary>
    /// A generic, consistent response model for all API endpoints.
    /// </summary>
    public class CommonResponseModel
    {
        /// <summary>
        /// Indicates whether the request was successful or not.
        /// </summary>
        public bool IsSuccess {  get; set; }

        /// <summary>
        /// Message describing the result
        /// </summary>
        public string? Message {  get; set; }

        /// <summary>
        /// Optional response data (can be any type).
        /// </summary>
        public object? Data {  get; set; }

        /// <summary>
        /// List of error or validation messages
        /// </summary>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// HTTP status code for the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Return all Exception message
        /// </summary>
        public Exception? ExceptionMessage { get; set; }

        // Factory methods for consistent responses
        public static CommonResponseModel SuccessResponse(object data, string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new CommonResponseModel
            {
                IsSuccess = true,
                Message = message ?? "Request completed successfully.",
                Data = data,
                StatusCode = (int)statusCode
            };
        }
        public static CommonResponseModel ErrorResponse(string message, List<string>? errors = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new CommonResponseModel
            {
                IsSuccess = false,
                Message = message,
                Errors = errors,
                StatusCode = (int)statusCode
            };
        }
        public static CommonResponseModel ExceptionResponse(Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new CommonResponseModel
            {
                IsSuccess = false,
                Message = "An unexpected error occurred. Please try again later.",
                ExceptionMessage = ex,
                StatusCode = (int)statusCode
            };
        }
    }
}
