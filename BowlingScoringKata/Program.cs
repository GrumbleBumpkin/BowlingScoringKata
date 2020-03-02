using BowlingScoringKata.Interfaces;
using BowlingScoringKata.Objects;
using BowlingScoringKata.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BowlingScoringKata
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    RunFileParser(args[0]);
                }
                else
                {
                    RunStringParser(args[0]);
                }
            }
            else
            {
                bool inputRecognized = false;
                char[] acceptedInput = { 'i', 's', 'f' };
                while (inputRecognized == false)
                {
                    Console.WriteLine("Press <i> for interactive, <s> to parse input strings, or <f> to parse a CSV file.");
                    char input = Console.ReadKey().KeyChar;
                    if (acceptedInput.Contains(input))
                    {
                        inputRecognized = true;
                        Console.WriteLine('\n');
                        switch (input)
                        {
                            case 'i':
                                RunInteractiveGame();
                                break;
                            case 's':
                                RunStringParser();
                                break;
                            case 'f':
                                RunFileParser();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInput not recognized.");
                    }
                }
            }
        }

        static void RunInteractiveGame()
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

        static void RunFileParser()
        {
            Console.WriteLine("Specify CSV file path:");
            string filePath = Console.ReadLine();
            RunFileParser(filePath);
        }

        static void RunFileParser(string filePath)
        {
            Factory factory = new Factory();
            CsvParser csvParser = new CsvParser(filePath, factory);
            List<IGame> allScores = csvParser.GetTotalScores();
            for (int i = 0; i < allScores.Count; i++)
            {
                Console.WriteLine($"Game {i}: {allScores[i].GetTotalScore()}");
            } 
        }

        static void RunStringParser()
        {
            Console.WriteLine("Specify comma-separated scores:");
            string scores = Console.ReadLine();
            RunStringParser(scores);
        }

        static void RunStringParser(string inputString)
        {

        }
    }
}
