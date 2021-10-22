namespace Space_Invaders.High_Score
{
    public class HighScoreData
    {
        public HighScoreData(int score, string name)
        {
            this.Score = score;
            this.Name = name;
        }

        public int Score { get; }

        public string Name { get; }
    }
}