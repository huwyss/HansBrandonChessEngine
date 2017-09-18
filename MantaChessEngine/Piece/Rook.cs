using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Rook : Piece
    {
        public Rook(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'R' : 'r';
            }
        }

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "u", "r", "d", "l" }; // up, right, down, left
        }

        public override List<MoveBase> GetMoves(MoveGenerator moveGen, Board board, int file, int rank, bool includeCastling = true)
        {
            List<MoveBase> moves = new List<MoveBase>();
            int targetRank;
            int targetFile;
            bool valid;
            var directionSequences = GetMoveDirectionSequences();
            foreach (string sequence in directionSequences)
            {
                int currentFile = file;
                int currentRank = rank;
                for (int i = 1; i < 8; i++) // walk in the direction until off board or captured or next is own piece
                {
                    GetEndPosition(currentFile, currentRank, sequence, out targetFile, out targetRank, out valid);
                    if (!valid)
                    {
                        break;
                    }
                    Definitions.ChessColor targetColor = board.GetColor(targetFile, targetRank);
                    if (Color == targetColor)
                    {
                        break;
                    }

                    Piece targetPiece = board.GetPiece(targetFile, targetRank);
                    moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, targetPiece));

                    if (Definitions.ChessColor.Empty != targetColor)
                    {
                        break;
                    }

                    currentFile = targetFile;
                    currentRank = targetRank;
                }
            }

            return moves;
        }

        public override bool Equals(object obj)
        {
            if (obj is Rook)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
