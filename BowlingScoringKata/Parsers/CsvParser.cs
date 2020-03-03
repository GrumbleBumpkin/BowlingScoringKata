using BowlingScoringKata.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace BowlingScoringKata.Parsers
{
    class CsvParser
    {
        private Queue<string> fileLines;
        private readonly IGameFactory _gameFactory;

        public CsvParser(string filePath, IGameFactory gameFactory)
        {
            fileLines = new Queue<string>(File.ReadAllLines(filePath));
            _gameFactory = gameFactory;
        }

        public List<List<IGame>> GetTotalScores()
        {
            List<List<IGame>> rowsOfGames = new List<List<IGame>>();
            while (fileLines.Count > 0)
            {
                rowsOfGames.Add(new StringParser(fileLines.Dequeue(), _gameFactory).GetTotalScores());
            }
            return rowsOfGames;
        }
    }
}
