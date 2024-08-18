using System.Text.Json.Serialization;

namespace ECommerceFeedback.Common
{
    public class ApiResponse    {
        public ApiResponse()
        {
            ErrorDetails = new List<ErrorModel>();
        }

        public List<ErrorModel> ErrorDetails { get; set; }
        [JsonIgnore]
        public bool IsOverrideSuccess { get; set; }
        [JsonIgnore]
        public string ErrorType { get; set; }
        public bool Success
        {
            get { return ErrorDetails == null || ErrorDetails.Count <= 0 || IsOverrideSuccess; }
        }

    }
}