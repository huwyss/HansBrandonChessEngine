////using System;
////using System.Text;
////using System.Collections.Generic;
////using MantaChessEngine;
////using Microsoft.VisualStudio.TestTools.UnitTesting;

////namespace MantaChessEngineTest
////{
////    [TestClass]
////    public class MoveTreeTest
////    {
////        [TestMethod]
////        public void AddChildrenTest_WhenAddingChild_ThenHasNextChildTrue()
////        {
////            var target = new MoveTree();
////            Assert.IsFalse(target.HasCurrentNextChild());

////            var moves = new List<IMove>() {new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, null)};
////            target.AddChildren(moves);

////            Assert.IsTrue(target.HasCurrentNextChild(), "should have next child.");
////        }

////        [TestMethod]
////        public void AddChildrenTest_WhenAddingChildren_ThenCanGotoChildren_Depth1()
////        {
////            var target = new MoveTree();
////            var move1 = new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, null);
////            var move2 = new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 3, null);
////            var move3 = new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null);
////            var move4 = new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 6, null);

////            var movesWhite = new List<IMove> { move1, move2 };
////            target.AddChildren(movesWhite);

////            Assert.IsTrue(target.HasCurrentNextChild(), "should have next child.");

////            target.GotoNextChild();
////            Assert.AreEqual(move1, target.CurrentMove);
////            target.GotoParent();
////            Assert.IsTrue(target.HasCurrentNextChild());
////            target.GotoNextChild();
////            Assert.AreEqual(move2, target.CurrentMove);
////            target.GotoParent();
////            Assert.IsFalse(target.HasCurrentNextChild(), "Should not have next child.");
////            Assert.IsTrue(target.IsCurrentRoot(), "We should be at the root node.");
////        }


////        [TestMethod]
////        public void AddChildrenTest_WhenAddingChildren_ThenCanGotoChildren_Depth2()
////        {
////            var target = new MoveTree();
////            var move1 = new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, null);
////            var move2 = new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 3, null);
////            var move3 = new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null);
////            var move4 = new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 6, null);

////            var movesWhite = new List<IMove> {move1, move2};
////            var movesBlack = new List<IMove> {move3, move4};

////            target.AddChildren(movesWhite);
////            Assert.IsTrue(target.HasCurrentNextChild(), "should have next child.");
////            target.GotoNextChild();
////            Assert.AreEqual(move1, target.CurrentMove);
////            target.AddChildren(movesBlack);
////            target.GotoNextChild();
////            Assert.AreEqual(move3, target.CurrentMove);
////            target.GotoParent();
////            Assert.IsTrue(target.HasCurrentNextChild());
////            target.GotoNextChild();
////            Assert.AreEqual(move4, target.CurrentMove);
////            target.GotoParent();
////            Assert.IsFalse(target.HasCurrentNextChild(), "Should not have next child.");

////            target.GotoParent(); // now at root
////            Assert.IsTrue(target.IsCurrentRoot());
////            Assert.IsTrue(target.HasCurrentNextChild());
////            target.GotoNextChild();
////            Assert.AreEqual(move2, target.CurrentMove);
////            target.AddChildren(movesBlack);
////            Assert.IsTrue(target.HasCurrentNextChild(), "second half should have next child. we just added them.");
////            target.GotoNextChild();
////            Assert.AreEqual(move3, target.CurrentMove);
////            target.GotoParent();
////            Assert.IsTrue(target.HasCurrentNextChild(), "second half should have next child. we just added them.");
////            target.GotoNextChild();
////            Assert.AreEqual(move4, target.CurrentMove);
////            target.GotoParent();
////            Assert.IsFalse(target.HasCurrentNextChild(), "Should not have next child.");
////            target.GotoParent();
////            Assert.IsTrue(target.IsCurrentRoot(), "We should be at the root node.");
////        }

////    }
////}
