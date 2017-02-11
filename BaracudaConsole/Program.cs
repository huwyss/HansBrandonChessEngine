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
            bool humanGame = false;

            // ----------------------------------------------------------------

            bool whiteHuman = false;
            bool blackHuman = false;

            int runStatisticGames = 1; // run statistic = 1 --> only one game
            bool quiet = false; // quiet = true --> no display of moves or board

            if (humanGame)
            {
                whiteHuman = true;
                blackHuman = false;
                runStatisticGames = 1;
                quiet = false;
            }

            BaracudaEngine whiteEngine = new BaracudaEngine(EngineType.MinmaxPosition);
            BaracudaEngine blackEngine = new BaracudaEngine(EngineType.Minmax);

            int whiteWins = 0;
            bool isMoveValid;

            for (int i = 0; i < runStatisticGames; i++)
            {
                int moveCount = 1;
                whiteEngine.SetInitialPosition();
                blackEngine.SetInitialPosition();

                if (!quiet)
                {
                    PrintBoard(whiteEngine);
                }

                while (true)
                {
                    if (!quiet)
                    {
                        if (whiteEngine.SideToMove() == Definitions.ChessColor.White)
                        {
                            Console.WriteLine(moveCount++);
                        }
                    }

                    if (!quiet)
                    {
                        Console.WriteLine("Next Move: " + whiteEngine.SideToMove());
                    }

                    // Human move
                    if ((whiteHuman && whiteEngine.SideToMove() == Definitions.ChessColor.White) ||
                        (blackHuman && whiteEngine.SideToMove() == Definitions.ChessColor.Black))
                    {
                        // human move from console
                        do
                        {
                            Console.WriteLine("Enter your move (ie. e2e4): ");
                            string moveConsoleString = Console.ReadLine();
                            moveConsoleString = moveConsoleString.Trim();
                            isMoveValid = whiteEngine.Move(moveConsoleString);
                            if (!isMoveValid)
                            {
                                Console.WriteLine("Invalid move.");
                            }
                            else
                            {
                                blackEngine.Move(moveConsoleString);
                            }
                        } while (!isMoveValid);
                    }
                    else
                    {
                        Move moveComputer = null;
                        // computer move for white
                        if (!whiteHuman && whiteEngine.SideToMove() == Definitions.ChessColor.White)
                        {
                            moveComputer = whiteEngine.DoBestMove(Definitions.ChessColor.White);
                            blackEngine.Move(moveComputer);
                        }
                        // computer move for black
                        else if (!blackHuman && whiteEngine.SideToMove() == Definitions.ChessColor.Black)
                        {
                            moveComputer = blackEngine.DoBestMove(Definitions.ChessColor.Black);
                            whiteEngine.Move(moveComputer);
                        }

                        if (!quiet && moveComputer.ToString() != "")
                        {
                            Console.WriteLine("Computer move: " + moveComputer.ToString());
                        }
                    }


                    if (!quiet)
                        PrintBoard(whiteEngine);

                    if (whiteEngine.IsWinner(Definitions.ChessColor.White))
                    {
                        if (!quiet)
                            Console.WriteLine("\nWhite wins!");

                        whiteWins++;
                        break;
                    }
                    if (whiteEngine.IsWinner(Definitions.ChessColor.Black))
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
