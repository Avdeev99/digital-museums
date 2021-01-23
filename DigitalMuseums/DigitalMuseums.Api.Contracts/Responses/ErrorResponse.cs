using System.Collections.Generic;
using Newtonsoft.Json;

namespace DigitalMuseums.Api.Contracts.Responses
{
    /// <summary>
    /// Represents error response model.
    /// </summary>
    public class ErrorResponse
    {
        public ErrorResponse(string message, int status)
        {
            Message = message;
            Status = status;
            Errors = new Dictionary<string, List<string>>();
        }

        public ErrorResponse(string message, int status, Dictionary<string, List<string>> errors)
        {
            Message = message;
            Status = status;
            Errors = errors;
        }

        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("status")]
        public int Status { get; private set; }

        [JsonProperty("errors")]
        public Dictionary<string, List<string>> Errors { get; private set; }
    }
}