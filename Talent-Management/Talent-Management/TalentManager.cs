using System.Collections.Generic;
using System.Linq;
using Talent_Management.Models;
using Talent_Management.Utilities;

namespace Talent_Management
{
    public class TalentManager
    {
        private readonly string inputLocation = "\\Input\\input.json";
        private readonly MovieSerializer _movieSerializer;

        public TalentManager()
        {
            _movieSerializer = new(inputLocation);
        }

        public void Start()
        {
            var movie = _movieSerializer.DeserializeMovie();

            var price = CalculateSallary(movie.Actors, movie.Scenes);
        }

        private double CalculateSallary(List<Actor> actors, List<Scene> scenesSequence)
        {
            return actors.Sum(actor => CalculateSallary(actor, scenesSequence));
        }

        private double CalculateSallary(Actor actor, List<Scene> scenesSequence)
        {
            var firstScene = scenesSequence.First(scene => scene.ParticipantId.Contains(actor.Id));
            var lastScene = scenesSequence.Last(scene => scene.ParticipantId.Contains(actor.Id));

            var firstSceneId = scenesSequence.FindIndex(scene => scene.Id == firstScene.Id);
            var lastSceneId = scenesSequence.FindIndex(scene => scene.Id == lastScene.Id);

            var actingDays = lastSceneId - firstSceneId + 1;

            return actingDays * actor.DaySalary;
        }
    }
}
