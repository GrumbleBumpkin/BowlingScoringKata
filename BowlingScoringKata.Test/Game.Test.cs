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
            Assert.AreEqual(300, testGame.GetTotalScore());
        }
        
        [TestMethod]
        public void PerfectGame_TenFrames_TwentyPins_ScoreIs600()
        {
            int pinCount = 20;
            Factory testFactory = new Factory(pinCount);
            Game testGame = new Game(testFactory);
            while (testGame.IsFinished != true)
            {
                testGame.AddScore(pinCount);
            }
            Assert.AreEqual(600, testGame.GetTotalScore());
        }

        [TestMethod]
        public void PerfectGame_TwentyFrames_ScoreIs600()
        {
            Factory testFactory = new Factory();
            Game testGame = new Game(testFactory, 20);
            while (testGame.IsFinished != true)
            {
                testGame.AddScore(10);
            }
            Assert.AreEqual(600, testGame.GetTotalScore());
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
            Assert.AreEqual(100, testGame.GetTotalScore());
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
            Assert.AreEqual(150, testGame.GetTotalScore());
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
