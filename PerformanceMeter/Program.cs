using System;
using MantaChessEngine;
using System.Diagnostics;

namespace PerformanceMeter
{
    class Program
    {
        static MantaEngine _engine = null;
        static Board _board = null;
        static Stopwatch _stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            CreateEngine();

            Console.WriteLine("Perft tests, see https://www.chessprogramming.org/Perft_Results\n");

            var depth = 4;

            ExecutePerft("Initial Position", "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", depth);
            ExecutePerft("Position 2", "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", depth);
            ExecutePerft("Position 3", "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - ", depth);
            ExecutePerft("Position 4", "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", depth);
            ExecutePerft("Position 5", "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8  ", depth);
            ExecutePerft("Position 6", "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", depth);

            Console.WriteLine("\nTests done. Hit enter to quit.");
            Console.ReadLine();
        }

        private static void ExecutePerft(string title, string fen, int depth)
        {
            Console.WriteLine(title);
            for (int n = 1; n <= depth; n++)
            {
                _engine.SetFenPosition(fen);
                _stopwatch.Restart();
                var nodes = _engine.Perft(n);
                _stopwatch.Stop();
                var timeMs = _stopwatch.ElapsedMilliseconds != 0 ? _stopwatch.ElapsedMilliseconds : 1;
                Console.WriteLine($"Perft ({n}): Nodes: {nodes}, time: {timeMs}ms, nps: {(int)nodes / timeMs}k");
            }

            Console.WriteLine();
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



/*

Perft tests, see https://www.chessprogramming.org/Perft_Results

Initial Position
Perft (1): Nodes: 20, time: 10ms, nps: 2k
Perft (2): Nodes: 400, time: 4ms, nps: 100k
Perft (3): Nodes: 8902, time: 94ms, nps: 94k
Perft (4): Nodes: 197281, time: 1899ms, nps: 103k

Position 2
Perft (1): Nodes: 48, time: 2ms, nps: 24k
Perft (2): Nodes: 2039, time: 26ms, nps: 78k
Perft (3): Nodes: 97862, time: 1257ms, nps: 77k
Perft (4): Nodes: 4085662, time: 49308ms, nps: 82k   // the website sais it is 4085603 nodes

Position 3
Perft (1): Nodes: 14, time: 1ms, nps: 14k
Perft (2): Nodes: 191, time: 1ms, nps: 191k
Perft (3): Nodes: 2812, time: 17ms, nps: 165k
Perft (4): Nodes: 43238, time: 236ms, nps: 183k

Position 4
Perft (1): Nodes: 6, time: 1ms, nps: 6k
Perft (2): Nodes: 264, time: 4ms, nps: 66k
Perft (3): Nodes: 9467, time: 128ms, nps: 73k
Perft (4): Nodes: 422333, time: 5972ms, nps: 70k

Position 5
Perft (1): Nodes: 44, time: 1ms, nps: 44k
Perft (2): Nodes: 1486, time: 17ms, nps: 87k
Perft (3): Nodes: 62379, time: 745ms, nps: 83k
Perft (4): Nodes: 2103487, time: 24058ms, nps: 87k

Position 6
Perft (1): Nodes: 46, time: 1ms, nps: 46k
Perft (2): Nodes: 2079, time: 24ms, nps: 86k
Perft (3): Nodes: 89890, time: 1082ms, nps: 83k
Perft (4): Nodes: 3894594, time: 42945ms, nps: 90k


Tests done. Hit enter to quit.

*/