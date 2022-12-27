using System;
using System.Diagnostics;
using MantaChessEngine;
using MantaBitboardEngine;
using MantaCommon;

/*
Useful commands:

uci
ucinewgame
position startpos moves e2e4
go
go depth 4

*/


namespace MantaUCI
{
    class Program
    {
        static IMantaEngine _engine = null;

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
                var inputWords = input.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (inputWords.Length <= 0)
                    continue;

                var command = inputWords[0];

                if (command.Equals("uci"))
                {
                    Console.WriteLine("id name Manta Bitboard Engine");
                    Console.WriteLine("id author Hans Ulrich Wyss");
                    Console.WriteLine("uciok");
                }
                else if (command.Equals("isready"))
                {
                    Console.WriteLine("readyok");
                }
                else if (command.Equals("ucinewgame"))
                {
                    if (_engine == null)
                    {
                        CreateEngine();
                    }
                }
                else if (command.Equals("position"))
                {
                    if (inputWords.Length >= 2 && inputWords[1].Equals("startpos")) // position startpos ...
                    {
                        if (inputWords.Length >= 3 && inputWords[2].Equals("moves")) // position startpos moves ...
                        {
                            _fenString = "";
                            _movesFromInitPosition = input.Substring(input.IndexOf("moves") + "moves".Length + 1).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        }
                        else // position startpos
                        {
                            _fenString = "";
                            _movesFromInitPosition = new string[0];
                        }
                    }
                    else if (inputWords.Length >= 2 && inputWords[1].Equals("fen")) // position fen
                    {
                        var indexOfMoves = input.IndexOf("moves");
                        if (indexOfMoves < 0)
                        {
                            // example: position fen rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
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
                    else
                    {
                        Console.WriteLine("Syntax error after 'position'");
                    }
                }
                else if (command.Equals("go"))
                {
                    if (inputWords.Length >= 2 && inputWords[1].Equals("depth")) // go depth x
                    {
                        var depth = int.Parse(input.Substring(input.IndexOf("depth") + "depth".Length + 1));
                        AnswerBestMove(depth);
                    }
                    else
                    {
                        // could be:
                        //   go
                        //   go movetime 1000 [ms]
                        //   go wtime w btime x winc y binc z (w, x, y, z in ms)
                        //   go wtime w btime x winc 0 binc movestogo y (w, x in ms, y number of moves ==> moves to do in time w)

                        AnswerBestMove(4);
                    }
                }
                else if (command.Equals("quit"))
                {
                    _running = false;
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
            }
        }

        private static void SetStartPosition()
        {
            if (string.IsNullOrEmpty(_fenString))
            {
                _engine.SetInitialPosition();
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
            UciMoveRating bestMove = null;
            _engine.ClearPreviousPV();
            _engine.SetAdditionalSelectiveDepth(1);

            for (int currentDepth = 1; currentDepth <= depth; currentDepth++)
            {
                SetStartPosition();
                DoMoves();
                _engine.SetMaxSearchDepth(currentDepth);
                _stopwatch.Restart();
                bestMove = _engine.DoBestMove();
                _stopwatch.Stop();
                var scoreFromEngine = bestMove.MovingColor == ChessColor.White
                    ? bestMove.Score
                    : - bestMove.Score;

                string principalVariation = string.Empty;
                foreach (var move in bestMove.PrincipalVariation)
                {
                    principalVariation += move + " ";
                }

                var duration = _stopwatch.ElapsedMilliseconds;
                var nps = duration != 0 ? (int)(1000 * bestMove.EvaluatedPositions / duration) : 0;

                Console.WriteLine("info depth " + bestMove.Depth + " seldepth " + bestMove.SelectiveDepth + " score cp " + scoreFromEngine + " nodes " + bestMove.EvaluatedPositions + " nps " + nps + " time " + duration + " pv " + principalVariation );
            }
            
            Console.WriteLine("bestmove " + bestMove.Move);
        }

        private static void CreateEngine()
        {
            _engine = GetMantaBitboardEngine();

            ////_engine = GetMantaEngine();
        }

        private static IMantaEngine GetMantaBitboardEngine()
        {
            var engine = new MantaBitboardEngine.MantaBitboardEngine();

            return engine;
        }

        private static IMantaEngine GetMantaEngine()
        {
            var engine = new MantaEngine(EngineType.AlphaBeta);
            ////_engine = new MantaEngine(EngineType.MinimaxPosition);
            ////_engine = new MantaEngine(EngineType.Random);
            engine.SetMaxSearchDepth(3);

            return engine;
        }

    }
}
