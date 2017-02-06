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
            bool whiteHuman = true;
            bool blackHuman = false;

            bool showBoard = true;

            BaracudaEngine _whiteEngine = new BaracudaEngine();
            BaracudaEngine _blackEngine = new BaracudaEngine();

            int moveCount = 1;
            _whiteEngine.SetInitialPosition();
            _blackEngine.SetInitialPosition();

            bool isMoveValid;

            if (showBoard)
            {
                PrintBoard(_whiteEngine);
            }

            while (true)
            {
                if (_whiteEngine.SideToMove() == Definitions.ChessColor.White)
                {
                    Console.WriteLine(moveCount++);
                }

                Console.WriteLine("Next Move: " + _whiteEngine.SideToMove());

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

                    if (moveComputer.ToString() != "")
                    {
                        Console.WriteLine("Computer move: " + moveComputer.ToString());
                    }
                }

                if (showBoard)
                {
                    PrintBoard(_whiteEngine);
                }

                if (_whiteEngine.IsWinner(Definitions.ChessColor.White))
                {
                    Console.WriteLine("\nWhite wins!");
                    break;
                }
                if (_whiteEngine.IsWinner(Definitions.ChessColor.Black))
                {
                    Console.WriteLine("\nBlack wins!");
                    break;
                }
            }

            Console.ReadLine();
        }





        //PrintBoard(_engine);
        //        if (_engine.IsWinner(Definitions.ChessColor.White))
        //        {
        //            Console.WriteLine("\nWhite wins!");
        //            break;
        //        }

        //        Move moveBlack = _engine.DoBestMove(Definitions.ChessColor.Black);
        //        if (moveBlack.ToString() != "")
        //        {
        //            Console.WriteLine("Computer move: " + moveBlack.ToString());
        //            PrintBoard(_engine);
        //            if (_engine.IsWinner(Definitions.ChessColor.Black))
        //            {
        //                Console.WriteLine("\nComputer wins!");
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Computer gives up. You win!");
        //        }
        //    }

        //    Console.ReadLine();
        //}

        private static void PrintBoard(BaracudaEngine engine)
        {
            Console.Write(engine.GetPrintString().Replace("p", "o"));
        }
    }
}
