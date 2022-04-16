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
        static MantaEngine _engine = null;
        static Board _board = null;
        static Definitions.ChessColor _currentColor;
        static string[] _movesFromInitPosition = null;

        static void Main(string[] args)
        {
            var _running = true;

            while (_running)
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
                    if (_engine == null)
                    {
                        CreateEngine();
                    }
                }
                // position startpos moves e2e4
                else if (input.StartsWith("position startpos moves"))
                {
                    _movesFromInitPosition = input.Substring(24).Split("".ToCharArray());
                }
                else if (input.StartsWith("position startpos"))
                {
                    _movesFromInitPosition = new string[0];
                }
                else if (input.StartsWith("go depth"))
                {
                    var depth = int.Parse(input.Substring(8));
                    AnswerBestMove(depth);
                }
                // could be:
                //   go movetime 1000 [ms]
                //   go wtime w btime x winc y binc z (w, x, y, z in ms)
                //   go wtime w btime x winc 0 binc movestogo y (w, x in ms, y number of moves ==> moves to do in time w)
                else if (input.StartsWith("go"))
                {
                    AnswerBestMove(4);
                }
                else if (input.Equals("quit"))
                {
                    _running = false;
                }
            }
        }

        private static void SetStartPosition()
        {
            _board.SetInitialPosition();
            _currentColor = Definitions.ChessColor.White;
            foreach (var move in _movesFromInitPosition)
            {
                var valid = _engine.MoveUci(move);
                if (!valid)
                {
                    Console.WriteLine("moveerror");
                }

                _currentColor = Helper.GetOppositeColor(_currentColor);
            }
        }

        private static void AnswerBestMove(int depth)
        {
            MoveRating bestMove = null;

            for (int currentDepth = 1; currentDepth <= depth; currentDepth++)
            {
                SetStartPosition();
                _engine.SetMaxSearchDepth(currentDepth);
                bestMove = _engine.DoBestMove(_currentColor);
                var scoreFromEngine = _currentColor == Definitions.ChessColor.White
                    ? (int)(100 * bestMove.Score)
                    : -(int)(100 * bestMove.Score);
                Console.WriteLine("info depth " + bestMove.Depth + " seldepth " + bestMove.PruningCount + " nodes " + bestMove.EvaluatedPositions + " pv " + bestMove.Move.ToUciString() + " score cp " + scoreFromEngine);
            }
            
            Console.WriteLine("bestmove " + bestMove.Move.ToUciString());
        }

        private static void CreateEngine()
        {
            _board = new Board();
            _engine = new MantaEngine(EngineType.AlphaBeta);
            ////_engine = new MantaEngine(EngineType.MinimaxPosition);
            ////_engine = new MantaEngine(EngineType.Random);
            _engine.SetMaxSearchDepth(3);
            _engine.SetBoard(_board);
        }
    }
}
