using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBCommon;

namespace HansBrandonChessEngine
{
    public abstract class MultiStepPiece : Piece
    {
        /// <summary>
        /// Multistep pieces are queen, rook and bishop
        /// </summary>
        public MultiStepPiece(ChessColor color) : base(color)
        {
        }

        public override List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, Square fromSquare, bool includeCastling = true)
        {
            List<IMove> moves = new List<IMove>();
            var directionSequences = GetMoveDirectionSequences();
            foreach (string sequence in directionSequences)
            {
                var currentSquare = fromSquare;
                for (int i = 1; i < 8; i++) // walk in the direction until off board or captured or next is own piece
                {
                    GetEndPosition(currentSquare, sequence, out Square toSquare, out bool valid);
                    if (!valid)
                    {
                        break;
                    }
                    ChessColor targetColor = board.GetColor(toSquare);
                    if (Color == targetColor)
                    {
                        break;
                    }

                    Piece targetPiece = board.GetPiece(toSquare);
                    moves.Add(MoveFactory.MakeNormalMove(this, fromSquare, toSquare, targetPiece));

                    if (ChessColor.Empty != targetColor)
                    {
                        break;
                    }

                    currentSquare = toSquare;
                }
            }

            return moves;
        }
    }
}
