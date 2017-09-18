using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public abstract class Piece
    {
        public Definitions.ChessColor Color { get; set; }
        public Piece(Definitions.ChessColor color)
        {
            Color = color;
        }

        public abstract char Symbol { get; }

        public abstract IEnumerable<string> GetMoveDirectionSequences();
        public virtual List<MoveBase> GetMoves(MoveGenerator moveGen, Board board, int file, int rank, bool includeCastling = true)
        { return null; }
            
        public static Piece MakePiece(char pieceChar)
        {
            switch (pieceChar)
            {
                case 'K':
                    return new King(Definitions.ChessColor.White);
                case 'k':
                    return new King(Definitions.ChessColor.Black);
                case 'Q':
                    return new Queen(Definitions.ChessColor.White);
                case 'q':
                    return new Queen(Definitions.ChessColor.Black);
                case 'R':
                    return new Rook(Definitions.ChessColor.White);
                case 'r':
                    return new Rook(Definitions.ChessColor.Black);
                case 'B':
                    return new Bishop(Definitions.ChessColor.White);
                case 'b':
                    return new Bishop(Definitions.ChessColor.Black);
                case 'N':
                    return new Knight(Definitions.ChessColor.White);
                case 'n':
                    return new Knight(Definitions.ChessColor.Black);
                case 'P':
                    return new Pawn(Definitions.ChessColor.White);
                case 'p':
                    return new Pawn(Definitions.ChessColor.Black);

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

        internal bool IsFieldsEmpty(Board board, int sourceFile, int sourceRank, int targetFile)
        {
            bool empty = true;

            for (int file = sourceFile; file <= targetFile; file++)
            {
                empty &= board.GetPiece(file, sourceRank) == null; //Definitions.EmptyField;
            }

            return empty;
        }
    }
}
