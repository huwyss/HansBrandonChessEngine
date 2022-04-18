using System;
using static MantaChessEngine.Definitions;

namespace MantaChessEngine
{
    public enum EngineType
    {
        Random,
        Minimax,
        MinimaxPosition,
        AlphaBeta
    }

    public class MantaEngine
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IMoveGenerator _moveGenerator;
        private MoveFactory _moveFactory;
        private ISearchService _search;
        private IEvaluator _evaluator;
        private IBoard _board;
        private IMoveOrder _moveOrder;

        public MantaEngine(EngineType engineType)
        {
            _moveFactory = new MoveFactory();
            _moveGenerator = new MoveGenerator(_moveFactory);

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
                    _moveOrder = new MoveOrderPV();
                    _search = new SearchAlphaBeta(_evaluator, _moveGenerator, 4, _moveOrder);
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

        public bool Move(IMove move)
        {
            _board.Move(move);
            return true;
        }

        public bool UndoMove()
        {
            _board.Back();
            return true;
        }

        public bool IsWinner(ChessColor color)
        {
            return _board.IsWinner(color);
        }

        public MoveRating DoBestMove(ChessColor color)
        {
            MoveRating nextMove = _search.Search(_board, color);
            _board.Move(nextMove.Move);
            _log.Debug("Score: " + nextMove.Score);

            return nextMove;
        }

        public MoveRating DoBestMove()
        {
            MoveRating nextMove = _search.Search(_board, _board.SideToMove);
            _board.Move(nextMove.Move);
            _log.Debug("Score: " + nextMove.Score);

            return nextMove;
        }

        public ChessColor SideToMove()
        {
            return _board.SideToMove;
        }

        public bool IsCheck(Definitions.ChessColor color)
        {
            return _moveGenerator.IsCheck(_board, color);
        }

        public void Back()
        {
            _board.Back();
            _board.Back();
        }

        public void SetMaxSearchDepth(int depth)
        {
            if (_search != null)
            {
                _search.SetMaxDepth(depth);
            }
        }

        public UInt64 Perft(int depth, IMove moveParam)
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
                nodes += Perft(depth - 1, move);
                UndoMove();
            }

            return nodes;
        }
    }
}
