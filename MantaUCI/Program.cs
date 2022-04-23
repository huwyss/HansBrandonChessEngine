using System;
using System.Diagnostics;
using MantaChessEngine;

namespace MantaUCI
{
    class Program
    {
        static MantaEngine _engine = null;
        static Board _board = null;
        static string[] _movesFromInitPosition = new string[0];
        static Stopwatch _stopwatch = new Stopwatch();

        static string _fenString = "";

        static void Main(string[] args)
        {
            var _running = true;
            CreateEngine();

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
                    _fenString = "";
                    _movesFromInitPosition = input.Substring(24).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                }
                else if (input.StartsWith("position fen"))
                {
                    var indexOfMoves = input.IndexOf("moves");
                    if (indexOfMoves < 0)
                    {
                        _fenString = input.Substring(13);
                        var error = _engine.SetFenPosition(_fenString);
                        if (string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine(error);
                        }
                        else
                        {
                            _movesFromInitPosition = new string[0];
                        }
                    }
                    else
                    {
                        // example: position fen rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1 moves e2e4 e7e5
                        var lengthOfFen = indexOfMoves - 14;
                        var lengthOfMoves = input.Length - indexOfMoves - 6;
                        _fenString = input.Substring(13, lengthOfFen);
                        var movesString = input.Substring(indexOfMoves + 6, lengthOfMoves);
                        
                        var error = _engine.SetFenPosition(_fenString);
                        if (string.IsNullOrEmpty(error))
                        {
                            _movesFromInitPosition = movesString.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            DoMoves();
                        }
                        else
                        {
                            Console.WriteLine(error);
                        }
                    }
                }
                else if (input.Equals("position startpos"))
                {
                    _fenString = "";
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
            if (string.IsNullOrEmpty(_fenString))
            {
                _board.SetInitialPosition();
            }
            else
            {
                _engine.SetFenPosition(_fenString);
            }
        }

        private static void DoMoves()
        {
            foreach (var move in _movesFromInitPosition)
            {
                var valid = _engine.MoveUci(move);
                if (!valid)
                {
                    Console.WriteLine("moveerror");
                }
            }
        }

        private static void AnswerBestMove(int depth)
        {
            MoveRating bestMove = null;

            for (int currentDepth = 1; currentDepth <= depth; currentDepth++)
            {
                SetStartPosition();
                DoMoves();
                _engine.SetMaxSearchDepth(currentDepth);
                _stopwatch.Restart();
                bestMove = _engine.DoBestMove();
                _stopwatch.Stop();
                var scoreFromEngine = bestMove.Move.Color == Definitions.ChessColor.White
                    ? bestMove.Score
                    : - bestMove.Score;

                string principalVariation = string.Empty;
                foreach (var move in bestMove.PrincipalVariation)
                {
                    principalVariation += move.ToUciString() + " ";
                }

                var duration = _stopwatch.ElapsedMilliseconds;
                var nps = duration != 0 ? (int)(1000 * bestMove.EvaluatedPositions / duration) : 0;

                Console.WriteLine("info depth " + bestMove.Depth + " seldepth " + bestMove.PruningCount + " score cp " + scoreFromEngine + " nodes " + bestMove.EvaluatedPositions + " nps " + nps + " time " + duration + " pv " + principalVariation );
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
