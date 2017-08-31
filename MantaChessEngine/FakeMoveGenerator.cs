using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class FakeMoveGenerator : IMoveGenerator
    {
        public List<MoveBase> ReturnsWhiteGetAllMoves { get; set; }
        public List<MoveBase> ReturnsBlackGetAllMoves { get; set; }
        public List<MoveBase> GetAllMoves(Board board, Definitions.ChessColor color, bool includeCastling = true)
        {
            return color == Definitions.ChessColor.White ? ReturnsWhiteGetAllMoves : ReturnsBlackGetAllMoves;
        }

        public List<MoveBase> ReturnsGetMoves { get; set; }
        public List<MoveBase> GetMoves(Board board, int file, int rank, bool includeCastling = true)
        {
            return ReturnsGetMoves;
        }

        public bool ReturnsIsValid { get; set; }
        public bool IsMoveValid(Board board, MoveBase move)
        {
            return ReturnsIsValid;
        }

        public bool ReturnsIsAttacked { get; set; }
        public bool IsAttacked(Board board, Definitions.ChessColor color, int file, int rank)
        {
            return ReturnsIsAttacked;
        }

        public bool ReturnsIsCheck { get; set; }
        public bool IsCheck(Board board, Definitions.ChessColor color)
        {
            return ReturnsIsCheck;
        }
    }
}
