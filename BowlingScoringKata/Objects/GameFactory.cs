using BowlingScoringKata.Interfaces;

namespace BowlingScoringKata.Objects
{
    class GameFactory : IGameFactory
    {
        private readonly int _frameCount;
        private readonly IFrameFactory _factory;

        public GameFactory(IFrameFactory frameFactory)
        {
            _frameCount = 10;
            _factory = frameFactory;
        }

        public GameFactory(IFrameFactory frameFactory, int frameCount)
        {
            _frameCount = frameCount;
            _factory = frameFactory;
        }

        public IGame BuildGame()
        {
            return new Game(_factory, _frameCount);
        }
    }
}
