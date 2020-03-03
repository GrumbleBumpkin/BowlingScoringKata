using BowlingScoringKata.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoringKata.Parsers
{
    public class StringParser
    {
        private Queue<int> scoresQueue;
        private readonly IGameFactory _gameFactory;

        public StringParser(string scores, IGameFactory gameFactory)
        {
            scoresQueue = new Queue<int>(scores.Split(',').Select(int.Parse).ToList());
            _gameFactory = gameFactory;
        }

        public List<IGame> GetTotalScores()
        {
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
