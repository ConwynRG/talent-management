using System;
using System.Collections.Generic;
using System.Linq;
using Talent_Management.Models;
using Talent_Management.Utilities;

namespace Talent_Management
{
    public class TalentManager
    {
        private readonly string inputLocation = "\\Input\\simpleTest.json";
        private readonly MovieSerializer _movieSerializer;
        private readonly Random _randomizer = new Random();

        
        private readonly Queue<double> _thresholdPriceQueue = new();

        private double _bestPrice;
        private List<Scene> _bestSceneSequence;

        private List<Actor> _actors;
        private List<Scene> _currentScene;

        private int _queueLength = 20;

        public TalentManager()
        {
            _movieSerializer = new(inputLocation);
        }

        public void Initialize()
        {
            var movie = _movieSerializer.DeserializeMovie();
            var initialPrice = CalculateSallary(movie.Actors, movie.Scenes);

            _queueLength = (movie.Actors.Count + movie.Scenes.Count)/2;

            for (int i = 0; i < _queueLength; i++)
            {
                _thresholdPriceQueue.Enqueue(initialPrice);
            }

            _actors = movie.Actors;
            _currentScene = movie.Scenes;

            _bestPrice = initialPrice;
            _bestSceneSequence = movie.Scenes;
        }

        public void Start()
        {
            int best_iterator = 0;
            int change_iterator = 0;
            int iterationWithoutBest = _currentScene.Count * _actors.Count * 2000;
            int iterationWithoutChange = (_currentScene.Count + _actors.Count);

            while (best_iterator < iterationWithoutBest && change_iterator < iterationWithoutChange)
            {
                var newSceneSequence = ChangeSceneOrder(_currentScene);
                var newSceneSequencePrice = CalculateSallary(_actors, newSceneSequence);

                _thresholdPriceQueue.Enqueue(newSceneSequencePrice);

                if (newSceneSequencePrice < _bestPrice)
                {
                    _bestPrice = newSceneSequencePrice;
                    _bestSceneSequence = newSceneSequence;

                    best_iterator = 0;
                }

                var currentThreshold = _thresholdPriceQueue.Dequeue();

                if (newSceneSequencePrice < currentThreshold)
                {
                    _currentScene = newSceneSequence;
                    change_iterator = 0;
                }

                best_iterator++;
                change_iterator++;
            }

            Console.WriteLine($"Best Price: {Math.Round(_bestPrice, 2)}");
            Console.Write($"Best Sequence: ");

            _bestSceneSequence.ForEach(scene => Console.Write($"{scene.Id} "));

            Console.WriteLine();
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

        private List<Scene> ChangeSceneOrder(List<Scene> scenes)
        {
            var newScenes = new List<Scene>(scenes);
            var index = _randomizer.Next(1, scenes.Count);

            newScenes.Swap(index, index - 1);

            return newScenes;
        }
    }
}
