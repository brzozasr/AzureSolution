namespace SharedModels.Models
{
    public class Weather
    {
        public string Time { get; set; }
        public string Station { get; set; }
        public string Utc { get; set; }
        public Data Data { get; set; }
    }
}