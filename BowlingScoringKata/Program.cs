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

        static void PrintGameScores(List<IGame> games)
        {
            for (int i = 0; i < games.Count; i++)
            {
                IGame game = games[i];
                string scoreReport = $"\tGame {i + 1}: {game.GetTotalScore()} points";
                if (game.IsFinished == false)
                {
                    scoreReport += $" (Unfinished with last score in frame {game.Frames.Count})";
                }
                Console.WriteLine(scoreReport);
            }
        }

        static void RunInteractiveGame()
        {
            FrameFactory frameFactory = new FrameFactory();
            Game newGame = new Game(frameFactory);
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
            while (true)
            {
                Console.WriteLine("Specify CSV file path or type 'exit' to quit.");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    break;
                }
                RunFileParser(input);
                Console.WriteLine();
            }
        }

        static void RunFileParser(string filePath)
        {
            FrameFactory frameFactory = new FrameFactory();
            GameFactory gameFactory = new GameFactory(frameFactory);
            try
            {
                CsvParser csvParser = new CsvParser(filePath, gameFactory);
                List<List<IGame>> rowsOfGames = csvParser.GetTotalScores();
                for (int i = 0; i < rowsOfGames.Count; i++)
                {
                    Console.WriteLine($"CSV File Line {i + 1}");
                    PrintGameScores(rowsOfGames[i]);
                }
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is FileNotFoundException || ex is FormatException)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void RunStringParser()
        {
            while (true)
            {
                Console.WriteLine("Specify comma-separated scores or type 'exit' to quit.");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    break;
                }
                RunStringParser(input);
                Console.WriteLine();
            }
        }

        static void RunStringParser(string inputString)
        {
            FrameFactory frameFactory = new FrameFactory();
            GameFactory gameFactory = new GameFactory(frameFactory);
            try
            {
                StringParser stringParser = new StringParser(inputString, gameFactory);
                PrintGameScores(stringParser.GetTotalScores());
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is FormatException)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
