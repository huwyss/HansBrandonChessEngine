using MantaCommon;

namespace MantaBitboardEngine
{
    public class BitEvaluatorSimple : IEvaluator
    {
        private readonly Bitboards _board;
        private readonly HelperBitboards _helperBits;

        private readonly int[] _value;

        public BitEvaluatorSimple(Bitboards board, HelperBitboards helperBits)
        {
            _board = board;
            _helperBits = helperBits;
            _value = new int[(int)BitPieceType.King + 1];
            _value[(int)BitPieceType.Pawn] = 100;
            _value[(int)BitPieceType.Knight] = 300;
            _value[(int)BitPieceType.Bishop] = 300;
            _value[(int)BitPieceType.Rook] = 500;
            _value[(int)BitPieceType.Queen] = 900;
            _value[(int)BitPieceType.King] = 0; // king has only position bonus
        }

        public int Evaluate()
        {
            var score = 0;

            for (int color = (int)ChessColor.White; color <= (int)ChessColor.Black; color++)
            {
                for (int piece = (int)BitPieceType.Pawn; piece <= (int)BitPieceType.King; piece++)
                {
                    var whitePieceBit = _board.Bitboard_Pieces[color, piece];
                    while (whitePieceBit != 0)
                    {
                        var square = BitHelper.BitScanForward(whitePieceBit);
                        whitePieceBit &= _helperBits.NotIndexMask[square];

                        if (color == (int)ChessColor.White)
                        {
                            score += _value[piece];
                        }
                        else
                        {
                            score -= _value[piece];
                        }
                    }
                }
            }

            return score;
        }
    }
}
