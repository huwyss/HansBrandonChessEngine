using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class MoveGenerator
    {
        Board _board;

        public void SetBoard(Board board)
        {
            _board = board;
        }

        //public List<Move> GetAllMoves()
        //{
        //    List<Move> moves = new List<Move>();
        //    return moves;
        //}

        public List<Move> GetMoves(int file, int rank)
        {
            List<Move> moves = new List<Move>();
            char piece = _board.GetPiece(file, rank);
            int targetRank;
            int targetFile;
            bool valid;

            switch (piece.ToString().ToLower()[0])
            {
                case 'n':
                    var directionSequences = Helper.GetMoveDirectionSequence('n');
                    foreach (string sequence in directionSequences)
                    {
                        Helper.GetEndPosition(file, rank, sequence, out targetFile, out targetRank, out valid);
                        if (valid)
                        {
                            moves.Add(new Move(file, rank, targetFile, targetRank, _board.GetPiece(targetFile, targetRank)));
                        }
                    }
                    break;

            }

            return moves;
        }
    }
}
