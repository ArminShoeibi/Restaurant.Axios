using System.Text.Json.Serialization;

namespace Restaurant.Axios.DTOs
{
    public record AxiosResponseDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

    }
}
