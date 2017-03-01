using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class MoveFactory
    {
        public static Move GetCorrectMove(Board board, string moveStringUser) // input is like "e2e4"
        {
            if (Move.IsCorrectMove(moveStringUser))
            {
                Move move = new Move(moveStringUser);
                move.MovingPiece = board.GetPiece(move.SourceFile, move.SourceRank);

                if (board.GetColor(move.TargetFile, move.TargetRank) == Definitions.ChessColor.Empty &&
                    board.History.LastEnPassantFile == move.TargetFile && board.History.LastEnPassantRank == move.TargetRank)
                {
                    move.CapturedPiece = board.GetColor(move.SourceFile, move.SourceRank) == Definitions.ChessColor.White
                        ? Definitions.PAWN.ToString().ToLower()[0]
                        : Definitions.PAWN.ToString().ToUpper()[0];
                    move.EnPassant = true;
                }
                else
                {
                    move.CapturedPiece = board.GetPiece(move.TargetFile, move.TargetRank);
                }

                return move;
            }

            return null;
        }
    }
}
