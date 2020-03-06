using BowlingScoringKata.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace BowlingScoringKata.Parsers
{
    public class CsvParser : ICsvParser
    {
        private Queue<string> fileLines;
        private IStringParser _stringParser;

        public CsvParser(IStringParser stringParser)
        {
            _stringParser = stringParser;
        }

        public List<List<IGame>> GetGamesInCsv(string filePath)
        {
            if (File.Exists(filePath) != true)
            {
                throw new FileNotFoundException($"File at '{filePath}' could not be found.");
            }
            fileLines = new Queue<string>(File.ReadAllLines(filePath));
            List<List<IGame>> rowsOfGames = new List<List<IGame>>();
            while (fileLines.Count > 0)
            {
                rowsOfGames.Add(_stringParser.GetGamesInString(fileLines.Dequeue()));
            }
            return rowsOfGames;
        }
    }
}
