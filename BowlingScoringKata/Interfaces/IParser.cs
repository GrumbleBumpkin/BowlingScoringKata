using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScoringKata.Interfaces
{
    public interface IParser
    {
        List<IGame> GetTotalScores();
    }
}
