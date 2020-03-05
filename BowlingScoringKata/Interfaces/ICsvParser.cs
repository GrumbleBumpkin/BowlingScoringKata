using System.Collections.Generic;

namespace BowlingScoringKata.Interfaces
{
    public interface ICsvParser
    {
       List<List<IGame>> GetGamesInCsv(string filePath);
    }
}
