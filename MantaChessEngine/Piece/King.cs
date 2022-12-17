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

        public override List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, int file, int rank, bool includeCastling = true)
        {
            var moves = base.GetMoves(moveGen, board, file, rank, includeCastling);

            if (!includeCastling)
            {
                return moves;
            }

            // Castling
            if (Color == ChessColor.White) // white king
            {
                // check for king side castling (0-0)
                Piece maybeWhiteKingRook = board.GetPiece(Helper.FileCharToFile('h'), 1);
                if (board.BoardState.LastCastlingRightWhiteKingSide && // castling right
                    file == Helper.FileCharToFile('e') && rank == 1 && // king initial position
                    maybeWhiteKingRook is Rook && maybeWhiteKingRook.Color == ChessColor.White && // rook init position
                    IsFieldsEmpty(board, Helper.FileCharToFile('f'), 1, Helper.FileCharToFile('g')) && // fields between king and rook empty
                    !moveGen.IsAttacked(board, ChessColor.White, Helper.FileCharToFile('e'), 1) && // king not attacked
                    !moveGen.IsAttacked(board, ChessColor.White, Helper.FileCharToFile('f'), 1) && // field next to king not attacked
                    !moveGen.IsAttacked(board, ChessColor.White, Helper.FileCharToFile('g'), 1)    // new king field not attacked
                    )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.WhiteKingSide, this));
                }

                // check for queen side castling (0-0-0)
                Piece maybeWhiteQueenRook = board.GetPiece(Helper.FileCharToFile('a'), 1);
                if (board.BoardState.LastCastlingRightWhiteQueenSide && // castling right
                    file == Helper.FileCharToFile('e') && rank == 1 && // king initial position
                    maybeWhiteQueenRook is Rook && maybeWhiteQueenRook.Color == ChessColor.White && // rook init position
                    IsFieldsEmpty(board, Helper.FileCharToFile('b'), 1, Helper.FileCharToFile('d')) &&// fields between king and rook empty
                    !moveGen.IsAttacked(board, ChessColor.White, Helper.FileCharToFile('e'), 1) && // king not attacked
                    !moveGen.IsAttacked(board, ChessColor.White, Helper.FileCharToFile('d'), 1) && // field next to king not attacked
                    !moveGen.IsAttacked(board, ChessColor.White, Helper.FileCharToFile('c'), 1)    // new king field not attacked
                    )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.WhiteQueenSide, this));
                }
            }
            
            if (Color == ChessColor.Black) // black king
            {
                // check for king side castling (0-0)
                Piece maybeBlackKingRook = board.GetPiece(Helper.FileCharToFile('h'), 8);
                if (board.BoardState.LastCastlingRightBlackKingSide && // castling right
                    file == Helper.FileCharToFile('e') && rank == 8 && // king initial position
                    maybeBlackKingRook is Rook && maybeBlackKingRook.Color == ChessColor.Black && // rook init position
                    IsFieldsEmpty(board, Helper.FileCharToFile('f'), 8, Helper.FileCharToFile('g')) && // fields between king and rook empty
                    !moveGen.IsAttacked(board, ChessColor.Black, Helper.FileCharToFile('e'), 8) && // king not attacked
                    !moveGen.IsAttacked(board, ChessColor.Black, Helper.FileCharToFile('f'), 8) && // field next to king not attacked
                    !moveGen.IsAttacked(board, ChessColor.Black, Helper.FileCharToFile('g'), 8)    // new king field not attacked
                )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.BlackKingSide, this));
                }

                // check for queen side castling (0-0-0)
                Piece maybeBlackQueenRook = board.GetPiece(Helper.FileCharToFile('a'), 8);
                if (board.BoardState.LastCastlingRightBlackQueenSide && // castling right
                    file == Helper.FileCharToFile('e') && rank == 8 && // king initial position
                    maybeBlackQueenRook is Rook && maybeBlackQueenRook.Color == ChessColor.Black && // rook init position
                    IsFieldsEmpty(board, Helper.FileCharToFile('b'), 8, Helper.FileCharToFile('d')) && // fields between king and rook empty
                    !moveGen.IsAttacked(board, ChessColor.Black, Helper.FileCharToFile('e'), 8) && // king not attacked
                    !moveGen.IsAttacked(board, ChessColor.Black, Helper.FileCharToFile('d'), 8) && // field next to king not attacked
                    !moveGen.IsAttacked(board, ChessColor.Black, Helper.FileCharToFile('c'), 8)    // new king field not attacked
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
