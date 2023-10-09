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


        }
    }
}
