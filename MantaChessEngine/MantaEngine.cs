using System;
using MantaCommon;

namespace MantaChessEngine
{
    public enum EngineType
    {
        Random,
        Minimax,
        MinimaxPosition,
        AlphaBeta
    }

    public class MantaEngine : IMantaEngine
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IMoveGenerator _moveGenerator;
        private MoveFactory _moveFactory;
        private ISearchService _search;
        private IEvaluator _evaluator;
        private IBoard _board;

        public MantaEngine(EngineType engineType)
        {
            _moveFactory = new MoveFactory();
            _moveGenerator = new MoveGenerator();

            switch (engineType)
            {
                case EngineType.Random:
                    _search = new SearchRandom(_moveGenerator);
                    break;

                case EngineType.Minimax:
                    _evaluator = new EvaluatorSimple();
                    _search = new SearchMinimax(_evaluator, _moveGenerator);
                    break;
                
                case EngineType.MinimaxPosition:
                    _evaluator = new EvaluatorPosition();
                    _search = new SearchMinimax(_evaluator, _moveGenerator);
                    break;
                
                // strongest --------------------------------
                case EngineType.AlphaBeta:
                    _evaluator = new EvaluatorPosition();
                    var moveOrder = new OrderPvAndImportance();
                    var captureOnly = new FilterCapturesOnly();
                    _search = new SearchAlphaBeta(_evaluator, _moveGenerator, 4, moveOrder, captureOnly);
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
            IMove move = _moveFactory.MakeMoveUci(_board, moveStringUci);
            if (move == null)
            {
                return false;
            }

            bool valid = _moveGenerator.IsMoveValid(_board, move);
            if (valid)
            {
                _board.Move(move);
            }

            return valid;
        }

        public bool Move(string moveStringUser)
        {
            IMove move = _moveFactory.MakeMove(_board, moveStringUser);
            if (move == null)
            {
                return false;
            }

            bool valid = _moveGenerator.IsMoveValid(_board, move);
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

        public UciMoveRating DoBestMove(ChessColor color)
        {
            MoveRating nextMove = _search.Search(_board, color);
            _board.Move(nextMove.Move);
            _log.Debug("Score: " + nextMove.Score);

            UciMoveRating uciRating = new UciMoveRating();
            // todo implement conversion.

            return uciRating;
        }

        public UciMoveRating DoBestMove()
        {
            MoveRating nextMove = _search.Search(_board, _board.BoardState.SideToMove);
            _board.Move(nextMove.Move);
            _log.Debug("Score: " + nextMove.Score);

            UciMoveRating uciRating = new UciMoveRating();
            // todo implement conversion.

            return uciRating;
        }

        public ChessColor SideToMove()
        {
            return _board.BoardState.SideToMove;
        }

        public bool IsCheck(ChessColor color)
        {
            return _moveGenerator.IsCheck(_board, color);
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

            var moves = _moveGenerator.GetLegalMoves(_board, SideToMove());
            foreach (var move in moves)
            {
                Move(move);
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

            var moves = _moveGenerator.GetLegalMoves(_board, SideToMove());
            foreach (var move in moves)
            {
                Move(move);
                nodes += PerftCastling(depth - 1, move);
                UndoMove();
            }

            return nodes;
        }

        public void Divide(int depth)
        {
            Console.WriteLine($"Divide depth {depth}");

            var moves = _moveGenerator.GetLegalMoves(_board, SideToMove());
            foreach (var move in moves)
            {
                Move(move);
                var nodes = Perft(depth - 1);
                UndoMove();
                
                Console.WriteLine($"Move {move.ToUciString()} : {nodes}");
            }
        }
    }
}
