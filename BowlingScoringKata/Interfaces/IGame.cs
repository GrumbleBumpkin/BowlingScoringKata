using System.Collections.Generic;

namespace BowlingScoringKata.Interfaces
{
    public interface IGame
    {
        List<IFrame> Frames { get; }
        IFrame AddNewFrame();
        void AddScore(int score);
        int GetRemainingPinsInFrame();
        int GetScoreAtFrame(int frameNumber);
        int GetTotalScore();
        bool IsFinished { get; }
    }
}
