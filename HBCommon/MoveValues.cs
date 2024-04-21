using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCommon
{
    public class MoveValues : IMoveValues
    {
        public int PromotionValue { get; } = 200;
        public int CapturePromotionValue { get; } = 220;
        public int PawnMoveValue { get; } = 10;
        public int GeneralMoveValue { get; } = 0;
        public byte[,] CaptureValues { get; } = new byte[6, 6]; // dimensions: capturing piece, captured piece

        public MoveValues()
        {
            InitializeCaptureValues();
        }

        private void InitializeCaptureValues()
        {
            for (int i = (int)PieceType.Pawn; i <= (int)PieceType.King; i++)
            {
                CaptureValues[(int)PieceType.Pawn, i] = PawnCaptureValues[i];
                CaptureValues[(int)PieceType.Knight, i] = KnightCaptureValues[i];
                CaptureValues[(int)PieceType.Bishop, i] = BishopCaptureValues[i];
                CaptureValues[(int)PieceType.Rook, i] = RookCaptureValues[i];
                CaptureValues[(int)PieceType.Queen, i] = QueenCaptureValues[i];
                CaptureValues[(int)PieceType.King, i] = KingCaptureValues[i];
            }
        }

        // value := 100 + (value captured piece - value capturing piece)
        private readonly byte[] PawnCaptureValues = new byte[6] { 100, 120, 120, 140, 180, 0 };
        private readonly byte[] KnightCaptureValues = new byte[6] { 80, 100, 100, 120, 160, 0 };
        private readonly byte[] BishopCaptureValues = new byte[6] { 80, 100, 100, 120, 160, 0 };
        private readonly byte[] RookCaptureValues = new byte[6] { 60, 80, 80, 100, 140, 0 };
        private readonly byte[] QueenCaptureValues = new byte[6] { 20, 40, 40, 60, 100, 0 };
        private readonly byte[] KingCaptureValues = new byte[6] { 90, 90, 90, 90, 90, 0 };
    }

    public interface IMoveValues
    {
        int PromotionValue { get; }
        int CapturePromotionValue { get; }
        int PawnMoveValue { get; }
        int GeneralMoveValue { get; }
        byte[,] CaptureValues { get; }
    }
}
