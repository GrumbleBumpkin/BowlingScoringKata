using BowlingScoringKata.Interfaces;
using System;
using System.Collections.Generic;

namespace BowlingScoringKata.Objects
{
    public class Game : IGame
    {
        private IFrameFactory _frameFactory;

        public int _frameCount = 10;
        public List<IFrame> Frames { get; } = new List<IFrame>();

        public Game(IFrameFactory frameFactory)
        {
            _frameFactory = frameFactory;
        }

        public Game(IFrameFactory frameFactory, int frameCount)
        {
            _frameFactory = frameFactory;
            _frameCount = frameCount;
        }

        public IFrame AddNewFrame()
        {
            IFrame newFrame = _frameFactory.BuildFrame();
            if (Frames.Count > 0)
            {
                IFrame lastClosedFrame = Frames[Frames.Count - 1];
                lastClosedFrame.NextFrame = newFrame;
            }
            Frames.Add(newFrame);
            newFrame.IsLastFrame = Frames.Count == _frameCount;
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
            if (frameNumber > Frames.Count)
            {
                throw new ArgumentOutOfRangeException($"Frame number {frameNumber} does not exist in this game.");
            }
            int totalScore = 0;
            for (int i = 0; i < frameNumber; ++i)
            {
                totalScore += Frames[i].TotalScore;
            }
            return totalScore;
        }
        
        public int GetTotalScore()
        {
            return GetScoreAtFrame(Frames.Count);
        }

        public bool IsFinished
        {
            get
            {
                return Frames.Count == _frameCount && (Frames.TrueForAll(fr => fr.IsClosed == true) == true);
            }
        }
    }
}
