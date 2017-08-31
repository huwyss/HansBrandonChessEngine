using System;
using System.Collections.Generic;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class SearchServiceTreeTest
    {
        [TestMethod]
        public void CreateSearchTreeTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_White()
        {
            var gen = new FakeMoveGenerator();
            gen.ReturnsWhiteGetAllMoves = new List<MoveBase>()
            {
                new NormalMove('P', 'e', 2, 'e', 4, '.'),
                new NormalMove('N', 'g', 1, 'f', 3, '.'),
            };

            gen.ReturnsBlackGetAllMoves = new List<MoveBase>()
            {
                new NormalMove('p', 'e', 7, 'e', 5, '.'),
                new NormalMove('n', 'g', 8, 'f', 6, '.'),
            };

            var target = new SearchMinimaxTree(null, gen);
            var board = new Board();

            var searchTree = target.CreateSearchTree(board, Definitions.ChessColor.White);

            // check search tree
            // first white move
            var moveWhite0 = target.MoveRoot.GetChild(0).Data;
            Assert.AreEqual(new NormalMove('P', 'e', 2, 'e', 4, '.'), moveWhite0);

            var moveBlack00 = target.MoveRoot.GetChild(0).GetChild(0).Data;
            Assert.AreEqual(new NormalMove('p', 'e', 7, 'e', 5, '.'), moveBlack00);

            var moveBlack01 = target.MoveRoot.GetChild(0).GetChild(1).Data;
            Assert.AreEqual(new NormalMove('n', 'g', 8, 'f', 6, '.'), moveBlack01);

            // second white move
            var moveWhite1 = target.MoveRoot.GetChild(1).Data;
            Assert.AreEqual(new NormalMove('N', 'g', 1, 'f', 3, '.'), moveWhite1);

            var moveBlack10 = target.MoveRoot.GetChild(1).GetChild(0).Data;
            Assert.AreEqual(new NormalMove('p', 'e', 7, 'e', 5, '.'), moveBlack10);

            var moveBlack11 = target.MoveRoot.GetChild(1).GetChild(1).Data;
            Assert.AreEqual(new NormalMove('n', 'g', 8, 'f', 6, '.'), moveBlack11);
        }

    }
}
