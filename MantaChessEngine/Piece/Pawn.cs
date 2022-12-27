using System.Collections.Generic;
using MantaCommon;

namespace MantaChessEngine
{
    public class Pawn : Piece
    {
        public Pawn(ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == ChessColor.White ? 'P' : 'p';
            }
        }

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "u", "uu", "ul", "ur" }; // up, up up, up left, up right
        }

        public override List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, int file, int rank, bool includeCastling = true)
        {
            var moves = new List<IMove>();
            var directionSequences = GetMoveDirectionSequences();
            int twoFieldMoveInitRank = 2;
            foreach (string sequence in directionSequences)
            {
                string currentSequence = sequence;
                if (Color == ChessColor.Black)
                {
                    currentSequence = sequence.Replace('u', 'd');
                    twoFieldMoveInitRank = 7;
                }

                GetEndPosition(file, rank, currentSequence, out int targetFile, out int targetRank, out bool valid);
                if (currentSequence == "u" || currentSequence == "d") // walk straight one field
                {
                    if (valid && board.GetColor(targetFile, targetRank) == ChessColor.Empty) // empty field
                    {
                        if ((currentSequence == "u" && targetRank < 8) ||  // white normal pawn move straight
                            (currentSequence == "d" && targetRank > 1))    // black normal pawn move straight
                        {
                            moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, null));
                        }
                        else // pawn is promoted
                        {
                            moves.Add(MoveFactory.MakePromotionMove(this, file, rank, targetFile, targetRank, null, CommonDefinitions.QUEEN));
                            moves.Add(MoveFactory.MakePromotionMove(this, file, rank, targetFile, targetRank, null, CommonDefinitions.ROOK));
                            moves.Add(MoveFactory.MakePromotionMove(this, file, rank, targetFile, targetRank, null, CommonDefinitions.BISHOP));
                            moves.Add(MoveFactory.MakePromotionMove(this, file, rank, targetFile, targetRank, null, CommonDefinitions.KNIGHT));
                        }
                    }
                }
                else if ((currentSequence == "uu" || currentSequence == "dd") && rank == twoFieldMoveInitRank) // walk straight two fields
                {
                    string currentSequence2 = currentSequence == "uu" ? "u" : "d";
                    GetEndPosition(file, rank, currentSequence2, out int targetFile2, out int targetRank2, out bool valid2);
                    if ((valid && board.GetColor(targetFile, targetRank) == ChessColor.Empty) && // end field is empty
                        (valid2 && board.GetColor(targetFile2, targetRank2) == ChessColor.Empty)) // field between current and end field is also empty
                    {
                        moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, null));
                    }
                }
                else if (currentSequence == "ul" || currentSequence == "ur" ||
                         currentSequence == "dl" || currentSequence == "dr") // capture
                {
                    // normal capture
                    if (valid && Color == Helper.GetOppositeColor(board.GetColor(targetFile, targetRank)))
                    {
                        if ((currentSequence == "ul" || currentSequence == "ur") && targetRank < 8 || // white normal pawn move capture
                            (currentSequence == "dl" || currentSequence == "dr") && targetRank > 1 )  // black normal pawn move capture
                        {
                            moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                        }
                        else // pawn is promoted
                        {
                            moves.Add(MoveFactory.MakePromotionMove(this, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank), CommonDefinitions.QUEEN));
                            moves.Add(MoveFactory.MakePromotionMove(this, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank), CommonDefinitions.ROOK));
                            moves.Add(MoveFactory.MakePromotionMove(this, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank), CommonDefinitions.BISHOP));
                            moves.Add(MoveFactory.MakePromotionMove(this, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank), CommonDefinitions.KNIGHT));
                        }
                    }
                    // en passant capture
                    else if (valid && targetFile == board.BoardState.LastEnPassantFile && targetRank == board.BoardState.LastEnPassantRank)
                    {
                        Piece capturedPawn = Color == ChessColor.White // moving pawn is white
                            ? board.GetPiece(targetFile, targetRank - 1)  
                            : board.GetPiece(targetFile, targetRank + 1); 
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

        public override int GetPlainPieceValue()
        {
            return Definitions.ValuePawn;
        }
    }
}
