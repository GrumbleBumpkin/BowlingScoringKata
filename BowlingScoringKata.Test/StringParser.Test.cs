using BowlingScoringKata.Interfaces;
using BowlingScoringKata.Objects;
using BowlingScoringKata.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BowlingScoringKata.Test
{
    [TestClass]
    public class StringParserIntegrationTests
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseString_StingIsInvalid_RaiseException()
        {
            string testString = "These,are,not,integers";
            IFrameFactory frameFactory = new FrameFactory();
            IGameFactory gameFactory = new GameFactory(frameFactory);

            IStringParser stringParser = new StringParser(gameFactory);
            stringParser.GetGamesInString(testString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseString_ValueOutOfRange_RaiseException()
        {
            string testString = "10,10,10,10,10,99999,10,10,10,10,10";
            IFrameFactory frameFactory = new FrameFactory();
            IGameFactory gameFactory = new GameFactory(frameFactory);

            IStringParser stringParser = new StringParser(gameFactory);
            stringParser.GetGamesInString(testString);
        }

        [TestMethod]
        public void ParseCsv_FileExists_ReturnsParsedList()
        {
            string testString = "10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10";

            IFrameFactory frameFactory = new FrameFactory();
            IGameFactory gameFactory = new GameFactory(frameFactory);
            IStringParser stringParser = new StringParser(gameFactory);

            List<IGame> parsedResult = stringParser.GetGamesInString(testString);
            Assert.AreEqual(3, parsedResult.Count);
        }
    }
}
