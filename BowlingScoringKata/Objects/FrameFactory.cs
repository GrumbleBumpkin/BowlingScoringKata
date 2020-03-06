using BowlingScoringKata.Interfaces;

namespace BowlingScoringKata.Objects
{
    public class FrameFactory : IFrameFactory
    {
        private readonly int _pinCount;

        public FrameFactory()
        {
            _pinCount = 10;
        }

        public FrameFactory(int pinCount)
        {
            _pinCount = pinCount;
        }

        public IFrame BuildFrame()
        {
            return new Frame(_pinCount);
        }
    }
}
