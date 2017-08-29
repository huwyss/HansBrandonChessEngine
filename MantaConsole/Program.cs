using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaChessEngine;

namespace MantaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool humanGame = true;

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

            Board board = new Board();
            board.SetInitialPosition();
            MantaEngine whiteEngine = new MantaEngine(EngineType.MinmaxPosition);
            MantaEngine blackEngine = new MantaEngine(EngineType.MinmaxPosition);
            whiteEngine.SetBoard(board);
            blackEngine.SetBoard(board);

            float whiteWins = 0;
            bool isMoveValid;

            for (int i = 0; i < runStatisticGames; i++)
            {
                int moveCount = 1;

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
                            if (moveConsoleString == "back" || moveConsoleString == "b")
                            {
                                board.Back();
                                board.Back();
                                moveCount -= 2;
                                break;
                            }
                            isMoveValid = whiteEngine.Move(moveConsoleString);
                            if (!isMoveValid)
                            {
                                Console.WriteLine("Invalid move.");
                            }
                        } while (!isMoveValid);
                    }
                    else
                    {
                        IMove moveComputer = null;
                        // computer move for white
                        if (!whiteHuman && whiteEngine.SideToMove() == Definitions.ChessColor.White)
                        {
                            moveComputer = whiteEngine.DoBestMove(Definitions.ChessColor.White);

                            if (moveComputer is NoLegalMove)
                            {
                                // check for stall mate and check mate
                                if (whiteEngine.IsCheck(Definitions.ChessColor.White))
                                {
                                    Console.WriteLine("\nBlack wins!");
                                }
                                else
                                {
                                    Console.WriteLine("\nBlack is stall mate. Game is draw!");
                                    whiteWins += 0.5f;
                                }
                                break;
                            }
                        }
                        // computer move for black
                        else if (!blackHuman && whiteEngine.SideToMove() == Definitions.ChessColor.Black)
                        {
                            moveComputer = blackEngine.DoBestMove(Definitions.ChessColor.Black);
                            //whiteEngine.Move(moveComputer);

                            if (moveComputer is NoLegalMove)
                            {
                                // check for stall mate and check mate
                                if (whiteEngine.IsCheck(Definitions.ChessColor.Black))
                                {
                                    Console.WriteLine("\nWhite wins!");
                                    whiteWins++;
                                }
                                else
                                {
                                    Console.WriteLine("\nWhite is stall mate. Game is draw!");
                                    whiteWins += 0.5f;
                                }
                                break;
                            }
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
                        {
                            Console.WriteLine("\nBlack wins!");
                        }

                        break;
                    }
                }

                Console.WriteLine("Games: " + i+1 + " - White wins: " + whiteWins);
            }

            Console.WriteLine("\n\nResult\n\nGames: " + runStatisticGames + " - White wins: " + whiteWins);
            Console.ReadLine();
        }

        private static void PrintBoard(MantaEngine engineRandom)
        {
            Console.Write(engineRandom.GetPrintString().Replace("p", "o"));
        }
    }
}
