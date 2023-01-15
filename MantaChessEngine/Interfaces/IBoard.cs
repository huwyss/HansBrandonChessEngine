using MantaCommon;

namespace MantaChessEngine
{
    public interface IBoard : ISearchableBoard<IMove>
    {
        BoardState BoardState { get; }
        
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
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        /// <returns></returns>
        Piece GetPiece(Square square);

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        void SetPiece(Piece piece, Square square);

        void RemovePiece(Square square);

        /// <summary>
        /// Do a move and update the board
        /// </summary>
        ////void Move(IMove nextMove);

        /// <summary>
        /// Takes the last move back
        /// </summary>
        ////void Back();

        ChessColor GetColor(Square square);

        Square GetKing(ChessColor color);
    }
}