using BowlingScoringKata.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScoringKata.Objects
{
    public class Factory : IFactory
    {
        public IFrame BuildFrame()
        {
            return new Frame();
        }
    }
}
