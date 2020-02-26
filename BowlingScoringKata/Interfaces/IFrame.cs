using System.Collections.Generic;

namespace BowlingScoringKata.Interfaces
{
    public interface IFrame
    {
        public void AddScore(int score);
        public int GetNextScore();
        public List<int> GetNextTwoScores();
        bool IsClosed { get; }
        bool IsLastFrame { get; set; }
        public IFrame NextFrame { get; set; }
        int?[] Scores { get; }
        int RemainingPins { get; set; }
        int TotalScore { get; }
    }
}
