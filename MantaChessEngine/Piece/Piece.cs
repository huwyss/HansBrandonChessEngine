using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public abstract class Piece
    {
        public ChessColor Color { get; set; }
        public Piece(ChessColor color)
        {
            Color = color;
        }

        public abstract PieceType PieceType { get; }

        public abstract char Symbol { get; }

        public char UniversalSymbol => Symbol.ToString().ToUpper()[0];

        public abstract IEnumerable<string> GetMoveDirectionSequences();

        public virtual List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, Square fromSquare, bool includeCastling = true)
        { 
            return null;
        }

        public static Piece MakePiece(PieceType pieceType, ChessColor color)
        {
            switch (pieceType)
            {
                case PieceType.King:
                    return new King(color);

                case PieceType.Queen:
                    return new Queen(color);

                case PieceType.Rook:
                    return new Rook(color);

                case PieceType.Bishop:
                    return new Bishop(color);

                case PieceType.Knight:
                    return new Knight(color);

                case PieceType.Pawn:
                    return new Pawn(color);

                default:
                    return null;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj as Piece).Color == Color;
        }

        // unit tests need access.
        // valid means move is within board. 
        internal void GetEndPosition(Square fromSquare, string sequence, out Square toSquare, out bool valid)
        {
            toSquare = fromSquare;
            valid = false;

            for (int i = 0; i < sequence.Length; i++)
            {
                var currentFile = Helper.GetFile(toSquare);
                var currentRank = Helper.GetRank(toSquare);

                char direction = sequence[i];
                switch (direction)
                {
                    case Definitions.UP:
                        valid = currentRank < 8;
                        toSquare += 8;
                        break;
                    case Definitions.RIGHT:
                        valid = currentFile < 8;
                        toSquare++;
                        break;
                    case Definitions.DOWN:
                        valid = currentRank > 1;
                        toSquare -=8;
                        break;
                    case Definitions.LEFT:
                        valid = currentFile > 1;
                        toSquare--;
                        break;
                    default:
                        break;
                }

                if (!valid)
                {
                    return;
                }
            }
        }

        internal bool IsFieldsEmpty(IBoard board, Square fromSquare, Square toSquare)
        {
            bool empty = true;

            for (var square = fromSquare; square <= toSquare; square++)
            {
                empty &= board.GetPiece(square) == null;
            }

            return empty;
        }

        public abstract int GetPlainPieceValue();
    }
}
