using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class SearchMinimaxTest
    {
        // ---------------------------------------------------------------------------------------------
        // White is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "k......." +
                                 "........" +
                                 "...p...." +
                                 "....p..." +
                                 ".....B.." +
                                 "........" +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 1, out score); // level 1
            MoveBase goodMove = new NormalMove('B', 'f', 4, 'e', 5, 'p');

            Assert.AreEqual(goodMove, actualMove, "White bishop should capture pawn. We are on level 1");
            Assert.AreEqual(2, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "k......." +
                                 "........" +
                                 "...p...." +
                                 "....p..." +
                                 ".....B.." +
                                 "........" +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 2, out score); // level 2
            MoveBase badMove = new NormalMove('B', 'f', 4, 'e', 5, 'p');

            Assert.AreNotEqual(badMove, actualMove, "White bishop should not capture pawn.");
            Assert.AreEqual(1, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....p..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

                                 //"k......." +   After position
                                 //"...n...." +
                                 //"........" +
                                 //"....N..." +   can also be B
                                 //"........" +
                                 //"........" +
                                 //"........" +
                                 //"K.......";

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 3, out score); // level 3
            MoveBase expectedMove = new NormalMove('B', 'f', 4, 'e', 5, 'p');
            MoveBase expectedMove2 = new NormalMove('N', 'f', 3, 'e', 5, 'p');

            bool passed = actualMove.Equals(expectedMove) ||
                          actualMove.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "White bishop or knight should capture black pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....p..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 4, out score); // level 4
            MoveBase badMove = new NormalMove('B', 'f', 4, 'e', 5, 'p');
            MoveBase badMove2 = new NormalMove('N', 'f', 3, 'e', 5, 'p');

            Assert.AreNotEqual(badMove, actualMove, "White bishop or knight should not capture black pawn.");
            Assert.AreNotEqual(badMove2, actualMove, "White bishop or knight should not capture black pawn.");
            Assert.AreEqual(-1, score);
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteInCheck_ThenDoNotAttackBlackKing_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "....q..R" +
                                 "........" +
                                 "....k..." +
                                 "...b...." +
                                 "....K..." +
                                 "........" +
                                 "........" +
                                 "........";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 3, out score); // level 3
            MoveBase wrongMove = new NormalMove('R', 'h', 8, 'e', 8, 'q');
            MoveBase wrongMove2 = new NormalMove('K', 'e', 4, 'd', 5, 'b');

            Assert.AreNotEqual(wrongMove, actualMove, "White must escape check.");
            Assert.AreNotEqual(wrongMove2, actualMove, "White must escape check.");
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteIsCheckMate_ThenNoLegalMove_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "...k...." +
                                 "...q...." +
                                 "...K....";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 3, out score); // level 3
            IMove expectedMove = new NoLegalMove();

            Assert.AreEqual(expectedMove, actualMove, "White is check mate. no legal move possible.");
        }

        // ---------------------------------------------------------------------------------------------
        // Black is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "k......." +
                                 "........" +
                                 ".....b.." +
                                 "....P..." +
                                 "...P...." +
                                 "........" +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 1, out score); // level 1
            MoveBase goodMove = new NormalMove('b', 'f', 6, 'e', 5, 'P');

            Assert.AreEqual(goodMove, actualMove, "Black bishop should capture pawn. We are on level 1");
            Assert.AreEqual(-2, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "k......." +
                                 "........" +
                                 ".....b.." +
                                 "....P..." +
                                 "...P...." +
                                 "........" +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 2, out score); // level 2
            MoveBase badMove = new NormalMove('b', 'f', 6, 'e', 5, 'P');

            Assert.AreNotEqual(badMove, actualMove, "Black bishop should not capture pawn.");
            Assert.AreEqual(-1, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....P..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            //"k......." +   After position
            //"........" +
            //"........" +
            //"....n..." +   can also be b
            //"........" +
            //".....N.." +
            //"........" +
            //"K.......";

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 3, out score); // level 3
            MoveBase expectedMove = new NormalMove('b', 'd', 6, 'e', 5, 'P');
            MoveBase expectedMove2 = new NormalMove('n', 'd', 7, 'e', 5, 'P');

            bool passed = actualMove.Equals(expectedMove) ||
                          actualMove.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "Black bishop or knight should capture white pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var board = new Board(gen);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....P..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 4, out score); // level 4
            MoveBase badMove = new NormalMove('b', 'd', 6, 'e', 5, 'P');
            MoveBase badMove2 = new NormalMove('n', 'd', 7, 'e', 5, 'P');

            Assert.AreNotEqual(badMove, actualMove, "Black bishop or knight should not capture white pawn.");
            Assert.AreNotEqual(badMove2, actualMove, "Black bishop or knight should not capture white pawn.");
            Assert.AreEqual(1, score);
        }
    }
}
