using Newtonsoft.Json;

namespace Talent_Management.Models
{
    public class Actor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("daySalary")]
        public double DaySalary { get; set; }
    }
}
