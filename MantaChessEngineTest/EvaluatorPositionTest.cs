﻿using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class EvaluatorPositionTest
    {
        [TestMethod]
        public void EvaluateTest_WhenPawnInCenter_Then1_2()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(1.2f, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenPawnIn3rdRank_Then1_1()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(1.1f, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenKnightIsAtBorder_ThenItsAShame()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......N" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(0.95f * 3f, score);
        }
        
        [TestMethod]
        public void EvaluateTest_WhenKnightIsNotAtBorder_ThenItsOk()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....N..." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(3f, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteHasTwoBishopAndBlackHasBishopAndKnight_ThenWhiteBetter()
        {
            Board board = new Board(null);
            string position = "........" +
                              "....bn.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....BB.." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(true, score > 0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenBlackHasDoubleBishop_ThenBlackBetter()
        {
            Board board = new Board(null);
            string position = "........" +
                              "....bb.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....NB.." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(true, score < -0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteDidCastle_ThenWhiteBetter()
        {
            var gen = new MoveGenerator();
            Board board = new Board(gen);
            string position = "rnbqk..r" +
                              "ppppppbp" +
                              ".....np." +
                              "........" +
                              "........" +
                              ".....NP." +
                              "PPPPPPBP" +
                              "RNBQK..R";
            board.SetPosition(position);

            // white castling
            board.Move("e1g1");
            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);
            Assert.AreEqual(true, score > 0.1f, "White did castling. so white should be better.");

            // black castling
            board.Move("e8g8");
            score = target.Evaluate(board);
            Assert.AreEqual(true, score == 0, "White and Black did castling. They are equal.");

            // take black move back
            board.Back();
            score = target.Evaluate(board);
            Assert.AreEqual(true, score > 0.1f, "Black castling was taken back. so white should be better.");
        }
    }
}