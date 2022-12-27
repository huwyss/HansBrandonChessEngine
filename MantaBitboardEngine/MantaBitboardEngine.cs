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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Bitboards _board;
        private readonly BitMoveGenerator _moveGenerator;
        private readonly HelperBitboards _helperBits;
        private readonly BitEvaluator _evaluator;
        private readonly BitSearchAlphaBeta _search;
        private readonly BitMoveFactory _moveFactory;

        public MantaBitboardEngine()
        {
            _board = new Bitboards();
            _helperBits = new HelperBitboards();
            _moveGenerator = new BitMoveGenerator(_board, _helperBits);
            _evaluator = new BitEvaluator(_helperBits);
            _search = new BitSearchAlphaBeta(_evaluator, _moveGenerator, 4);
            _moveFactory = new BitMoveFactory(_helperBits);
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
            var move = _moveFactory.MakeMoveUci(_board, moveStringUci);
            if (move.Equals(BitMove.CreateEmptyMove()))
            {
                return false;
            }

            bool valid = _moveGenerator.IsMoveValid(move);
            if (valid)
            {
                _board.Move(move);
            }

            return valid;
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
            BitMoveRating nextMove = _search.Search(_board, color);
            _board.Move(nextMove.Move);
            _log.Debug("Score: " + nextMove.Score);

            UciMoveRating uciRating = BitMoveRatingConverter.NewFrom(nextMove);

            return uciRating;
        }

        public UciMoveRating DoBestMove()
        {
            BitMoveRating nextMove = _search.Search(_board, _board.BoardState.SideToMove);
            _board.Move(nextMove.Move);
            _log.Debug("Score: " + nextMove.Score);

            UciMoveRating uciRating = BitMoveRatingConverter.NewFrom(nextMove);

            return uciRating;
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
            if (_search != null)
            {
                _search.SetMaxDepth(maxDepth);
            }
        }

        public void SetAdditionalSelectiveDepth(int additionalSelectiveDepth)
        {
            if (_search != null)
            {
                _search.SetAdditionalSelectiveDepth(additionalSelectiveDepth);
            }
        }

        public void ClearPreviousPV()
        {
            _search.ClearPreviousPV();
        }

        public UInt64 Perft(int depth)
        {
            if (depth == 0)
            {
                return 1;
            }

            UInt64 nodes = 0;

            var moves = _moveGenerator.GetLegalMoves(SideToMove()).ToArray();

            ////Console.Write(_board.GetPrintString);
            ////Console.WriteLine();


            foreach (var move in moves)
            {
                Move(move);
                if (IsCheck(move.MovingColor))
                {
                    UndoMove();
                    continue;
                }

                ////Console.WriteLine(move.ToPrintString());
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
