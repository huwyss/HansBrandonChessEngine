﻿using System;
using System.Collections.Generic;
using System.Linq;
using HBCommon;

namespace HansBrandonChessEngine
{
    public enum EngineType
    {
        Random,
        Minimax,
        MinimaxPosition,
        AlphaBeta
    }

    public class HansBrandonEngine : IHansBrandonEngine
    {
        private const int StandardHashSize = 2 * 1024 * 1024;

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IMoveGenerator<IMove> _moveGenerator;
        private IMoveFactory<IMove> _moveFactory;
        private IMoveRatingFactory<IMove> _moveRatingFactory;
        private ISearchService<IMove> _search;
        private IEvaluator _evaluator;
        private IBoard _board;
        private IHashtable _hashtable;

        public HansBrandonEngine()
            : this(EngineType.AlphaBeta, StandardHashSize)
        {
        }

        public HansBrandonEngine(EngineType engineType, int hashSize)
        {
            _hashtable = new Hashtable(hashSize);
            _board = new Board(_hashtable);
            _moveFactory = new MoveFactory(_board);
            _moveGenerator = new MoveGenerator(_board);
            _moveRatingFactory = new MoveRatingFactory(_moveGenerator);

            switch (engineType)
            {
                case EngineType.Random:
                    _search = new SearchRandom(_moveGenerator);
                    break;

                case EngineType.Minimax:
                    _evaluator = new EvaluatorSimple(_board);
                    _search = new SearchMinimax(_board, _evaluator, _moveGenerator);
                    break;
                
                case EngineType.MinimaxPosition:
                    _evaluator = new EvaluatorPosition(_board);
                    _search = new SearchMinimax(_board, _evaluator, _moveGenerator);
                    break;
                
                // strongest --------------------------------
                case EngineType.AlphaBeta:
                    _evaluator = new EvaluatorPosition(_board);
                    _search = new GenericSearchAlphaBeta<IMove>(_board, _evaluator, _moveGenerator, _hashtable, _moveFactory, _moveRatingFactory, 4);
                    break;
                // -------------------------------------------

                default:
                    throw new Exception("No engine type defined.");
            }
        }

        public void SetBoard(Board board)
        {
            _board = board;
        }

        public void SetInitialPosition()
        {
            _board.SetInitialPosition();
        }

        public void SetPosition(string position)
        {
            _board.SetPosition(position);
        }

        public string SetFenPosition(string fen)
        {
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
            IMove move = _moveFactory.MakeMoveUci(moveStringUci);
            if (move == null)
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
            IMove move = _moveFactory.MakeMoveUci(moveStringUser); // todo should be for user string. remove user string in all parts of the HansBrandonEngine. it should work with uci strings.
            if (move == null)
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

        private bool Move(IMove move)
        {
            _board.Move(move);
            return true;
        }

        public bool UndoMove()
        {
            _board.Back();
            return true;
        }

        public UciMoveRating CalculateBestMove(ChessColor color)
        {
            IMoveRating<IMove> nextMove = _search.Search(color);
            _board.Move(nextMove.Move);
            _log.Debug("Score: " + nextMove.Score);

            UciMoveRating uciRating = MoveRatingConverter.NewFrom(nextMove);

            return uciRating;
        }

        public UciMoveRating CalculateBestMove(int maxMoveTime)
        {
            IMoveRating<IMove> nextMove = _search.Search(_board.BoardState.SideToMove);
            _log.Debug("Score: " + nextMove.Score);

            UciMoveRating uciRating = MoveRatingConverter.NewFrom(nextMove);

            return uciRating;
        }

        public void AbortSearch()
        {
            _search.AbortSearch();
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

            var moves = _moveGenerator.GetAllMoves(SideToMove());
            foreach (var move in moves)
            {
                Move(move);
                if (IsCheck(move.MovingColor))
                {
                    UndoMove();
                    continue;
                }

                nodes += Perft(depth - 1);
                UndoMove();
            }

            return nodes;
        }

        public UInt64 PerftCastling(int depth, IMove moveParam)
        {
            if (depth == 0)
            {
                return moveParam is CastlingMove ? (UInt64)1 : 0;
            }

            UInt64 nodes = 0;

            var moves = _moveGenerator.GetAllMoves(SideToMove());
            foreach (var move in moves)
            {
                Move(move);
                if (IsCheck(move.MovingColor))
                {
                    UndoMove();
                    continue;
                }

                nodes += PerftCastling(depth - 1, move);
                UndoMove();
            }

            return nodes;
        }

        public void Divide(int depth)
        {
            Console.WriteLine($"Divide depth {depth}");

            var moves = _moveGenerator.GetAllMoves(SideToMove());
            foreach (var move in moves)
            {
                Move(move);
                if (IsCheck(move.MovingColor))
                {
                    UndoMove();
                    continue;
                }

                var nodes = Perft(depth - 1);
                UndoMove();
                
                Console.WriteLine($"Move {move.ToUciString()} : {nodes}");
            }
        }

        public string GetPvMovesFromHashtable(ChessColor color)
        {
            var key = _hashtable.CurrentKey;
            var emptyMove = new NoLegalMove();
            var currentColor = color;
            HashEntry movePVHash;
            IMove currentPvMove = emptyMove;
            string pvMoves = "";
            var numberPly = 0;
            var encounteredPositions = new List<UInt64>();

            do
            {
                movePVHash = _hashtable.LookupPvMove(currentColor);
                if (movePVHash != null)
                {
                    currentPvMove = _moveFactory.MakeMove(movePVHash.From, movePVHash.To, movePVHash.PromotionPiece);
                    var allMoves = _moveGenerator.GetAllMoves(currentColor);

                    if (allMoves.Contains(currentPvMove))
                    {
                        _board.Move(currentPvMove);
                        var currentPosition = _hashtable.CurrentKey;

                        if (_moveGenerator.IsCheck(currentColor) ||
                            encounteredPositions.Contains(currentPosition)) // we have already been at this position so the moves are cyclic.
                        {
                            _board.Back();
                            break;
                        }

                        encounteredPositions.Add(currentPosition);
                        pvMoves += currentPvMove.ToUciString() + " ";
                        numberPly++;
                        currentColor = CommonHelper.OtherColor(currentColor);
                    }
                    else
                    {
                        break;
                    }
                }
            } while (movePVHash != null && currentPvMove != emptyMove);

            for (var i = 0; i < numberPly; i++)
            {
                _board.Back();
            }

            return pvMoves;
        }
    }
}
