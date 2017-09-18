using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Knight : Piece
    {
        public Knight(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'N' : 'n';
            }
        }

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "uul", "uur", "rru", "rrd", "ddr", "ddl", "lld", "llu" }; // up up left, up up right, ...
        }

        public override IEnumerable<MoveBase> GetMoves(Board board, int file, int rank, bool includeCastling = true)
        {
            int targetRank;
            int targetFile;
            bool valid;
            List<MoveBase> moves = new List<MoveBase>();
            IEnumerable<string> directionSequences = GetMoveDirectionSequences();
            foreach (string sequence in directionSequences)
            {
                GetEndPosition(file, rank, sequence, out targetFile, out targetRank, out valid);
                if (valid && Color != board.GetColor(targetFile, targetRank)) // capture or empty field
                {
                    moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                }
            }

            return moves;
        }

        public override bool Equals(object obj)
        {
            if (obj is Knight)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
