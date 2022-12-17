using System;
using MantaChessEngine;
using MantaCommon;
using log4net;

namespace MantaConsole
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public enum GameType
        {
            HumanVsComputer,
            ComputerVsComputerOnce,
            ComputerVsComputerStatistic
        }

        static void Main(string[] args)
        {
            _log.Info("ManteChessEngine started");

            var gameType = GameType.HumanVsComputer;
            var whiteLevel = 2;
            var blackLevel = 2;
            
            bool whiteHuman;
            bool blackHuman;
            int runStatisticGames; // number of games to be played
            bool quiet;            // true = print board with every move, false = print only who wins

            switch (gameType)
            {
                case GameType.HumanVsComputer:
                    whiteHuman = true;
                    blackHuman = false;
                    runStatisticGames = 1;
                    quiet = false;
                    break;

                case GameType.ComputerVsComputerOnce:
                    whiteHuman = false;
                    blackHuman = false;
                    runStatisticGames = 1;
                    quiet = false;
                    break;

                case GameType.ComputerVsComputerStatistic:
                    whiteHuman = false;
                    blackHuman = false;
                    runStatisticGames = 100;
                    quiet = true;
                    break;

                default:
                    whiteHuman = false;
                    blackHuman = false;
                    runStatisticGames = 1;
                    quiet = false;
                    break;
            }

            MantaEngine whiteEngine = new MantaEngine(EngineType.MinimaxPosition);
            whiteEngine.SetMaxSearchDepth(whiteLevel);
            MantaEngine blackEngine = new MantaEngine(EngineType.MinimaxPosition);
            blackEngine.SetMaxSearchDepth(blackLevel);

            DefineLogLevel(quiet);

            float whiteWins = 0;
            float blackWins = 0;
            bool isMoveValid;

            for (int i = 0; i < runStatisticGames; i++)
            {
                Board board = new Board();
                board.SetInitialPosition();

                //board.SetPosition("........" +
                //                  "........" +
                //                  "........" +
                //                  "........" +
                //                  "........" +
                //                  "..k....." +
                //                  ".......q" +
                //                  ".K......"); // black should find winning move if human moves 1. Kb1a1, with level 4 it does find it but doesnt find it's checkmate.

                //board.SetPosition("........" +
                //                  "........" +
                //                  "........" +
                //                  "........" +
                //                  "........" +
                //                  ".k......" +
                //                  "q......." +  // white is check mate --> bug engine thinks it's stall mate
                //                  "K.......");

                //board.SetPosition("........" +
                //                  "........" +
                //                  "........" +
                //                  "........" +
                //                  "........" +
                //                  ".k......" +
                //                  ".r......" +  // white is stall mate --> ok
                //                  "K.......");

                //board.SetPosition(  "r.b....." +
                //                    "o.b....." +
                //                    ".....k.." +
                //                    "..N.o..." +
                //                    ".oP..o.." +
                //                    "q....o.." +
                //                    "P......." +
                //                    "RK......");

                //board.SetPosition("........" +
                //                  "........" +
                //                  "........" +
                //                  "........" +
                //                  "........" +
                //                  ".....k.." +
                //                  ".....r.." +  
                //                  "K.......");

                //board.SetPosition(".rbqkb.r" +
                //                  "ppppp.pp" +
                //                  "..n....." +
                //                  "......N." +
                //                  "..B....." +
                //                  "........" +
                //                  "PPPP.PPP" +
                //                  "R.BnK..R");

                whiteEngine.SetBoard(board);
                blackEngine.SetBoard(board);

                int moveCount = 1;

                if (!quiet)
                {
                    PrintBoard(whiteEngine);
                }

                while (true)
                {
                    if (!quiet)
                    {
                        if (whiteEngine.SideToMove() == ChessColor.White)
                        {
                            Console.WriteLine(moveCount++);
                        }

                        Console.WriteLine("Next Move: " + whiteEngine.SideToMove());
                    }
                    else
                    {
                        Console.Write(".");
                    }

                    // Human move
                    if ((whiteHuman && whiteEngine.SideToMove() == ChessColor.White) ||
                        (blackHuman && whiteEngine.SideToMove() == ChessColor.Black))
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
                                moveCount --;
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
                        MoveRating moveComputer = null;
                        // computer move for white
                        if (!whiteHuman && whiteEngine.SideToMove() == ChessColor.White)
                        {
                            ////moveComputer = whiteEngine.DoBestMove(ChessColor.White);
                            //// todo fix if required

                            if (!quiet && moveComputer.Move.ToString() != "")
                            {
                                Console.WriteLine("Computer move: " + moveComputer.Move.ToPrintString());
                            }

                            if (moveComputer.Move is NoLegalMove)
                            {
                                // check for stall mate and check mate
                                if (whiteEngine.IsCheck(ChessColor.White))
                                {
                                    Console.WriteLine("\nBlack wins!");
                                    blackWins++;
                                }
                                else
                                {
                                    Console.WriteLine("\nWhite is stall mate. Game is draw!");
                                    whiteWins += 0.5f;
                                    blackWins += 0.5f;
                                }
                                break;
                            }
                            else if (moveComputer.Score == Definitions.ScoreWhiteWins)
                            {
                                Console.WriteLine("\nWhite wins!");
                                whiteWins++;
                                break;
                            }
                        }
                        // computer move for black
                        else if (!blackHuman && whiteEngine.SideToMove() == ChessColor.Black)
                        {
                            ////moveComputer = blackEngine.DoBestMove(ChessColor.Black);
                            //// todo fix if required

                            if (!quiet && moveComputer.Move.ToString() != "")
                            {
                                Console.WriteLine("Computer move: " + moveComputer.Move.ToPrintString());
                            }

                            if (moveComputer.Move is NoLegalMove)
                            {
                                // check for stall mate and check mate
                                if (whiteEngine.IsCheck(ChessColor.Black))
                                {
                                    Console.WriteLine("\nWhite wins!");
                                    whiteWins++;
                                }
                                else
                                {
                                    Console.WriteLine("\nBlack is stall mate. Game is draw!");
                                    whiteWins += 0.5f;
                                    blackWins += 0.5f;
                                }
                                break;
                            }
                            else if (moveComputer.Score == Definitions.ScoreBlackWins)
                            {
                                Console.WriteLine("\nBlack wins!");
                                blackWins++;
                                break;
                            }
                        }
                    }

                    if (!quiet)
                    {
                        PrintBoard(whiteEngine);
                    }
                }

                if (!quiet)
                {
                    PrintBoard(whiteEngine);
                }

                Console.WriteLine("Games: " + (i+1).ToString() + " - White score: " + whiteWins + " - Black score: " + blackWins);
            }

            Console.WriteLine("\n\nResult\n\nGames: " + runStatisticGames + " - White score: " + whiteWins + " - Black score: " + blackWins);
            Console.ReadLine();
        }

        private static void PrintBoard(MantaEngine engineRandom)
        {
            Console.Write(engineRandom.GetPrintString().Replace("p", "o"));
        }
        
        private static void DefineLogLevel(bool quiet)
        {
            log4net.Core.Level level = quiet ? log4net.Core.Level.Error : log4net.Core.Level.Debug;
            foreach (ILog logger in log4net.LogManager.GetCurrentLoggers())
            {
                ((log4net.Repository.Hierarchy.Logger)logger.Logger).Level = level;
            }
        }
    }
}
