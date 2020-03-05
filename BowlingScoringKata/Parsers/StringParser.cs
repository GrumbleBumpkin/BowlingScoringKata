using BowlingScoringKata.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoringKata.Parsers
{
    public class StringParser : IStringParser
    {
        private readonly IGameFactory _gameFactory;

        public StringParser(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public List<IGame> GetGamesInString(string scores)
        {
            Queue<int> scoresQueue = new Queue<int>(scores.Split(',').Select(int.Parse).ToList());
            List<IGame> gamesInRow = new List<IGame>();
            while (scoresQueue.Count > 0)
            {
                IGame newGame = _gameFactory.BuildGame();
                gamesInRow.Add(newGame);
                while (newGame.IsFinished == false)
                {
                    if (scoresQueue.Count == 0)
                    {
                        break;
                    }
                    newGame.AddScore(scoresQueue.Dequeue());
                }
            }
            return gamesInRow;
        }
    }
}
