using BowlingScoringKata.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoringKata.Objects
{
    public class Frame : IFrame
    {
        private int scoreCounter = 0;
        private readonly int framePinCount;
        public int?[] Scores { get; }
        public int RemainingPins { get; set; }
        public bool IsLastFrame { get; set; } = false;
        public IFrame NextFrame { get; set; }

        public Frame(int pinCount)
        {
            Scores = new int?[3];
            framePinCount = pinCount;
            RemainingPins = framePinCount;
        }

        public void AddScore(int score)
        {
            if (score > RemainingPins)
            {
                throw new ArgumentOutOfRangeException("Score value cannot be higher than the remaining pins in the frame.");
            }

            RemainingPins -= score;
            Scores[scoreCounter++] = score;

            // Reset pins for extra balls if this is the last frame.
            if (RemainingPins == 0 && IsLastFrame == true && IsClosed == false)
            {
                RemainingPins = framePinCount;
            }
        }

        /// <summary>
        /// Get the next non-null score in this frame and return it.
        /// </summary>
        /// <returns>The next score in the next frame.</returns>
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
        /// <returns>List of the next two scores in the next one or two frames.</returns>
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

            if (NextFrame != null)
            {
                nextTwoScores.Add(NextFrame.GetNextScore());
            }

            return nextTwoScores;
        }

        public bool IsClosed
        {
            get
            {
                if (IsLastFrame == false)
                {
                    return RemainingPins == 0 || scoreCounter == 2;
                }
                else
                {
                    return (TotalScore < framePinCount && scoreCounter == 2) || (TotalScore >= framePinCount && scoreCounter == 3);
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
                        return (int)Scores.Sum() + (NextFrame != null ? NextFrame.GetNextTwoScores().Sum() : 0);
                    }
                    else
                    {
                        // A double ball frame with no remaining pins is a spare.
                        return (int)Scores.Sum() + (NextFrame != null ? NextFrame.GetNextScore() : 0);
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
