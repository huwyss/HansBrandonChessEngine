using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaChessEngine;

namespace MantaUCI
{
    class Program
    {
        static MantaEngine _engine;
        static Board _board = new Board();
        static Definitions.ChessColor _currentColor;

        static void Main(string[] args)
        {
            var running = true;

            while (running)
            {
                var input = Console.ReadLine().Trim();

                if (input.Equals("uci"))
                {
                    Console.WriteLine("id name Manta Chess Engine");
                    Console.WriteLine("id author Hans Ulrich Wyss");
                    Console.WriteLine("uciok");
                }
                else if (input.Equals("isready"))
                {
                    Console.WriteLine("readyok");
                }
                else if (input.Equals("ucinewgame"))
                {
                    _engine = new MantaEngine(EngineType.MinimaxPosition);
                    _engine.SetMaxSearchDepth(2);
                    _engine.SetBoard(_board);
                }
                else if (input.StartsWith("position startpos moves"))
                {
                    _board.SetInitialPosition();
                    var moves = input.Substring(24).Split("".ToCharArray());
                    _currentColor = Definitions.ChessColor.White;
                    foreach (var move in moves)
                    {
                        var valid = _engine.Move(move);
                        if  (!valid)
                        {
                            Console.WriteLine("moveerror");
                        }

                        _currentColor = Helper.GetOppositeColor(_currentColor);
                    }
                }
                else if (input.StartsWith("position startpos"))
                {
                    _board.SetInitialPosition();
                    _currentColor = Definitions.ChessColor.White;
                }
                else if (input.Equals("go"))
                {
                    AnswerBestMove();
                }
                else if (input.StartsWith("go depth"))
                {
                    var depth = int.Parse(input.Substring(8));
                    _engine.SetMaxSearchDepth(Math.Min(4, depth));
                    AnswerBestMove();
                }
            }
        }

        private static void AnswerBestMove()
        {
            var bestMove = _engine.DoBestMove(_currentColor);
            Console.WriteLine("bestmove " + bestMove.Move.ToUciString());
        }
    }
}
