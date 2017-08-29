using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    class SearchServiceTree : ISearchService
    {
        private IEvaluator _evaluator;
        private MoveGenerator _moveGenerator;

        public SearchServiceTree(IEvaluator evaluator, MoveGenerator generator)
        {
            _evaluator = evaluator;
            _moveGenerator = generator;
        }

        public IMove Search(Board board, Definitions.ChessColor color, out float score)
        {

            // todo
            // 0. Tree mit test erstellen. braucht noch GetSibling und GetParent
            // 1. Create complete tree with all possible moves of required depth --> test
            // 2. take tree and evaluate each position. --> test
            // Auf diese Weise können beide schritte getestet werden
            // und später können opitimiertere Suchen implementiert werden mit dem gleichen bereits
            // erstellten kompletten Baum.

            MoveBase bestMove = null;
            float bestScore = InitBestScoreSofar(color);
            var moves = new Tree<MoveBase>(null);

            var possibleFirstMoves = _moveGenerator.GetAllMoves(board, color);
            for (int i=0; i<possibleFirstMoves.Count; i++)
            {
                
                moves.AddChild(possibleFirstMoves[i]);
                board.Move(possibleFirstMoves[i]);
                var secondColor = Helper.GetOpositeColor(color);
                var possibleSecondMoves = _moveGenerator.GetAllMoves(board, color);

                for (int j=0; j<possibleSecondMoves.Count; j++)
                {
                    var secondMoveNode = moves.GetChild(i);
                    board.Move(possibleSecondMoves[j]);
                    float scoreCurrentMove = _evaluator.Evaluate(board);
                    if (IsBestMoveSofar(color, bestScore, scoreCurrentMove))
                    {
                        bestMove = possibleSecondMoves[j];
                        bestScore = scoreCurrentMove;
                    }

                    board.Back();
                }

                board.Back();
            }

            score = bestScore;
            return bestMove;
        }

        private float InitBestScoreSofar(Definitions.ChessColor color)
        {
            if (color == Definitions.ChessColor.White)
            {
                return -10000;
            }
            else
            {
                return 10000;
            }
        }

        private bool IsBestMoveSofar(Definitions.ChessColor color, float bestScoreSoFar, float currentScore)
        {
            if (color == Definitions.ChessColor.White)
            {
                if (currentScore > bestScoreSoFar)
                {
                    return true;
                }
            }
            else
            {
                if (currentScore < bestScoreSoFar)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

