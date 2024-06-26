﻿using HBCommon;

namespace HansBrandonBitboardEngine
{
    public interface ISearchableBitboard : ISearchableBoard<BitMove>
    {
    }

    public interface IBitBoard : ISearchableBitboard
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

        void ClearAllPieces();

        /// <summary>
        /// Returns the chess piece.
        /// </summary>
        BitPiece GetPiece(Square square);

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        void SetPiece(ChessColor color, PieceType piece, Square square);

        /// <summary>
        /// Removes the piece from the square.
        /// </summary>
        /// <param name="square"></param>
        void RemovePiece(Square square);

        /// <summary>
        /// Do a move and update the board
        /// </summary>
        //// void Move(BitMove nextMove);

        /// <summary>
        /// Takes the last move back
        /// </summary>
        //// void Back();
    }
}