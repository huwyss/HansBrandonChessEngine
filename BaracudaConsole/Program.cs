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
            bool isMoveValid;
            Move moveConsole;

            Board board = new Board();
            board.SetInitialPosition();
            MoveGenerator generator = new MoveGenerator();
            generator.SetBoard(board);

            Console.WriteLine(board.GetString("\n"));
            while (true)
            {
                do
                {
                    Console.WriteLine("Enter your move (ie. e2e4): ");
                    string moveConsoleString = Console.ReadLine();
                    moveConsoleString = moveConsoleString.Trim();
                    moveConsole = generator.GetValidMove(moveConsoleString);
                    isMoveValid = generator.IsMoveValid(moveConsole);
                    if (!isMoveValid)
                    {
                        Console.WriteLine("Invalid move.");
                    }
                } while (!isMoveValid);

                board.Move(moveConsole);
                Console.Write(board.GetString("\n"));

                var possibleMovesComputer = generator.GetAllMoves(Definitions.ChessColor.Black);
                int numberPossibleMoves = possibleMovesComputer.Count;
                var rand = new Random(123);
                if (numberPossibleMoves > 0)
                {
                    int randomMoveIndex = rand.Next(0, numberPossibleMoves - 1);
                    Move moveBlack = possibleMovesComputer[randomMoveIndex];
                    Console.WriteLine("Computer move: " + moveBlack.ToString());
                    board.Move(moveBlack);
                    Console.Write(board.GetString("\n"));
                }
                else
                {
                    Console.WriteLine("Computer gives up. You win!");
                }
            }

            Console.ReadLine();


        }
    }
}
