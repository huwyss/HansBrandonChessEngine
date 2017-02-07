using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaracudaChessEngine;

namespace BaracudaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool whiteHuman = false;
            bool blackHuman = false;

            int runStatisticGames = 100; // run statistic = 1 --> only one game
            bool quiet = true; // quiet = true --> no display of moves or board

            BaracudaEngine whiteEngineRandom = new BaracudaEngine(EngineType.DepthOne);
            BaracudaEngine blackEngineRandom = new BaracudaEngine(EngineType.Random);

            int whiteWins = 0;

            bool isMoveValid;

            if (!quiet)
            {
                PrintBoard(whiteEngineRandom);
            }

            for (int i = 0; i < runStatisticGames; i++)
            {
                int moveCount = 1;
                whiteEngineRandom.SetInitialPosition();
                blackEngineRandom.SetInitialPosition();

                while (true)
                {
                    if (!quiet)
                    {
                        if (whiteEngineRandom.SideToMove() == Definitions.ChessColor.White)
                        {
                            Console.WriteLine(moveCount++);
                        }
                    }

                    if (!quiet)
                    {
                        Console.WriteLine("Next Move: " + whiteEngineRandom.SideToMove());
                    }

                    // Human move
                    if ((whiteHuman && whiteEngineRandom.SideToMove() == Definitions.ChessColor.White) ||
                        (blackHuman && whiteEngineRandom.SideToMove() == Definitions.ChessColor.Black))
                    {
                        // human move from console
                        do
                        {
                            Console.WriteLine("Enter your move (ie. e2e4): ");
                            string moveConsoleString = Console.ReadLine();
                            moveConsoleString = moveConsoleString.Trim();
                            isMoveValid = whiteEngineRandom.Move(moveConsoleString);
                            if (!isMoveValid)
                            {
                                Console.WriteLine("Invalid move.");
                            }
                            else
                            {
                                blackEngineRandom.Move(moveConsoleString);
                            }
                        } while (!isMoveValid);
                    }
                    else
                    {
                        Move moveComputer = null;
                        // computer move for white
                        if (!whiteHuman && whiteEngineRandom.SideToMove() == Definitions.ChessColor.White)
                        {
                            moveComputer = whiteEngineRandom.DoBestMove(Definitions.ChessColor.White);
                            blackEngineRandom.Move(moveComputer);
                        }
                        // computer move for black
                        else if (!blackHuman && whiteEngineRandom.SideToMove() == Definitions.ChessColor.Black)
                        {
                            moveComputer = blackEngineRandom.DoBestMove(Definitions.ChessColor.Black);
                            whiteEngineRandom.Move(moveComputer);
                        }

                        if (!quiet && moveComputer.ToString() != "")
                        {
                            Console.WriteLine("Computer move: " + moveComputer.ToString());
                        }
                    }


                    if (!quiet)
                        PrintBoard(whiteEngineRandom);

                    if (whiteEngineRandom.IsWinner(Definitions.ChessColor.White))
                    {
                        if (!quiet)
                            Console.WriteLine("\nWhite wins!");

                        whiteWins++;
                        break;
                    }
                    if (whiteEngineRandom.IsWinner(Definitions.ChessColor.Black))
                    {
                        if (!quiet)
                            Console.WriteLine("\nBlack wins!");

                        break;
                    }
                }

                Console.WriteLine("Games: " + i + " - White wins: " + whiteWins);
            }

            Console.WriteLine("\n\nResult\n\nGames: " + runStatisticGames + " - White wins: " + whiteWins);
            Console.ReadLine();
        }

        private static void PrintBoard(BaracudaEngine engineRandom)
        {
            Console.Write(engineRandom.GetPrintString().Replace("p", "o"));
        }
    }
}
