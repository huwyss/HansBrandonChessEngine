using System.Collections.Generic;
using HBCommon;

namespace HansBrandonChessEngine
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

        public override PieceType PieceType => PieceType.Pawn;

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "u", "uu", "ul", "ur" }; // up, up up, up left, up right
        }

        public override List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, Square fromSquare, bool includeCastling = true)
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

                GetEndPosition(fromSquare, currentSequence, out Square toSquare, out bool valid);
                if (currentSequence == "u" || currentSequence == "d") // walk straight one field
                {
                    if (valid && board.GetColor(toSquare) == ChessColor.Empty) // empty field
                    {
                        if ((currentSequence == "u" && toSquare < Square.A8) ||  // white normal pawn move straight
                            (currentSequence == "d" && toSquare > Square.H1))    // black normal pawn move straight
                        {
                            moves.Add(MoveFactory.MakeNormalMove(this, fromSquare, toSquare, null));
                        }
                        else // pawn is promoted
                        {
                            moves.Add(MoveFactory.MakePromotionMove(this, fromSquare, toSquare, null, PieceType.Queen));
                            moves.Add(MoveFactory.MakePromotionMove(this, fromSquare, toSquare, null, PieceType.Rook));
                            moves.Add(MoveFactory.MakePromotionMove(this, fromSquare, toSquare, null, PieceType.Bishop));
                            moves.Add(MoveFactory.MakePromotionMove(this, fromSquare, toSquare, null, PieceType.Knight));
                        }
                    }
                }
                else if ((currentSequence == "uu" || currentSequence == "dd") && Helper.GetRank(fromSquare) == twoFieldMoveInitRank) // walk straight two fields
                {
                    string currentSequence2 = currentSequence == "uu" ? "u" : "d";
                    GetEndPosition(fromSquare, currentSequence2, out Square toSquare2,  out bool valid2);
                    if ((valid && board.GetColor(toSquare) == ChessColor.Empty) && // end field is empty
                        (valid2 && board.GetColor(toSquare2) == ChessColor.Empty)) // field between current and end field is also empty
                    {
                        moves.Add(MoveFactory.MakeNormalMove(this, fromSquare, toSquare, null));
                    }
                }
                else if (currentSequence == "ul" || currentSequence == "ur" ||
                         currentSequence == "dl" || currentSequence == "dr") // capture
                {
                    // normal capture
                    if (valid && Color == Helper.GetOppositeColor(board.GetColor(toSquare)))
                    {
                        if ((currentSequence == "ul" || currentSequence == "ur") && Helper.GetRank(toSquare) < 8 || // white normal pawn move capture
                            (currentSequence == "dl" || currentSequence == "dr") && Helper.GetRank(toSquare) > 1 )  // black normal pawn move capture
                        {
                            moves.Add(MoveFactory.MakeNormalMove(this, fromSquare, toSquare, board.GetPiece(toSquare)));
                        }
                        else // pawn is promoted
                        {
                            moves.Add(MoveFactory.MakePromotionMove(this, fromSquare, toSquare, board.GetPiece(toSquare), PieceType.Queen));
                            moves.Add(MoveFactory.MakePromotionMove(this, fromSquare, toSquare, board.GetPiece(toSquare), PieceType.Rook));
                            moves.Add(MoveFactory.MakePromotionMove(this, fromSquare, toSquare, board.GetPiece(toSquare), PieceType.Bishop));
                            moves.Add(MoveFactory.MakePromotionMove(this, fromSquare, toSquare, board.GetPiece(toSquare), PieceType.Knight));
                        }
                    }
                    // en passant capture
                    else if (valid && toSquare == board.BoardState.LastEnPassantSquare)
                    {
                        Piece capturedPawn = Color == ChessColor.White // moving pawn is white
                            ? board.GetPiece(toSquare - 8)  
                            : board.GetPiece(toSquare + 8); 
                        moves.Add(MoveFactory.MakeEnPassantCaptureMove(this, fromSquare, toSquare, capturedPawn));
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
