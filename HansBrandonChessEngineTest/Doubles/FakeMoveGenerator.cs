using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HansBrandonChessEngine;
using HBCommon;

namespace HansBrandonChessEngineTest
{
    public class FakeMoveGenerator : IMoveGenerator
    {
        public List<IMove> ReturnsWhiteGetAllMoves { get; set; }
        public List<IMove> ReturnsBlackGetAllMoves { get; set; }
        public IEnumerable<IMove> GetAllMoves(IBoard board, ChessColor color, bool includeCastling = true, bool includePawnMoves = true)
        {
            return color == ChessColor.White ? ReturnsWhiteGetAllMoves : ReturnsBlackGetAllMoves;
        }

        public List<IMove> ReturnsGetMoves { get; set; }
        public List<IMove> GetMoves(IBoard board, int file, int rank, bool includeCastling = true)
        {
            return ReturnsGetMoves;
        }

        public bool ReturnsIsValid { get; set; }
        public bool IsMoveValid(IBoard board, IMove move)
        {
            return ReturnsIsValid;
        }

        public bool ReturnsIsAttacked { get; set; }
        public bool IsAttacked(IBoard board, ChessColor color, int file, int rank)
        {
            return ReturnsIsAttacked;
        }

        public bool ReturnsIsCheck { get; set; }
        public bool IsCheck(IBoard board, ChessColor color)
        {
            return ReturnsIsCheck;
        }

        public IEnumerable<IMove> GetLegalMoves(IBoard board, ChessColor color)
        {
            throw new NotImplementedException();
        }
    }
}
