﻿using MantaCommon;

namespace MantaChessEngine
{
    public class MoveFactory : IMoveFactory<IMove>
    {
        public static NormalMove MakeNormalMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece)
        {
            return new NormalMove(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece);
        }

        public static EnPassantCaptureMove MakeEnPassantCaptureMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece)
        {
            return new EnPassantCaptureMove(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece);
        }

        public static PromotionMove MakePromotionMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece, char promotionPiece)
        {
            return new PromotionMove(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece, promotionPiece);
        }

        public static CastlingMove MakeCastlingMove(CastlingType castlingType, Piece king)
        {
            return new CastlingMove(castlingType, king);
        }

        public NoLegalMove MakeNoLegalMove()
        {
            return new NoLegalMove();
        }

        public IMove MakeMoveUci(IBoard board, string moveStringUci) // input is like "e2e4" or "a7a8q" (Promotion)
        {
            if (!CommonHelper.IsCorrectMoveUci(moveStringUci))
            {
                return null;
            }

            char promotionPiece = moveStringUci.Length == 5 ? moveStringUci[4] : (char)0;

            return MakeCorrectMoveInternal(board, moveStringUci, promotionPiece);
        }


        public IMove MakeMove(IBoard board, string moveStringUser) // input is like "e2e4"
        {
            if (!CommonHelper.IsCorrectMove(moveStringUser))
            {
                return null;
            }

            return MakeCorrectMoveInternal(board, moveStringUser, CommonDefinitions.QUEEN);
        }

        private IMove MakeCorrectMoveInternal(IBoard board, string moveString, char promotionPiece)
        {
            Piece movingPiece;
            Piece capturedPiece;
            bool enPassant = false;

            GetPositions(moveString, out int sourceFile, out int sourceRank, out int targetFile, out int targetRank);
            movingPiece = board.GetPiece(sourceFile, sourceRank);

            // set captured Piece
            if (IsEnPassantCapture(board, movingPiece, sourceFile, sourceRank, targetFile, targetRank))
            {
                capturedPiece = board.GetColor(sourceFile, sourceRank) == ChessColor.White
                    ? board.GetPiece(targetFile, targetRank - 1)
                    : board.GetPiece(targetFile, targetRank + 1);
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

            if (IsWhiteKingSideCastling(movingPiece, sourceFile, sourceRank, targetFile, targetRank))
            {
                return new CastlingMove(CastlingType.WhiteKingSide, movingPiece);
            }

            if (IsWhiteQueenSideCastling(movingPiece, sourceFile, sourceRank, targetFile, targetRank))
            {
                return new CastlingMove(CastlingType.WhiteQueenSide, movingPiece);
            }

            if (IsBlackKingSideCastling(movingPiece, sourceFile, sourceRank, targetFile, targetRank))
            {
                return new CastlingMove(CastlingType.BlackKingSide, movingPiece);
            }

            if (IsBlackQueenSideCastling(movingPiece, sourceFile, sourceRank, targetFile, targetRank))
            {
                return new CastlingMove(CastlingType.BlackQueenSide, movingPiece);
            }

            if (IsPromotion(movingPiece, sourceFile, sourceRank, targetFile, targetRank))
            {
                return new PromotionMove(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece, promotionPiece);
            }

            return new NormalMove(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece);
        }

        private bool IsEnPassantCapture(IBoard board, Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank)
        {
            return movingPiece is Pawn &&
                   board.GetColor(targetFile, targetRank) == ChessColor.Empty &&
                   board.BoardState.LastEnPassantFile == targetFile && 
                   board.BoardState.LastEnPassantRank == targetRank;
        }

        public void GetPositions(string moveString, out int sourceFile, out int sourceRank, out int targetFile, out int targetRank)
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

        private bool IsWhiteKingSideCastling(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank)
        {
            return movingPiece is King && movingPiece.Color == ChessColor.White &&
                    sourceFile == Helper.FileCharToFile('e') && sourceRank == 1 &&
                    targetFile == Helper.FileCharToFile('g') && targetRank == 1;
        }

        private bool IsWhiteQueenSideCastling(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank)
        {
            return movingPiece is King && movingPiece.Color == ChessColor.White &&
                   sourceFile == Helper.FileCharToFile('e') && sourceRank == 1 &&
                   targetFile == Helper.FileCharToFile('c') && targetRank == 1;
        }

        private bool IsBlackKingSideCastling(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank)
        {
            return movingPiece is King && movingPiece.Color == ChessColor.Black &&
                sourceFile == Helper.FileCharToFile('e') && sourceRank == 8 &&
                targetFile == Helper.FileCharToFile('g') && targetRank == 8;
        }

        private bool IsBlackQueenSideCastling(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank)
        {
            return movingPiece is King && movingPiece.Color == ChessColor.Black &&
                   sourceFile == Helper.FileCharToFile('e') && sourceRank == 8 &&
                   targetFile == Helper.FileCharToFile('c') && targetRank == 8;
        }

        private bool IsPromotion(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank)
        {
            return ((movingPiece is Pawn && movingPiece.Color == ChessColor.White && targetRank == 8) || // white promotion
                    (movingPiece is Pawn && movingPiece.Color == ChessColor.Black && targetRank == 1));  // black promotion
        }

        public IMove MakeMove(Square fromSquare, Square toSquare, BitPieceType promotionPiece)
        {
            throw new System.NotImplementedException(); 
        }

        public IMove MakeMoveUci(string moveStringUci)
        {
            throw new System.NotImplementedException(); // todo implement this. In 
        }
    }
}
