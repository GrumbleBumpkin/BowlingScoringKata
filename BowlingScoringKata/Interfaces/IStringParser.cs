using System.Collections.Generic;

namespace BowlingScoringKata.Interfaces
{
    public interface IStringParser
    {
        List<IGame> GetGamesInString(string scores);
    }
}