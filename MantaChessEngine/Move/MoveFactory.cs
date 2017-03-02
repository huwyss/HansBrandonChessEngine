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
            char movingPiece;
            char capturedPiece;
            int sourceFile = 0;
            int sourceRank = 0;
            int targetFile = 0;
            int targetRank = 0;
            bool enPassant = false;

            if (Move.IsCorrectMove(moveStringUser))
            {
                // Move move = new Move(moveStringUser);
                GetPositions(moveStringUser, out sourceFile, out sourceRank, out targetFile, out targetRank);
                
                movingPiece = board.GetPiece(sourceFile, sourceRank);

                if (board.GetColor(targetFile, targetRank) == Definitions.ChessColor.Empty &&
                    board.History.LastEnPassantFile == targetFile && board.History.LastEnPassantRank == targetRank)
                {
                    capturedPiece = board.GetColor(sourceFile, sourceRank) == Definitions.ChessColor.White
                        ? Definitions.PAWN.ToString().ToLower()[0]
                        : Definitions.PAWN.ToString().ToUpper()[0];
                    enPassant = true;
                }
                else
                {
                    capturedPiece = board.GetPiece(targetFile, targetRank);
                }

                if (enPassant)
                {
                    return new EnPassantCaptureMove(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece);
                }
                else
                {
                    return new Move(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece, false);
                }
            }

            return null;
        }

        public static void GetPositions(string moveString, out int sourceFile, out int sourceRank, out int targetFile, out int targetRank)
        {
            if (moveString.Length >= 4)
            {
                sourceFile = Helper.FileCharToFile(moveString[0]);
                sourceRank = int.Parse(moveString[1].ToString());
                targetFile = Helper.FileCharToFile(moveString[2]);
                targetRank = int.Parse(moveString[3].ToString());
            }
            else
            {
                sourceFile = 0;
                sourceRank = 0;
                targetFile = 0;
                targetRank = 0;
            }
        }
    }
}
