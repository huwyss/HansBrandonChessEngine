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

            BaracudaEngine _whiteEngine = new BaracudaEngine();
            BaracudaEngine _blackEngine = new BaracudaEngine();

            int whiteWins = 0;

            bool isMoveValid;

            if (!quiet)
            {
                PrintBoard(_whiteEngine);
            }

            for (int i = 0; i < runStatisticGames; i++)
            {
                int moveCount = 1;
                _whiteEngine.SetInitialPosition();
                _blackEngine.SetInitialPosition();

                while (true)
                {
                    if (!quiet)
                    {
                        if (_whiteEngine.SideToMove() == Definitions.ChessColor.White)
                        {
                            Console.WriteLine(moveCount++);
                        }
                    }

                    if (!quiet)
                    {
                        Console.WriteLine("Next Move: " + _whiteEngine.SideToMove());
                    }

                    // Human move
                    if ((whiteHuman && _whiteEngine.SideToMove() == Definitions.ChessColor.White) ||
                        (blackHuman && _whiteEngine.SideToMove() == Definitions.ChessColor.Black))
                    {
                        // human move from console
                        do
                        {
                            Console.WriteLine("Enter your move (ie. e2e4): ");
                            string moveConsoleString = Console.ReadLine();
                            moveConsoleString = moveConsoleString.Trim();
                            isMoveValid = _whiteEngine.Move(moveConsoleString);
                            if (!isMoveValid)
                            {
                                Console.WriteLine("Invalid move.");
                            }
                            else
                            {
                                _blackEngine.Move(moveConsoleString);
                            }
                        } while (!isMoveValid);
                    }
                    else
                    {
                        Move moveComputer = null;
                        // computer move for white
                        if (!whiteHuman && _whiteEngine.SideToMove() == Definitions.ChessColor.White)
                        {
                            moveComputer = _whiteEngine.DoBestMove(Definitions.ChessColor.White);
                            _blackEngine.Move(moveComputer);
                        }
                        // computer move for black
                        else if (!blackHuman && _whiteEngine.SideToMove() == Definitions.ChessColor.Black)
                        {
                            moveComputer = _blackEngine.DoBestMove(Definitions.ChessColor.Black);
                            _whiteEngine.Move(moveComputer);
                        }

                        if (!quiet && moveComputer.ToString() != "")
                        {
                            Console.WriteLine("Computer move: " + moveComputer.ToString());
                        }
                    }


                    if (!quiet)
                        PrintBoard(_whiteEngine);

                    if (_whiteEngine.IsWinner(Definitions.ChessColor.White))
                    {
                        if (!quiet)
                            Console.WriteLine("\nWhite wins!");

                        whiteWins++;
                        break;
                    }
                    if (_whiteEngine.IsWinner(Definitions.ChessColor.Black))
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

        private static void PrintBoard(BaracudaEngine engine)
        {
            Console.Write(engine.GetPrintString().Replace("p", "o"));
        }
    }
}
