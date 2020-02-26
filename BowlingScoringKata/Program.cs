using BowlingScoringKata.Objects;
using System;

namespace BowlingScoringKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            Game newGame = new Game(factory);
            while (true)
            {
                int remainingPins = newGame.GetRemainingPinsInFrame();
                int frameNumber = newGame.Frames.Count;
                Console.WriteLine($"(Frame {frameNumber}) Enter the number of pins knocked down (remaining pins in frame = {remainingPins}):");
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int pinsScored))
                {
                    if (pinsScored >= 0 && pinsScored <= remainingPins)
                    {
                        newGame.AddScore(pinsScored);
                    }
                    else
                    {
                        Console.WriteLine($"Your score must be between 0 and the number of remaning pins in the frame ({remainingPins}).");
                    }
                }
                else
                {
                    Console.WriteLine("Input may only be integers.");
                }
                if (newGame.IsFinished == true)
                {
                    break;
                }
            }
            Console.WriteLine($"Final game score: {newGame.GetTotalScore()}");
        }
    }
}
