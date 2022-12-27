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

        public abstract char Symbol { get; }

        public char UniversalSymbol => Symbol.ToString().ToUpper()[0];

        public abstract IEnumerable<string> GetMoveDirectionSequences();

        public virtual List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, int file, int rank, bool includeCastling = true)
        { 
            return null;
        }
       
        public static Piece MakePiece(char pieceChar)
        {
            return MakePiece(pieceChar, Helper.GetPieceColor(pieceChar));
        }

        public static Piece MakePiece(char pieceChar, ChessColor color)
        {
            switch (pieceChar.ToString().ToLower()[0])
            {
                case CommonDefinitions.KING:
                    return new King(color);
                case CommonDefinitions.QUEEN:
                    return new Queen(color);
                case CommonDefinitions.ROOK:
                    return new Rook(color);
                case CommonDefinitions.BISHOP:
                    return new Bishop(color);
                case CommonDefinitions.KNIGHT:
                    return new Knight(color);
                case CommonDefinitions.PAWN:
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
        internal void GetEndPosition(int file, int rank, string sequence, out int targetFile, out int targetRank, out bool valid)
        {
            targetFile = file;
            targetRank = rank;

            for (int i = 0; i < sequence.Length; i++)
            {
                char direction = sequence[i];
                switch (direction)
                {
                    case Definitions.UP:
                        targetRank++;
                        break;
                    case Definitions.RIGHT:
                        targetFile++;
                        break;
                    case Definitions.DOWN:
                        targetRank--;
                        break;
                    case Definitions.LEFT:
                        targetFile--;
                        break;
                    default:
                        break;
                }
            }

            valid = targetFile >= 1 && targetFile <= 8 &&
                    targetRank >= 1 && targetRank <= 8;
        }

        internal bool IsFieldsEmpty(IBoard board, int sourceFile, int sourceRank, int targetFile)
        {
            bool empty = true;

            for (int file = sourceFile; file <= targetFile; file++)
            {
                empty &= board.GetPiece(file, sourceRank) == null;
            }

            return empty;
        }

        public abstract int GetPlainPieceValue();
    }
}
