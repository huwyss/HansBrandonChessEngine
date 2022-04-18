using System;
using static MantaChessEngine.Definitions;

namespace MantaChessEngine
{
    public class FenParser
    {
        public PositionInfo ToPositionInfo(string fen)
        {
            var fenParts = fen.Trim().Split(new char[] { ' ' });

            return new PositionInfo()
            {
                PositionString = GetPositionString(fenParts[0]),

                SideToMove = fenParts[1] == "w" ? ChessColor.White : ChessColor.Black,

                CastlingRightWhiteKingSide = fenParts[2].Contains("K"),
                CastlingRightWhiteQueenSide = fenParts[2].Contains("Q"),
                CastlingRightBlackKingSide = fenParts[2].Contains("k"),
                CastlingRightBlackQueenSide = fenParts[2].Contains("q"),

                EnPassantFile = GetEnPassantFile(fenParts[3]),
                EnPassantRank = GetEnPassantRank(fenParts[3]),

                MoveCountSincePawnOrCapture = int.Parse(fenParts[4]),

                MoveNumber = int.Parse(fenParts[5])
            };
        }

        public string ToFen(PositionInfo posInfo)
        {
            return "";
        }

        private string GetPositionString(string fenPosition)
        {
            var positionChars = new char[64];
            var index = 0;
            foreach(var posChar in fenPosition)
            {
                if (posChar >= '1' && posChar <= '9')
                {
                    var numberEmpty = int.Parse(posChar.ToString());
                    for (int i = 0; i < numberEmpty; i++)
                    {
                        positionChars[index++] = Definitions.EmptyField;
                    }
                }
                else if (posChar == '/')
                {
                    continue;
                }
                else
                {
                    positionChars[index++] = posChar;
                }
            }

            return new string(positionChars);
        }

        private char GetEnPassantFile(string enPassantField)
        {
            if (enPassantField.Length >= 2)
            {
                return enPassantField[0] >= 'a' && enPassantField[0] <= 'h' ? enPassantField[0] : '\0';
            }

            return '\0';
        }

        private int GetEnPassantRank(string enPassantField)
        {
            if (enPassantField.Length >= 2)
            {
                return enPassantField[1] == '3' || enPassantField[1] == '6' ? (int)enPassantField[1] - (int)'0' : 0;
            }

            return 0;
        }
    }

    public class PositionInfo
    {
        public string PositionString { get; set; }
        public ChessColor SideToMove { get; set; }

        public bool CastlingRightWhiteKingSide { get; set; }
        public bool CastlingRightWhiteQueenSide { get; set; }
        public bool CastlingRightBlackKingSide { get; set; }
        public bool CastlingRightBlackQueenSide { get; set; }

        public char EnPassantFile { get; set; }
        public int EnPassantRank { get; set; }

        public int MoveCountSincePawnOrCapture { get; set; }

        public int MoveNumber { get; set; }
    }
}
