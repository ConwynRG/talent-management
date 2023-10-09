using Newtonsoft.Json;
using System.Collections.Generic;

namespace Talent_Management.Models
{
    public class Movie
    {
        [JsonProperty("actors")]
        public List<Actor> Actors { get; set; }

        [JsonProperty("scenes")]
        public List<Scene> Scenes { get; set; }
    }
}
