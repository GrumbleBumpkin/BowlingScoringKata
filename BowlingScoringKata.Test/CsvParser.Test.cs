using BowlingScoringKata.Interfaces;
using BowlingScoringKata.Objects;
using BowlingScoringKata.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace BowlingScoringKata.Test
{
    [TestClass]
    public class CsvParserIntegrationTests
    {
        string tempFolderPath;

        [TestInitialize]
        public void CreateTempFolder()
        {
            tempFolderPath = Path.Join(Path.GetTempPath(), "CsvParserIntegrationTests");
            Directory.CreateDirectory(tempFolderPath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ParseCsv_FileDoesNotExist_RaisesException()
        {
            string testFilePath = "ThisFileDoesn'tExist";
            IFrameFactory frameFactory = new FrameFactory();
            IGameFactory gameFactory = new GameFactory(frameFactory);
            IStringParser stringParser = new StringParser(gameFactory);

            ICsvParser csvParser = new CsvParser(stringParser);
            csvParser.GetGamesInCsv(testFilePath);
        }

        [TestMethod]
        public void ParseCsv_FileExists_ReturnsParsedList()
        {
            string testString = "10,10,10,10,10,10,10,10,10,10,10,10\n10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10";
            string tempFilePath = Path.Join(tempFolderPath, "PerfectGames.csv");
            using (StreamWriter streamWriter = new StreamWriter(tempFilePath))
            {
                streamWriter.WriteLine(testString);
            }
            
            IFrameFactory frameFactory = new FrameFactory();
            IGameFactory gameFactory = new GameFactory(frameFactory);
            IStringParser stringParser = new StringParser(gameFactory);

            ICsvParser csvParser = new CsvParser(stringParser);
            List<List<IGame>> parsedResult = csvParser.GetGamesInCsv(tempFilePath);
            Assert.AreEqual(2, parsedResult.Count);
            Assert.AreEqual(1, parsedResult[0].Count);
            Assert.AreEqual(2, parsedResult[1].Count);
        }

        [TestCleanup]
        public void EnsureTempFolderRemoval()
        {
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
            }
        }
    }
}
