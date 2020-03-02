using BowlingScoringKata.Interfaces;
using BowlingScoringKata.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BowlingScoringKata.Parsers
{
    class CsvParser : IParser
    {
        private Queue<string> fileLines;
        private readonly IFactory _frameFactory;

        public CsvParser(string filePath, IFactory frameFactory)
        {
            fileLines = new Queue<string>(File.ReadAllLines(filePath));
            _frameFactory = frameFactory;
        }

        public List<IGame> GetTotalScores()
        {
            List<IGame> gamesTotalsInRow = new List<IGame>();
            while (fileLines.Count > 0)
            {
                Queue<int> fileLine = new Queue<int>(fileLines.Dequeue().Split(',').Select(int.Parse).ToList());
                while (fileLine.Count > 0)
                {
                    IGame newGame = new Game(_frameFactory); // TODO: Get rid fo newup.
                    gamesTotalsInRow.Add(newGame);
                    while (newGame.IsFinished == false)
                    {
                        newGame.AddScore(fileLine.Dequeue());
                    }
                }
            }
            return gamesTotalsInRow;
        }

    }
}
