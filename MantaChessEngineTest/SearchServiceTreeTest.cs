﻿////using System;
////using System.Collections.Generic;
////using MantaChessEngine;
////using Microsoft.VisualStudio.TestTools.UnitTesting;

////namespace MantaChessEngineTest
////{
////    [TestClass]
////    public class SearchServiceTreeTest
////    {
////        [TestMethod]
////        public void CreateSearchTreeTest_When2MovesOnEachLevel_ThenBuildCorrectTree()
////        {
////            var gen = new FakeMoveGenerator();
////            gen.ReturnsWhiteGetAllMoves = new List<IMove>()
////            {
////                new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 4, null),
////                new NormalMove(Piece.MakePiece('N'), 'g', 1, 'f', 3, null),
////            };

////            gen.ReturnsBlackGetAllMoves = new List<IMove>()
////            {
////                new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null),
////                new NormalMove(Piece.MakePiece('n'), 'g', 8, 'f', 6, null),
////            };

////            var target = new SearchMinimaxTree(null, gen);
////            target.SetLevel(2);
////            var board = new Board();

////            target.CreateSearchTree(board, Definitions.ChessColor.White);

////            // check search tree
////            // first white move
////            var moveWhite0 = target.MoveRoot.GetChild(0).Data.Move;
////            Assert.AreEqual(new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 4, null), moveWhite0);

////            var moveBlack00 = target.MoveRoot.GetChild(0).GetChild(0).Data.Move;
////            Assert.AreEqual(new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null), moveBlack00);

////            var moveBlack01 = target.MoveRoot.GetChild(0).GetChild(1).Data.Move;
////            Assert.AreEqual(new NormalMove(Piece.MakePiece('n'), 'g', 8, 'f', 6, null), moveBlack01);

////            // second white move
////            var moveWhite1 = target.MoveRoot.GetChild(1).Data.Move;
////            Assert.AreEqual(new NormalMove(Piece.MakePiece('N'), 'g', 1, 'f', 3, null), moveWhite1);

////            var moveBlack10 = target.MoveRoot.GetChild(1).GetChild(0).Data.Move;
////            Assert.AreEqual(new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null), moveBlack10);

////            var moveBlack11 = target.MoveRoot.GetChild(1).GetChild(1).Data.Move;
////            Assert.AreEqual(new NormalMove(Piece.MakePiece('n'), 'g', 8, 'f', 6, null), moveBlack11);
////        }

////        [TestMethod]
////        public void EvaluateTest_WhenTreeBuilt_ThenEvaluateCorrectly()
////        {
////            // create search tree from test before copied
////            var gen = new FakeMoveGenerator();
////            gen.ReturnsWhiteGetAllMoves = new List<IMove>()
////            {
////                new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 4, null),
////                new NormalMove(Piece.MakePiece('N'), 'g', 1, 'f', 3, null),
////            };

////            gen.ReturnsBlackGetAllMoves = new List<IMove>()
////            {
////                new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null),
////                new NormalMove(Piece.MakePiece('n'), 'g', 8, 'f', 6, null),
////            };

////            FakeEvaluator fakeEval = new FakeEvaluator(new List<float> {-1, 1, 2, 3});
////            var target = new SearchMinimaxTree(fakeEval, gen);
////            target.SetLevel(2);
////            var board = new Board();

////            target.CreateSearchTree(board, Definitions.ChessColor.White);

////            target.Evaluate();

////            Assert.AreEqual(-1, target.MoveRoot.GetChild(0).GetChild(0).Data.Score);
////            Assert.AreEqual(1, target.MoveRoot.GetChild(0).GetChild(1).Data.Score);
////            Assert.AreEqual(2, target.MoveRoot.GetChild(1).GetChild(0).Data.Score);
////            Assert.AreEqual(3, target.MoveRoot.GetChild(1).GetChild(1).Data.Score);
////        }

////        [TestMethod]
////        public void SelectBestMoveTest_WhenTreeEvaluated_ThenSelectBestMove()
////        {
////            // create search tree from test before copied
////            var gen = new FakeMoveGenerator();
////            gen.ReturnsWhiteGetAllMoves = new List<IMove>()
////            {
////                new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 4, null),
////                new NormalMove(Piece.MakePiece('N'), 'g', 1, 'f', 3, null),
////            };

////            gen.ReturnsBlackGetAllMoves = new List<IMove>()
////            {
////                new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null),
////                new NormalMove(Piece.MakePiece('n'), 'g', 8, 'f', 6, null),
////            };

////            FakeEvaluator fakeEval = new FakeEvaluator(new List<float> {-1, 1, 2, 3});
////            var target = new SearchMinimaxTree(fakeEval, gen);
////            var board = new Board();
////            target.SetLevel(2);

////            target.CreateSearchTree(board, Definitions.ChessColor.White);
////            target.Evaluate();
////            var bestMove = target.SelectBestMove();

////            Assert.AreEqual(new NormalMove(Piece.MakePiece('N'), 'g', 1, 'f', 3, null), bestMove);
////        }
////    }
////}
