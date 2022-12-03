using static MantaChessEngine.Definitions;

namespace MantaChessEngine.BitboardEngine
{
    public interface IBitBoard
    {
        IBitBoardState BoardState { get; }
        
        string GetPositionString { get; }

        string GetPrintString { get; }

        string SetFenPosition(string fen);

        string GetFenString();
        
        /// <summary>
        /// Sets the initial chess position.
        /// </summary>
        void SetInitialPosition();

        void SetPosition(string position);

        /// <summary>
        /// Returns the chess piece.
        /// </summary>
        BitPiece GetPiece(Square square);

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        void SetPiece(BitColor color, BitPieceType piece, Square square);

        /// <summary>
        /// Do a move and update the board
        /// </summary>
        void Move(BitMove nextMove);

        /// <summary>
        /// Takes the last move back
        /// </summary>
        void Back();
    }
}