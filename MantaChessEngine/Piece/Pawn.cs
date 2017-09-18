using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Pawn : Piece
    {
        public Pawn(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'P' : 'p';
            }
        }

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "u", "uu", "ul", "ur" }; // up, up up, up left, up right
        }

        public override List<MoveBase> GetMoves(MoveGenerator moveGen, Board board, int file, int rank, bool includeCastling = true)
        {
            var moves = new List<MoveBase>();
            int targetRank;
            int targetFile;
            bool valid;
            var directionSequences = GetMoveDirectionSequences();
            int twoFieldMoveInitRank = 2;
            foreach (string sequence in directionSequences)
            {
                string currentSequence = sequence;
                if (Color == Definitions.ChessColor.Black)
                {
                    currentSequence = sequence.Replace('u', 'd');
                    twoFieldMoveInitRank = 7;
                }

                GetEndPosition(file, rank, currentSequence, out targetFile, out targetRank, out valid);
                if (currentSequence == "u" || currentSequence == "d") // walk straight one field
                {
                    if (valid && board.GetColor(targetFile, targetRank) == Definitions.ChessColor.Empty) // empty field
                    {
                        moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, null));
                    }
                }
                else if ((currentSequence == "uu" || currentSequence == "dd") && rank == twoFieldMoveInitRank) // walk straight two fields
                {
                    int targetFile2 = 0;
                    int targetRank2 = 0;
                    bool valid2 = false;
                    string currentSequence2 = currentSequence == "uu" ? "u" : "d";
                    GetEndPosition(file, rank, currentSequence2, out targetFile2, out targetRank2, out valid2);
                    if ((valid && board.GetColor(targetFile, targetRank) == Definitions.ChessColor.Empty) && // end field is empty
                        (valid2 && board.GetColor(targetFile2, targetRank2) == Definitions.ChessColor.Empty)) // field between current and end field is also empty
                    {
                        moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, null));
                    }
                }
                else if (currentSequence == "ul" || currentSequence == "ur" ||
                         currentSequence == "dl" || currentSequence == "dr") // capture
                {
                    if (valid && Color == Helper.GetOpositeColor(board.GetColor(targetFile, targetRank)))
                    {
                        moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                    }
                    else if (valid && targetFile == board.History.LastEnPassantFile && targetRank == board.History.LastEnPassantRank)
                    {
                        Piece capturedPawn = Color == Definitions.ChessColor.White
                            ? Piece.MakePiece(Definitions.PAWN.ToString().ToLower()[0])
                            : Piece.MakePiece(Definitions.PAWN.ToString().ToUpper()[0]);
                        moves.Add(MoveFactory.MakeEnPassantCaptureMove(this, file, rank, targetFile, targetRank, capturedPawn));
                    }

                }
            }

            return moves;
        }

        public override bool Equals(object obj)
        {
            if (obj is Pawn)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
