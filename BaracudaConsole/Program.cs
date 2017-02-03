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
            Board board = new Board();
            board.SetInitialPosition();
            MoveGenerator generator = new MoveGenerator();
            generator.SetBoard(board);

            Console.Write(board.GetString("\n"));

            Console.Write("\n\nEnter your move (ie. e2e4): ");
            string moveConsoleString = Console.ReadLine();
            moveConsoleString = moveConsoleString.Trim();

            Move moveConsole = generator.GetValidMove(moveConsoleString);
            if (generator.IsMoveValid(moveConsole))
            {
                board.Move(moveConsole);
            }

            Console.Write(board.GetString("\n"));

            Console.ReadLine();


        }
    }
}
