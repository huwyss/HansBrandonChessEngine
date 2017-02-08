using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public enum EngineType
    {
        Random,
        DepthHalf,
        DepthOne
    }

    public class BaracudaEngine
    {
        private Board _board;
        private MoveGenerator _moveGenerator;
        private ISearchService _search;
        private IEvaluator _evaluator;
        
        public BaracudaEngine(EngineType engineType)
        {
            if (engineType == EngineType.Random)
            {
                _moveGenerator = new MoveGenerator();
                _board = new Board(_moveGenerator);
                _search = new SearchRandom();
            }
            else if (engineType == EngineType.DepthHalf)
            {
                _moveGenerator = new MoveGenerator();
                _board = new Board(_moveGenerator);
                _evaluator = new EvaluatorSimple();
                _search = new SearchServiceDepthHalfMove(_evaluator);
            }
            else if (engineType == EngineType.DepthOne)
            {
                _moveGenerator = new MoveGenerator();
                _board = new Board(_moveGenerator);
                _evaluator = new EvaluatorSimple();
                _search = new SearchServiceDepthOne(_evaluator);
            }
        }

        public void SetInitialPosition()
        {
            _board.SetInitialPosition();
        }

        public void SetPosition(string position)
        {
            _board.SetPosition(position);
        }

        public string GetString()
        {
            return _board.GetString;
        }

        public string GetPrintString()
        {
            return _board.GetPrintString;
        }

        public bool Move(string moveStringUser)
        {
            Move syntaxCorrectMove = _board.GetValidMove(moveStringUser);
            bool valid = _board.IsMoveValid(syntaxCorrectMove);
            if (valid)
            {
                _board.Move(syntaxCorrectMove);
            }

            return valid;
        }

        public bool Move(Move move)
        {
            _board.Move(move);
            return true;
        }

        public bool IsWinner(Definitions.ChessColor color)
        {
            return _board.IsWinner(color);
        }

        public Move DoBestMove(Definitions.ChessColor color)
        {
            Move nextMove = _search.Search(_board, color);
            _board.Move(nextMove);
            return nextMove;

            var possibleMovesComputer = GetAllMoves(color);
            int numberPossibleMoves = possibleMovesComputer.Count;
            
            if (numberPossibleMoves > 0)
            {
                //int randomMoveIndex = _rand.Next(0, numberPossibleMoves - 1);
                //nextMove = possibleMovesComputer[randomMoveIndex];
                _board.Move(nextMove);
            }

            return nextMove;
        }

        private List<Move> GetAllMoves(Definitions.ChessColor color)
        {
            return _board.GetAllMoves(color);
        }

        public Definitions.ChessColor SideToMove()
        {
            return _board.SideToMove;
        }
    }
}
