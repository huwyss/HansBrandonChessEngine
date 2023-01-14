using System.Collections.Generic;
using MantaCommon;

namespace MantaChessEngine
{
    public class King : SingleStepPiece
    {
        public King(ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == ChessColor.White ? 'K' : 'k';
            }
        }

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "u", "ur", "r", "rd", "d", "dl", "l", "lu" }; // up, up right, right, right down, ...
        }

        public override List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, Square fromSquare, bool includeCastling = true)
        {
            var moves = base.GetMoves(moveGen, board, fromSquare, includeCastling);

            if (!includeCastling)
            {
                return moves;
            }

            // Castling
            if (Color == ChessColor.White) // white king
            {
                // check for king side castling (0-0)
                Piece maybeWhiteKingRook = board.GetPiece(Square.H1);
                if (board.BoardState.LastCastlingRightWhiteKingSide && // castling right
                    fromSquare == Square.E1 && // king initial position
                    maybeWhiteKingRook is Rook && maybeWhiteKingRook.Color == ChessColor.White && // rook init position
                    IsFieldsEmpty(board, Square.F1, Square.G1) && // fields between king and rook empty
                    !moveGen.IsAttacked(ChessColor.White, Square.E1) && // king not attacked
                    !moveGen.IsAttacked(ChessColor.White, Square.F1) && // field next to king not attacked
                    !moveGen.IsAttacked(ChessColor.White, Square.G1)    // new king field not attacked
                    )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.WhiteKingSide, this));
                }

                // check for queen side castling (0-0-0)
                Piece maybeWhiteQueenRook = board.GetPiece(Square.A1);
                if (board.BoardState.LastCastlingRightWhiteQueenSide && // castling right
                    fromSquare == Square.E1 && // king initial position
                    maybeWhiteQueenRook is Rook && maybeWhiteQueenRook.Color == ChessColor.White && // rook init position
                    IsFieldsEmpty(board, Square.B1, Square.D1) &&// fields between king and rook empty
                    !moveGen.IsAttacked(ChessColor.White, Square.E1) && // king not attacked
                    !moveGen.IsAttacked(ChessColor.White, Square.D1) && // field next to king not attacked
                    !moveGen.IsAttacked(ChessColor.White, Square.C1)    // new king field not attacked
                    )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.WhiteQueenSide, this));
                }
            }
            
            if (Color == ChessColor.Black) // black king
            {
                // check for king side castling (0-0)
                Piece maybeBlackKingRook = board.GetPiece(Square.H8);
                if (board.BoardState.LastCastlingRightBlackKingSide && // castling right
                    fromSquare == Square.E8 && // king initial position
                    maybeBlackKingRook is Rook && maybeBlackKingRook.Color == ChessColor.Black && // rook init position
                    IsFieldsEmpty(board, Square.F8, Square.G8) && // fields between king and rook empty
                    !moveGen.IsAttacked(ChessColor.Black, Square.E8) && // king not attacked
                    !moveGen.IsAttacked(ChessColor.Black, Square.F8) && // field next to king not attacked
                    !moveGen.IsAttacked(ChessColor.Black, Square.G8)    // new king field not attacked
                )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.BlackKingSide, this));
                }

                // check for queen side castling (0-0-0)
                Piece maybeBlackQueenRook = board.GetPiece(Square.A8);
                if (board.BoardState.LastCastlingRightBlackQueenSide && // castling right
                    fromSquare == Square.E8 && // king initial position
                    maybeBlackQueenRook is Rook && maybeBlackQueenRook.Color == ChessColor.Black && // rook init position
                    IsFieldsEmpty(board, Square.B8, Square.D8) && // fields between king and rook empty
                    !moveGen.IsAttacked(ChessColor.Black, Square.E8) && // king not attacked
                    !moveGen.IsAttacked(ChessColor.Black, Square.D8) && // field next to king not attacked
                    !moveGen.IsAttacked(ChessColor.Black, Square.C8)    // new king field not attacked
                )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.BlackQueenSide, this));
                }
            }

            return moves;
        }

        public override bool Equals(object obj)
        {
            if (obj is King)
            {
                return base.Equals(obj);
            }

            return false;
        }

        public override int GetPlainPieceValue()
        {
            return 100;
        }
    }
}
