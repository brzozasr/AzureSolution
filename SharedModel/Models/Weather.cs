using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public class Weather
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("station")]
        public string Station { get; set; }
        [JsonPropertyName("utc")]
        public string Utc { get; set; }
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}