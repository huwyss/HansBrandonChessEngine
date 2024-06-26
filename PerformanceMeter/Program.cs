﻿using System;
using HansBrandonChessEngine;
using HansBrandonBitboardEngine;
using HBCommon;
using System.Diagnostics;

namespace PerformanceMeter
{
    class Program
    {
        static IHansBrandonEngine _engine = null;
        static Stopwatch _stopwatch = new Stopwatch();
        static Stopwatch _stopwatchTotal = new Stopwatch();

        static void Main(string[] args)
        {
            ////CreateEngine();
            CreateBitboardEngine();

            Console.WriteLine("Perft tests, see https://www.chessprogramming.org/Perft_Results\n");
            _stopwatchTotal.Start();
            var depth = 5;
            ExecutePerft("Initial Position", "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", depth);
            depth = 4;
            ExecutePerft("Position 2", "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", depth);
            depth = 6;
            ExecutePerft("Position 3", "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - ", depth);
            depth = 5;
            ExecutePerft("Position 4", "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", depth);
            depth = 4;
            ExecutePerft("Position 5", "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8  ", depth);
            ExecutePerft("Position 6", "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", depth);

            _stopwatchTotal.Stop();
            Console.WriteLine($"Time: {_stopwatchTotal.ElapsedMilliseconds / 1000} seconds.");
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

        // and move d5 x e6     d7-d5
        //          e6 x d7 ep

        private static void ExecuteDivide(string title, string fen, int depth)
        {
            Console.WriteLine(title);
            _engine.SetFenPosition(fen);

            _engine.MoveUci("d5e6");
            _engine.MoveUci("e7c5");
            _engine.MoveUci("e6e7");

            _stopwatch.Restart();
            _engine.Divide(depth);
            _stopwatch.Stop();
            var timeMs = _stopwatch.ElapsedMilliseconds != 0 ? _stopwatch.ElapsedMilliseconds : 1;

            Console.WriteLine();
        }

        private static void CreateEngine()
        {
            var engine = new HansBrandonEngine();
            ////_engine = new HansBrandonEngine(EngineType.AlphaBeta);
            ////_engine = new HansBrandonEngine(EngineType.MinimaxPosition);
            ////_engine = new HansBrandonEngine(EngineType.Random);
            engine.SetMaxSearchDepth(3);

            _engine = engine as IHansBrandonEngine;
        }

        private static void CreateBitboardEngine()
        {
            _engine = new HansBrandonBitboardEngine.HansBrandonBitboardEngine();
        }
    }
}



/*

Perft tests, see https://www.chessprogramming.org/Perft_Results

Initial Position
Perft (1): Nodes: 20, time: 15ms, nps: 1k
Perft (2): Nodes: 400, time: 5ms, nps: 80k
Perft (3): Nodes: 8902, time: 74ms, nps: 120k
Perft (4): Nodes: 197281, time: 1566ms, nps: 125k
Perft (5): Nodes: 4865609, time: 25902ms, nps: 187k

Position 2
Perft (1): Nodes: 48, time: 11ms, nps: 4k
Perft (2): Nodes: 2039, time: 21ms, nps: 97k
Perft (3): Nodes: 97862, time: 739ms, nps: 132k
Perft (4): Nodes: 4085603, time: 35962ms, nps: 113k
Perft (5): Nodes: 193690690, time: 1466263ms, nps: 132k

Position 3
Perft (1): Nodes: 14, time: 11ms, nps: 1k
Perft (2): Nodes: 191, time: 1ms, nps: 191k
Perft (3): Nodes: 2812, time: 13ms, nps: 216k
Perft (4): Nodes: 43238, time: 167ms, nps: 258k
Perft (5): Nodes: 674624, time: 2473ms, nps: 272k
Perft (6): Nodes: 11030083, time: 39957ms, nps: 276k

Position 4
Perft (1): Nodes: 6, time: 14ms, nps: 0k
Perft (2): Nodes: 264, time: 7ms, nps: 37k
Perft (3): Nodes: 9467, time: 140ms, nps: 67k
Perft (4): Nodes: 422333, time: 3479ms, nps: 121k
Perft (5): Nodes: 15833292, time: 124080ms, nps: 127k

Position 5
Perft (1): Nodes: 44, time: 32ms, nps: 1k
Perft (2): Nodes: 1486, time: 28ms, nps: 53k
Perft (3): Nodes: 62379, time: 1099ms, nps: 56k
Perft (4): Nodes: 2103487, time: 17530ms, nps: 119k

Position 6
Perft (1): Nodes: 46, time: 11ms, nps: 4k
Perft (2): Nodes: 2079, time: 18ms, nps: 115k
Perft (3): Nodes: 89890, time: 663ms, nps: 135k
Perft (4): Nodes: 3894594, time: 29254ms, nps: 133k


Tests done. Hit enter to quit.

*/

/*
 
Bitboard Generator, 25.12.2022

Perft tests, see https://www.chessprogramming.org/Perft_Results

Initial Position
Perft (1): Nodes: 20, time: 14ms, nps: 1k
Perft (2): Nodes: 400, time: 1ms, nps: 400k
Perft (3): Nodes: 8902, time: 4ms, nps: 2225k
Perft (4): Nodes: 197281, time: 67ms, nps: 2944k
Perft (5): Nodes: 4865609, time: 1575ms, nps: 3089k

Position 2
Perft (1): Nodes: 48, time: 1ms, nps: 48k
Perft (2): Nodes: 2039, time: 1ms, nps: 2039k
Perft (3): Nodes: 97862, time: 31ms, nps: 3156k
Perft (4): Nodes: 4085603, time: 1300ms, nps: 3142k
Perft (5): Nodes: 193690690, time: 63595ms, nps: 3045k

Position 3
Perft (1): Nodes: 14, time: 1ms, nps: 14k
Perft (2): Nodes: 191, time: 1ms, nps: 191k
Perft (3): Nodes: 2812, time: 1ms, nps: 2812k
Perft (4): Nodes: 43238, time: 16ms, nps: 2702k
Perft (5): Nodes: 674624, time: 237ms, nps: 2846k
Perft (6): Nodes: 11030083, time: 3817ms, nps: 2889k

Position 4
Perft (1): Nodes: 6, time: 1ms, nps: 6k
Perft (2): Nodes: 264, time: 1ms, nps: 264k
Perft (3): Nodes: 9467, time: 3ms, nps: 3155k
Perft (4): Nodes: 422333, time: 142ms, nps: 2974k
Perft (5): Nodes: 15833292, time: 5488ms, nps: 2885k

Position 5
Perft (1): Nodes: 44, time: 1ms, nps: 44k
Perft (2): Nodes: 1486, time: 1ms, nps: 1486k
Perft (3): Nodes: 62379, time: 21ms, nps: 2970k
Perft (4): Nodes: 2103487, time: 672ms, nps: 3130k

Position 6
Perft (1): Nodes: 46, time: 1ms, nps: 46k
Perft (2): Nodes: 2079, time: 1ms, nps: 2079k
Perft (3): Nodes: 89890, time: 30ms, nps: 2996k
Perft (4): Nodes: 3894594, time: 1337ms, nps: 2912k

Time: 78 seconds.

Tests done. Hit enter to quit.
 
 
 
 */