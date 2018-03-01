using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class King : SingleStepPiece
    {
        public King(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'K' : 'k';
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
            if (Color == Definitions.ChessColor.White) // white king
            {
                // check for king side castling (0-0)
                Piece maybeWhiteKingRook = board.GetPiece(Helper.FileCharToFile('h'), 1);
                if (board.CastlingRightWhiteKingSide && // castling right
                    file == Helper.FileCharToFile('e') && rank == 1 && // king initial position
                    maybeWhiteKingRook is Rook && maybeWhiteKingRook.Color == Definitions.ChessColor.White && // rook init position
                    IsFieldsEmpty(board, Helper.FileCharToFile('f'), 1, Helper.FileCharToFile('g')) && // fields between king and rook empty
                    !moveGen.IsAttacked(board, Color, Helper.FileCharToFile('e'), 1) && // king not attacked
                    !moveGen.IsAttacked(board, Color, Helper.FileCharToFile('f'), 1)
                    )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.WhiteKingSide, this));
                }

                // check for queen side castling (0-0-0)
                Piece maybeWhiteQueenRook = board.GetPiece(Helper.FileCharToFile('a'), 1);
                if (board.CastlingRightWhiteQueenSide && // castling right
                    file == Helper.FileCharToFile('e') && rank == 1 && // king initial position
                    maybeWhiteQueenRook is Rook && maybeWhiteQueenRook.Color == Definitions.ChessColor.White && // rook init position
                    IsFieldsEmpty(board, Helper.FileCharToFile('b'), 1, Helper.FileCharToFile('d')) &&// fields between king and rook empty
                    !moveGen.IsAttacked(board, Color, Helper.FileCharToFile('e'), 1) && // king not attacked
                    !moveGen.IsAttacked(board, Color, Helper.FileCharToFile('d'), 1)
                    )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.WhiteQueenSide, this));
                }
            }
            
            if (Color == Definitions.ChessColor.Black) // black king
            {
                // check for king side castling (0-0)
                Piece maybeBlackKingRook = board.GetPiece(Helper.FileCharToFile('h'), 8);
                if (board.CastlingRightBlackKingSide && // castling right
                    file == Helper.FileCharToFile('e') && rank == 8 && // king initial position
                    maybeBlackKingRook is Rook && maybeBlackKingRook.Color == Definitions.ChessColor.Black && // rook init position
                    IsFieldsEmpty(board, Helper.FileCharToFile('f'), 8, Helper.FileCharToFile('g')) && // fields between king and rook empty
                    !moveGen.IsAttacked(board, Color, Helper.FileCharToFile('e'), 8) && // king not attacked
                    !moveGen.IsAttacked(board, Color, Helper.FileCharToFile('f'), 8)    // field next to king not attacked
                )
                {
                    moves.Add(MoveFactory.MakeCastlingMove(CastlingType.BlackKingSide, this));
                }

                // check for queen side castling (0-0-0)
                Piece maybeBlackQueenRook = board.GetPiece(Helper.FileCharToFile('a'), 8);
                if (board.CastlingRightBlackQueenSide && // castling right
                    file == Helper.FileCharToFile('e') && rank == 8 && // king initial position
                    maybeBlackQueenRook is Rook && maybeBlackQueenRook.Color == Definitions.ChessColor.Black && // rook init position
                    IsFieldsEmpty(board, Helper.FileCharToFile('b'), 8, Helper.FileCharToFile('d')) && // fields between king and rook empty
                    !moveGen.IsAttacked(board, Color, Helper.FileCharToFile('e'), 8) && // king not attacked
                    !moveGen.IsAttacked(board, Color, Helper.FileCharToFile('d'), 8)    // field next to king not attacked
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
    }
}
