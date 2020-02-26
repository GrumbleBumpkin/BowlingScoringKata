using BowlingScoringKata.Interfaces;
using BowlingScoringKata.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoringKata.Test
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void PerfectGame_TenFrames_ScoreIs300()
        {
            Factory testFactory = new Factory();
            Game testGame = new Game(testFactory);
            while (testGame.IsFinished != true)
            {
                testGame.AddScore(10);
            }
            for (int i = 1; i <= 10; ++i)
            {
                Assert.AreEqual(30 * i, testGame.GetScoreAtFrame(i));
            }
        }

        [TestMethod]
        public void SpareEveryFrame_GutterThenStrike_ScoreIs100()
        {
            Factory testFactory = new Factory();
            Game testGame = new Game(testFactory);
            int gutterStrikeToggle = 0;
            while (testGame.IsFinished != true)
            {
                if (gutterStrikeToggle == 0)
                {
                    testGame.AddScore(0);
                    gutterStrikeToggle = 1;
                }
                else
                {
                    testGame.AddScore(10);
                    gutterStrikeToggle = 0;
                }
            }
            for (int i = 1; i <= 10; ++i)
            {
                Assert.AreEqual(10 * i, testGame.GetScoreAtFrame(i));
            }
        }

        [TestMethod]
        public void SpareEveryFrame_FivePinsPerBall_ScoreIs150()
        {
            Factory testFactory = new Factory();
            Game testGame = new Game(testFactory);
            while (testGame.IsFinished != true)
            {
                testGame.AddScore(5);
            }
            for (int i = 1; i <= 10; ++i)
            {
                Assert.AreEqual(15 * i, testGame.GetScoreAtFrame(i));
            }
        }

        [TestMethod]
        public void WorstGameEver_TenFrames_ScoreIs0()
        {
            Factory testFactory = new Factory();
            Game testGame = new Game(testFactory);
            while (testGame.IsFinished != true)
            {
                testGame.AddScore(0);
            }
            Assert.AreEqual(0, testGame.GetTotalScore());
        }
    }
}
