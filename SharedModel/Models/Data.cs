using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public class Data
    {
        [JsonPropertyName("ws")]
        public string Ws { get; set; }
        [JsonPropertyName("wg")]
        public string Wg { get; set; }
        [JsonPropertyName("wd")]
        public string Wd { get; set; }
        [JsonPropertyName("ta")]
        public string Ta { get; set; }
        [JsonPropertyName("t0")]
        public string T0 { get; set; }
        [JsonPropertyName("ha")]
        public string Ha { get; set; }
        [JsonPropertyName("p0")]
        public string P0 { get; set; }
        [JsonPropertyName("r1")]
        public string R1 { get; set; }
        [JsonPropertyName("ra")]
        public string Ra { get; set; }
        [JsonPropertyName("h0")]
        public string H0 { get; set; }
    }
}