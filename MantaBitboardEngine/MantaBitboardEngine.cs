using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaBitboardEngine
{
    public class MantaBitboardEngine : IMantaEngine
    {
        private readonly Bitboards _board;
        private readonly BitMoveGenerator _moveGenerator;

        public MantaBitboardEngine()
        {
            _board = new Bitboards();
            _board.Initialize();
            _moveGenerator = new BitMoveGenerator(_board);
        }

        public void SetInitialPosition()
        {
            _board.ClearAllPieces();
            _board.SetInitialPosition();
        }

        public void SetPosition(string position)
        {
            _board.ClearAllPieces();
            _board.SetPosition(position);
        }

        public string SetFenPosition(string fen)
        {
            _board.ClearAllPieces();
            return _board.SetFenPosition(fen);
        }

        public string GetString()
        {
            return _board.GetPositionString;
        }

        public string GetPrintString()
        {
            return _board.GetPrintString;
        }

        public bool MoveUci(string moveStringUci)
        {
            return true;
        }

        public bool Move(string moveStringUser)
        {
            return true;
        }

        private bool Move(BitMove move)
        {
            _board.Move(move);
            return true;
        }

        public bool UndoMove()
        {
            _board.Back();
            return true;
        }

        public UciMoveRating DoBestMove(ChessColor color)
        {
            return null;
        }

        public UciMoveRating DoBestMove()
        {
            return null;
        }

        public ChessColor SideToMove()
        {
            return _board.BoardState.SideToMove;
        }

        public bool IsCheck(ChessColor color)
        {
            return _moveGenerator.IsCheck(color);
        }

        public void Back()
        {
            _board.Back();
            _board.Back();
        }

        public void SetMaxSearchDepth(int maxDepth)
        {
        }

        public void SetAdditionalSelectiveDepth(int additionalSelectiveDepth)
        {
        }

        public void ClearPreviousPV()
        {
        }

        public UInt64 Perft(int depth)
        {
            if (depth == 0)
            {
                return 1;
            }

            UInt64 nodes = 0;

            var moves = _moveGenerator.GetLegalMoves(SideToMove()).ToList();

            //// Console.Write(_board.GetPrintString);
            //// Console.WriteLine();


            foreach (var move in moves)
            {
                ////Console.WriteLine(move.ToPrintString());
                Move(move);
                nodes += Perft(depth - 1);
                UndoMove();
            }

            return nodes;
        }

        ////public UInt64 PerftCastling(int depth, IMove moveParam)
        ////{
        ////    if (depth == 0)
        ////    {
        ////        return moveParam is CastlingMove ? (UInt64)1 : 0;
        ////    }

        ////    UInt64 nodes = 0;

        ////    var moves = _moveGenerator.GetLegalMoves(_board, SideToMove());
        ////    foreach (var move in moves)
        ////    {
        ////        Move(move);
        ////        nodes += PerftCastling(depth - 1, move);
        ////        UndoMove();
        ////    }

        ////    return nodes;
        ////}

        public void Divide(int depth)
        {
            Console.WriteLine($"Divide depth {depth}");

            var moves = _moveGenerator.GetLegalMoves(SideToMove());
            foreach (var move in moves)
            {
                Move(move);
                var nodes = Perft(depth - 1);
                UndoMove();

                Console.WriteLine($"Move {move} : {nodes}");
            }
        }

    }
}
