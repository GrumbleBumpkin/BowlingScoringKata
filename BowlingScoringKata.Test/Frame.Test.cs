using BowlingScoringKata.Interfaces;
using BowlingScoringKata.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BowlingScoringKata.Test
{
    [TestClass]
    public class FrameUnitTests
    {
        [TestMethod]
        public void AddScore_LastFrameSpare_PinsReset_FrameClosedAfterThirdScore()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.IsLastFrame = true;
            testFrame.AddScore(6);
            Assert.AreEqual(4, testFrame.RemainingPins);
            testFrame.AddScore(4);
            Assert.AreEqual(pinCount, testFrame.RemainingPins);
            testFrame.AddScore(5);
            Assert.IsTrue(testFrame.IsClosed);
        }

        [TestMethod]
        public void AddScore_LastFrameThreeStrikes_PinsReset_FrameClosedAfterThirdScore()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.IsLastFrame = true;
            testFrame.AddScore(pinCount);
            Assert.AreEqual(pinCount, testFrame.RemainingPins);
            testFrame.AddScore(pinCount);
            Assert.AreEqual(pinCount, testFrame.RemainingPins);
            testFrame.AddScore(pinCount);
            Assert.IsTrue(testFrame.IsClosed);
        }

        [TestMethod]
        public void AddScore_NormalFrame_OneScore_FrameIsOpen()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(pinCount/2);
            Assert.AreEqual(pinCount/2, testFrame.RemainingPins);
            Assert.IsFalse(testFrame.IsClosed);
        }

        [TestMethod]
        public void AddScore_NormalFrame_Strike_FrameIsClosed()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(pinCount);
            Assert.AreEqual(0, testFrame.RemainingPins);
            Assert.IsTrue(testFrame.IsClosed);
        }

        [TestMethod]
        public void AddScore_NormalFrame_TwoZeros_FrameIsClosed()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(0);
            testFrame.AddScore(0);
            Assert.AreEqual(pinCount, testFrame.RemainingPins);
            Assert.IsTrue(testFrame.IsClosed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddScore_ScoreExceedsRemainingPins_RaiseException()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(pinCount + 1);
        }

        [TestMethod]
        public void GetNextScore_NoScoresInFrame_ReturnZero()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            Assert.AreEqual(0, testFrame.GetNextScore());
        }

        [TestMethod]
        public void GetNextScore_TwoScoresInFrame_ReturnFirstScore()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(2);
            testFrame.AddScore(5);
            Assert.AreEqual(2, testFrame.GetNextScore());
        }

        [TestMethod]
        public void GetNextTwoScores_NoScoresInFrame_ReturnEmptyList()
        {
            List<int?> scoreList = new List<int?>();
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            CollectionAssert.AreEqual(scoreList, testFrame.GetNextTwoScores());
        }
        
        [TestMethod]
        public void GetNextTwoScores_StrikeInFrame_ReturnStrikeAndScoreInNextFrame()
        {
            Mock<IFrame> nextFrame = new Mock<IFrame>();
            nextFrame.Setup(m => m.GetNextScore()).Returns(4);

            List<int?> scoreList = new List<int?> { 10, 4 };
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.NextFrame = nextFrame.Object;
            testFrame.AddScore(10);
            CollectionAssert.AreEqual(scoreList, testFrame.GetNextTwoScores());
            nextFrame.VerifyAll();
        }

        [TestMethod]
        public void GetNextTwoScores_TwoScoresInFrame_ReturnListOfScores()
        {
            List<int?> scoreList = new List<int?> { 1, 2 };
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(1);
            testFrame.AddScore(2);
            CollectionAssert.AreEqual(scoreList, testFrame.GetNextTwoScores());
        }

        [TestMethod]
        public void TotalScore_LessThanPinCount_SumReturned()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(1);
            testFrame.AddScore(2);
            Assert.AreEqual(3, testFrame.TotalScore);
        }

        [TestMethod]
        public void TotalScore_Spare_NoNextFrame_SumReturned()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(pinCount - 1);
            testFrame.AddScore(1);
            Assert.AreEqual(10, testFrame.TotalScore);
        }

        [TestMethod]
        public void TotalScore_Spare_WithNextFrame_SumAndNextScoreReturned()
        {
            Mock<IFrame> nextFrame = new Mock<IFrame>();
            nextFrame.Setup(m => m.GetNextScore()).Returns(4);

            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.NextFrame = nextFrame.Object;
            testFrame.AddScore(pinCount - 1);
            testFrame.AddScore(1);
            Assert.AreEqual(14, testFrame.TotalScore);
            nextFrame.VerifyAll();
        }

        [TestMethod]
        public void TotalScore_Strike_NoNextFrame_SumReturned()
        {
            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.AddScore(pinCount);
            Assert.AreEqual(10, testFrame.TotalScore);
        }

        [TestMethod]
        public void TotalScore_Strike_WithNextFrame_SumAndNextScoresReturned()
        {
            List<int> scoreList = new List<int> { 1, 2 };
            Mock<IFrame> nextFrame = new Mock<IFrame>();
            nextFrame.Setup(m => m.GetNextTwoScores()).Returns(scoreList);

            int pinCount = 10;
            Frame testFrame = new Frame(pinCount);
            testFrame.NextFrame = nextFrame.Object;
            testFrame.AddScore(pinCount);
            Assert.AreEqual(13, testFrame.TotalScore);
            nextFrame.VerifyAll();
        }
    }
}