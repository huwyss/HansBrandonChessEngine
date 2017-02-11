using System;
using BaracudaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaracudaChessEngineTest
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
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 1, out score); // level 1
            Move goodMove = new Move("f4e5p");

            Assert.AreEqual(goodMove, actualMove, "White bishop should capture pawn. We are on level 1");
            Assert.AreEqual(2, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 2, out score); // level 2
            Move badMove = new Move("f4e5p");

            Assert.AreNotEqual(badMove, actualMove, "White bishop should not capture pawn.");
            Assert.AreEqual(1, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 3, out score); // level 3
            Move expectedMove = new Move("f4e5p");
            Move expectedMove2 = new Move("f3e5p");

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
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 4, out score); // level 4
            Move badMove = new Move("f4e5p");
            Move badMove2 = new Move("f3e5p");

            Assert.AreNotEqual(badMove, actualMove, "White bishop or knight should not capture black pawn.");
            Assert.AreNotEqual(badMove2, actualMove, "White bishop or knight should not capture black pawn.");
            Assert.AreEqual(-1, score);
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteInCheck_ThenDoNotAttackBlackKing_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 3, out score); // level 3
            Move wrongMove = new Move("h8e8q");
            Move wrongMove2 = new Move("e4d5b");

            Assert.AreNotEqual(wrongMove, actualMove, "White must escape check.");
            Assert.AreNotEqual(wrongMove2, actualMove, "White must escape check.");
        }

        // ---------------------------------------------------------------------------------------------
        // Black is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 1, out score); // level 1
            Move goodMove = new Move("f6e5P");

            Assert.AreEqual(goodMove, actualMove, "Black bishop should capture pawn. We are on level 1");
            Assert.AreEqual(-2, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 2, out score); // level 2
            Move badMove = new Move("f6e5P");

            Assert.AreNotEqual(badMove, actualMove, "Black bishop should not capture pawn.");
            Assert.AreEqual(-1, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchMinimax(evaluator);
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 3, out score); // level 3
            Move expectedMove = new Move("d6e5P");
            Move expectedMove2 = new Move("d7e5P");

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
            MoveGenerator gen = new MoveGenerator();
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
            Move actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 4, out score); // level 4
            Move badMove = new Move("d6e5P");
            Move badMove2 = new Move("d7e5P");

            Assert.AreNotEqual(badMove, actualMove, "Black bishop or knight should not capture white pawn.");
            Assert.AreNotEqual(badMove2, actualMove, "Black bishop or knight should not capture white pawn.");
            Assert.AreEqual(1, score);
        }
    }
}
