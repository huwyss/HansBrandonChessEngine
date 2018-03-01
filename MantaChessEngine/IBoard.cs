namespace MantaChessEngine
{
    public interface IBoard
    {
        Definitions.ChessColor SideToMove { get; set; }
        History History { get; set; }
        IMove LastMove { get; }
        int EnPassantFile { get; }
        int EnPassantRank { get; }
        bool CastlingRightWhiteQueenSide { get; }
        bool CastlingRightWhiteKingSide { get; }
        bool CastlingRightBlackQueenSide { get; }
        bool CastlingRightBlackKingSide { get; }
        bool WhiteDidCastling { get; set; }
        bool BlackDidCastling { get; set; }
        string GetString { get; }
        string GetPrintString { get; }

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
        /// Returns the chess piece.
        /// </summary>
        /// <param name="fileChar">'a' to 'h'</param>
        /// <param name="rank">1 to 8</param>
        /// <returns></returns>
        Piece GetPiece(char fileChar, int rank);

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        void SetPiece(Piece piece, int file, int rank);

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="fileChar">'a' to 'h'</param>
        /// <param name="rank">1 to 8</param>
        void SetPiece(Piece piece, char fileChar, int rank);

        /// <summary>
        /// Do a move and update the board
        /// </summary>
        void Move(IMove nextMove);

        Definitions.ChessColor GetColor(int file, int rank);
        bool IsWinner(Definitions.ChessColor color);
        void Back();
    }
}