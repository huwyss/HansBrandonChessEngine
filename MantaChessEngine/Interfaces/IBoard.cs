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
        Piece GetPiece(int file, int rank);

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        void SetPiece(Piece piece, int file, int rank);

        /// <summary>
        /// Do a move and update the board
        /// </summary>
        ////void Move(IMove nextMove);

        /// <summary>
        /// Takes the last move back
        /// </summary>
        ////void Back();

        ChessColor GetColor(int file, int rank);

        Position GetKing(ChessColor color);
    }
}