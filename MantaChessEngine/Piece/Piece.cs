using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Piece
    {
        public Definitions.ChessColor Color { get; set; }
        public Piece(Definitions.ChessColor color)
        {
            Color = color;
        }

        public virtual char Symbol { get; }

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
    }
}
