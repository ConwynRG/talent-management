using Newtonsoft.Json;
using System;
using System.IO;
using Talent_Management.Models;

namespace Talent_Management.Utilities
{
    public class MovieSerializer
    {
        private readonly string inputLocation;
        
        public MovieSerializer(string inputLocation)
        {
            this.inputLocation = inputLocation;
        }

        public Movie DeserializeMovie()
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            using (StreamReader reader = new StreamReader($"{projectDirectory}{inputLocation}"))
            {
                var json = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<Movie>(json);
            }
        }
    }
}
