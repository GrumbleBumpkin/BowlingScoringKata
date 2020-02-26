using BowlingScoringKata.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoringKata.Objects
{
    public class Frame : IFrame
    {
        private int scoreCounter = 0;
        public int?[] Scores { get; }
        public int RemainingPins { get; set; }
        public bool IsLastFrame { get; set; } = false;
        public IFrame nextFrame { get; set; }

        public Frame()
        {
            Scores = new int?[3];
            RemainingPins = 10;
        }

        public void AddScore(int score)
        {
            if (score > RemainingPins)
            {
                throw new Exception("Score value cannot be higher than the remaining pins in the frame.");
            }

            RemainingPins -= score;
            Scores[scoreCounter++] = score;

            // Reset pins for extra balls if this is the last frame.
            if (RemainingPins == 0 && IsLastFrame == true && IsClosed == false)
            {
                RemainingPins = 10;
            }
        }

        /// <summary>
        /// Get the next non-null score in this frame and return it.
        /// </summary>
        /// <returns></returns>
        public int GetNextScore()
        {
            foreach (int? score in Scores)
            {
                if (score != null)
                {
                    return (int)score;
                }
            }
            return 0;
        }

        /// <summary>
        /// Get the next two non-null scores in this frame and return it.
        /// If there is only one score in this frame, the second score will come from the next frame.
        /// </summary>
        /// <returns></returns>
        public List<int> GetNextTwoScores()
        {
            List<int> nextTwoScores = new List<int>();
            foreach (int? score in Scores)
            {
                if (score != null)
                {
                    nextTwoScores.Add((int)score);
                    if (nextTwoScores.Count == 2)
                    {
                        return nextTwoScores;
                    }
                }
            }

            if (nextFrame != null)
            {
                nextTwoScores.Add(nextFrame.GetNextScore());
            }

            return nextTwoScores;
        }

        public bool IsClosed
        {
            get
            {
                if (IsLastFrame == false)
                {
                    return TotalScore >= 10 || scoreCounter == 2;
                }
                else
                {
                    return (TotalScore < 10 && scoreCounter == 2) || (TotalScore >= 10 && scoreCounter > 2);
                }
            }
        }

        public int TotalScore
        {
            get
            {
                if (RemainingPins == 0)
                {
                    if (scoreCounter == 1)
                    {
                        // A single ball frame with no remaining pins is a strike.
                        return (int)Scores.Sum() + (nextFrame != null ? nextFrame.GetNextTwoScores().Sum() : 0);
                    }
                    else
                    {
                        // A double ball frame with no remaining pins is a spare.
                        return (int)Scores.Sum() + (nextFrame != null ? nextFrame.GetNextScore() : 0);
                    }
                }
                else
                {
                    return (int)Scores.Sum();
                }
            }
        }

    }
}
