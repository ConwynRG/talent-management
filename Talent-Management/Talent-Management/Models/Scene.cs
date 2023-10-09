using Newtonsoft.Json;
using System.Collections.Generic;

namespace Talent_Management.Models
{
    public class Scene
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("participantId")]
        public List<int> ParticipantId { get; set; }
    }
}
