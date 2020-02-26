using BowlingScoringKata.Interfaces;
using System.Collections.Generic;

namespace BowlingScoringKata.Objects
{
    public class Game : IGame
    {
        private IFactory _frameFractory;

        public int frameCount = 10;
        public List<IFrame> Frames { get; } = new List<IFrame>();

        public Game(IFactory frameFactory)
        {
            _frameFractory = frameFactory;
        }

        public IFrame AddNewFrame()
        {
            IFrame newFrame = _frameFractory.BuildFrame();
            if (Frames.Count > 0)
            {
                IFrame lastClosedFrame = Frames[Frames.Count - 1];
                lastClosedFrame.nextFrame = newFrame;
            }
            Frames.Add(newFrame);
            newFrame.IsLastFrame = Frames.Count == frameCount;
            return newFrame;
        }

        public void AddScore(int score)
        {
            IFrame openFrame = Frames.Find(fr => fr.IsClosed == false);
            if (openFrame == null)
            {
                openFrame = AddNewFrame();
                openFrame.AddScore(score);
            }
            else
            {
                openFrame.AddScore(score);
            }
        }

        public int GetRemainingPinsInFrame()
        {
            IFrame openFrame = Frames.Find(fr => fr.IsClosed == false);
            if (openFrame == null)
            {
                openFrame = AddNewFrame();
                return openFrame.RemainingPins;
            }
            else
            {
                return openFrame.RemainingPins;
            }
        }
        
        public int GetScoreAtFrame(int frameNumber)
        {
            int totalScore = 0;
            for (int i = 0; i < frameNumber; ++i)
            {
                totalScore += Frames[i].TotalScore;
            }
            return totalScore;
        }
        
        public int GetTotalScore()
        {
            return GetScoreAtFrame(frameCount);
        }

        public bool IsFinished
        {
            get
            {
                return Frames.Count == frameCount && (Frames.TrueForAll(fr => fr.IsClosed == true) == true);
            }
        }
    }
}
