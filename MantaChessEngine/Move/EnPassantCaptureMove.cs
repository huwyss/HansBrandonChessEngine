﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public class EnPassantCaptureMove : MoveBase
    {
        public EnPassantCaptureMove(Piece movingPiece, char sourceFile, int sourceRank, char targetFile, int targetRank, Piece capturedPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece)
        {
        }

        public EnPassantCaptureMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece)
        {
        }

        public override int CapturedRank // for en passant capture it is different from TargetRank
        {
            get
            {
                if (MovingColor == ChessColor.White)
                {
                    return TargetRank - 1;
                }

                return TargetRank + 1;
            }
        }

        public override void ExecuteMove(IBoard board)
        {
            board.SetPiece(null /*Definitions.EmptyField*/, CapturedFile, CapturedRank); // remove captured pawn if it is en passant
            base.ExecuteMove(board);
        }

        public override void UndoMove(IBoard board)
        {
            // note: CapturedRank is overridden and therefore taken from this class!
            base.UndoMove(board);
        }

        public override bool Equals(System.Object obj)
        {
            if (!(obj is EnPassantCaptureMove))
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override string ToString()
        {
            string moveString = base.ToString();
            moveString += "e";
            return moveString;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}