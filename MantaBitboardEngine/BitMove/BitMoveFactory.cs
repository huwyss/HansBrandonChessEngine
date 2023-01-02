using MantaCommon;

namespace MantaBitboardEngine
{
    public class BitMoveFactory :  IMoveFactory<BitMove>
    {
        private readonly Bitboards _board;

        public BitMoveFactory(Bitboards board)
        {
            _board = board;
        }

        public BitMove MakeMoveUci(string moveStringUci) // input is like "e2e4" or "a7a8q" (Promotion)
        {
            if (!CommonHelper.IsCorrectMoveUci(moveStringUci))
            {
                return BitMove.CreateEmptyMove();
            }

            var fromSquare = GetPosition(moveStringUci.Substring(0, 2));
            var toSquare = GetPosition(moveStringUci.Substring(2, 2));
            var promotionPiece = BitHelper.GetBitPieceType(moveStringUci.Length == 5 ? moveStringUci[4] : ' ');

            return MakeMove(fromSquare, toSquare, promotionPiece);
        }

        public BitMove MakeMove(Square fromSquare, Square toSquare, BitPieceType promotionPiece)
        {
            BitPiece capturedPiece;
            Square capturedSquare;

            var movingPiece = _board.GetPiece(fromSquare);

            // set captured Piece
            if (IsEnPassantCapture(movingPiece, fromSquare, toSquare))
            {
                capturedPiece = _board.GetPiece(fromSquare).Color == ChessColor.White
                    ? _board.GetPiece(toSquare - 8)
                    : _board.GetPiece(toSquare + 8);
                capturedSquare = _board.GetPiece(fromSquare).Color == ChessColor.White
                    ? toSquare - 8
                    : toSquare + 8;
            }
            else
            {
                capturedPiece = _board.GetPiece(toSquare);
                capturedSquare = capturedPiece.Piece != BitPieceType.Empty ? toSquare : Square.NoSquare;
            }

            if (IsWhiteKingSideCastling(movingPiece, fromSquare, toSquare) ||
                IsBlackKingSideCastling(movingPiece, fromSquare, toSquare))
            {
                return BitMove.CreateCastling(movingPiece.Color, CastlingType.KingSide, 0);
            }

            if (IsWhiteQueenSideCastling(movingPiece, fromSquare, toSquare) ||
                IsBlackQueenSideCastling(movingPiece, fromSquare, toSquare))
            {
                return BitMove.CreateCastling(movingPiece.Color, CastlingType.QueenSide, 0);
            }

            return BitMove.CreateCapture(movingPiece.Piece, fromSquare, toSquare, capturedPiece.Piece, capturedSquare, promotionPiece, movingPiece.Color, 0);
        }

        private bool IsEnPassantCapture(BitPiece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece.Piece == BitPieceType.Pawn &&
                   _board.GetPiece(toSquare).Color == ChessColor.Empty &&
                   _board.BoardState.LastEnPassantSquare == toSquare;
        }

        private Square GetPosition(string fieldString)
        {
            if (fieldString.Length != 2)
            {
                throw new MantaEngineException($"Illegal field string: {fieldString}");
            }

            return (Square)(FileCharToFile(fieldString[0]) + 8 * (int.Parse(fieldString[1].ToString()) - 1));
        }

        private bool IsWhiteKingSideCastling(BitPiece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece.Piece == BitPieceType.King && movingPiece.Color == ChessColor.White &&
                   fromSquare == Square.E1 && toSquare == Square.G1;
        }

        private bool IsWhiteQueenSideCastling(BitPiece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece.Piece == BitPieceType.King && movingPiece.Color == ChessColor.White &&
                   fromSquare == Square.E1 && toSquare == Square.C1;
        }

        private bool IsBlackKingSideCastling(BitPiece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece.Piece == BitPieceType.King && movingPiece.Color == ChessColor.Black &&
                   fromSquare == Square.E8 && toSquare == Square.G8;
        }

        private bool IsBlackQueenSideCastling(BitPiece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece.Piece == BitPieceType.King && movingPiece.Color == ChessColor.Black &&
                   fromSquare == Square.E8 && toSquare == Square.C8;
        }

        private static int FileCharToFile(char fileChar)
        {
            int file = fileChar - 'a';
            return file;
        }
    }
}
