using BowlingScoringKata.Interfaces;
using BowlingScoringKata.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BowlingScoringKata.Test
{
    [TestClass]
    public class GameIntegrationTests
    {
        [TestMethod]
        public void PerfectGame_TenFrames_ScoreIs300()
        {
            FrameFactory testFactory = new FrameFactory();
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
            FrameFactory testFactory = new FrameFactory(pinCount);
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
            FrameFactory testFactory = new FrameFactory();
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
            FrameFactory testFactory = new FrameFactory();
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
            FrameFactory testFactory = new FrameFactory();
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
            FrameFactory testFactory = new FrameFactory();
            Game testGame = new Game(testFactory);
            while (testGame.IsFinished != true)
            {
                testGame.AddScore(0);
            }
            Assert.AreEqual(0, testGame.GetTotalScore());
        }
    }

    [TestClass]
    public class GameUnitTests
    {
        [TestMethod]
        public void AddNewFrame_FirstFrameInGame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupSet(p => p.IsLastFrame = false).Verifiable();

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            testGame.AddNewFrame();

            frameMock.VerifyAll();
            frameFactoryMock.VerifyAll();
        }

        [TestMethod]
        public void AddNewFrame_LastFrameInGame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupSet(p => p.IsLastFrame = true).Verifiable();

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object, 1);
            testGame.AddNewFrame();

            frameMock.VerifyAll();
            frameFactoryMock.VerifyAll();
        }

        [TestMethod]
        public void AddNewFrame_SecondFrameInGame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupSet(p => p.IsLastFrame = false).Verifiable();
            frameMock.SetupSet(p => p.NextFrame = frameMock.Object).Verifiable();

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            testGame.AddNewFrame();
            testGame.AddNewFrame();

            frameMock.VerifyAll();
            frameFactoryMock.VerifyAll();
        }

        [TestMethod]
        public void AddScore_AllFramesClosed_MakeNewFrame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupGet(p => p.IsClosed).Returns(true);
            frameMock.Setup(m => m.AddScore(10)).Verifiable();

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            testGame.AddNewFrame();
            frameFactoryMock.Invocations.Clear();
            testGame.AddScore(10);

            frameMock.VerifyAll();
            frameFactoryMock.Verify(m => m.BuildFrame(), Times.Once());
        }

        [TestMethod]
        public void AddScore_LastFrameOpen_AddToExistingFrame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupGet(p => p.IsClosed).Returns(false);
            frameMock.Setup(m => m.AddScore(10)).Verifiable();

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            testGame.AddNewFrame();
            frameFactoryMock.Invocations.Clear();
            testGame.AddScore(10);

            frameMock.VerifyAll();
            frameFactoryMock.Verify(m => m.BuildFrame(), Times.Never());
        }

        [TestMethod]
        public void AddScore_NoFrames_MakeNewFrame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.Setup(m => m.AddScore(10)).Verifiable();

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            testGame.AddScore(10);

            frameMock.VerifyGet(p => p.IsClosed, Times.Never());
            frameMock.VerifyAll();
            frameFactoryMock.Verify(m => m.BuildFrame(), Times.Once());
        }

        [TestMethod]
        public void GetRemainingPinsInFrame_AllFramesClosed_AddNewFrame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupGet(p => p.IsClosed).Returns(true);
            frameMock.SetupGet(p => p.RemainingPins).Returns(10);

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            testGame.AddNewFrame();
            frameFactoryMock.Invocations.Clear();
            int remainingPins = testGame.GetRemainingPinsInFrame();

            frameMock.VerifyAll();
            frameFactoryMock.Verify(m => m.BuildFrame(), Times.Once());
            Assert.AreEqual(10, remainingPins);
        }

        [TestMethod]
        public void GetRemainingPinsInFrame_LastFrameOpen_ReturnRemainingPinsInFrame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupGet(p => p.IsClosed).Returns(false);
            frameMock.SetupGet(p => p.RemainingPins).Returns(7);

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            testGame.AddNewFrame();
            frameFactoryMock.Invocations.Clear();
            int remainingPins = testGame.GetRemainingPinsInFrame();

            frameMock.VerifyAll();
            frameFactoryMock.Verify(m => m.BuildFrame(), Times.Never());
            Assert.AreEqual(7, remainingPins);
        }

        [TestMethod]
        public void GetRemainingPinsInFrame_NoFrames_MakeNewFrame()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupGet(p => p.RemainingPins).Returns(10);

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            int remainingPins = testGame.GetRemainingPinsInFrame();

            frameMock.VerifyGet(p => p.IsClosed, Times.Never());
            frameMock.VerifyAll();
            frameFactoryMock.Verify(m => m.BuildFrame(), Times.Once());
            Assert.AreEqual(10, remainingPins);
        }

        [TestMethod]
        public void GetScoreAtFrame_IndexInRange_ReturnsTotalScore()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupGet(p => p.TotalScore).Returns(5);

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            for (int i = 0; i < 5; i++)
            {
                testGame.AddNewFrame();
            }
            int totalScore = testGame.GetScoreAtFrame(5);

            frameMock.VerifyAll();
            Assert.AreEqual(25, totalScore);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetScoreAtFrame_IndexOutOfRange_RaisesException()
        {
            Mock<IFrame> frameMock = new Mock<IFrame>();
            frameMock.SetupGet(p => p.TotalScore).Returns(5);

            Mock<IFrameFactory> frameFactoryMock = new Mock<IFrameFactory>();
            frameFactoryMock.Setup(m => m.BuildFrame()).Returns(frameMock.Object);

            Game testGame = new Game(frameFactoryMock.Object);
            int totalScore = testGame.GetScoreAtFrame(5);
        }
    }
}
