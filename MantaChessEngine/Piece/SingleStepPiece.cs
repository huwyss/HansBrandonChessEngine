using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    /// <summary>
    /// Single step piece are Knight and King
    /// </summary>
    public abstract class SingleStepPiece : Piece
    {
        public SingleStepPiece(ChessColor color) : base(color)
        {
        }

        public override List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, Square fromSquare, bool includeCastling = true)
        {
            Square toSquare;
            bool valid;
            List<IMove> moves = new List<IMove>();
            IEnumerable<string> directionSequences = GetMoveDirectionSequences();
            foreach (string sequence in directionSequences)
            {
                GetEndPosition(fromSquare, sequence, out toSquare, out valid);
                if (valid && Color != board.GetColor(toSquare)) // capture or empty field
                {
                    moves.Add(MoveFactory.MakeNormalMove(this, fromSquare, toSquare, board.GetPiece(toSquare)));
                }
            }

            return moves;
        }
    }
}
