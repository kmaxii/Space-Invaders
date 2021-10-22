using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Space_Invaders.High_Score
{
    public class HighScore
    {
        private List<HighScoreData> _highScores;
        private const string FileName = "assets/highscores.txt";
        private int _scoreToBeAdded;

        public int GetScoreToBeAdded => _scoreToBeAdded;
        public List<HighScoreData> GetHighScoreData => _highScores;

        public HighScore()
        {
            _highScores = new List<HighScoreData>();
            LoadData();
            _scoreToBeAdded = 0;
        }

        private void LoadData()
        {
            if (!File.Exists(FileName))
            {
                File.Create(FileName).Close();
                File.WriteAllText(FileName, "none:0");
            }

            foreach (var line in File.ReadLines(FileName, Encoding.UTF8))
            {
                string[] split = line.Trim().Split(":");

                //Adds the HighScore to the list
                _highScores.Add(new HighScoreData(int.Parse(split[1]), split[0]));
            }

            SortRecords();
        }

        public void SaveData()
        {
            File.Create(FileName).Close();
            List<string> toBeAddedToFile = new List<string>();
            foreach (var t in _highScores)
            {
                toBeAddedToFile.Add($"{t.Name}:{t.Score}");
            }

            File.WriteAllLines(FileName, toBeAddedToFile);
            Console.WriteLine($"Saved High scores");
        }


        public void AddHighScore(string playerName)
        {
            if (_highScores.Count <= 10 || _highScores[^1].Score < _scoreToBeAdded)
            {
                Console.WriteLine($"Added {playerName} with a score of {_scoreToBeAdded} to the high score list");
                _highScores.Add(new HighScoreData(_scoreToBeAdded, playerName));
            }

            SortRecords();
        }

        private void SortRecords()
        {
            _highScores = _highScores.OrderBy(record => -record.Score).ToList();

            if (_highScores.Count > 10)
            {
                _highScores.RemoveAt(10);
            }
        }


        //Opens the screen where the player can enter his name to save to the high score list
        //But only does it if the score is high enough
        public void EnterNewHighScore(int score, Scene scene)
        {
            if (_highScores.Count == 10 && _highScores[^1].Score > score)
            {
                //Starts rendering the leaderboard since score is not high enough
                scene.Loader.Open(scene, "Leaderboard");
                return;
            }


            _scoreToBeAdded = score;
            scene.Loader.Open(scene, "EnterNamePage");
        }
    }
}