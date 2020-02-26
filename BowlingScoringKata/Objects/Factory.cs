using BowlingScoringKata.Interfaces;

namespace BowlingScoringKata.Objects
{
    public class Factory : IFactory
    {
        private readonly int _pinCount;

        public Factory()
        {
            _pinCount = 10;
        }

        public Factory(int pinCount)
        {
            _pinCount = pinCount;
        }

        public IFrame BuildFrame()
        {
            return new Frame(_pinCount);
        }
    }
}
