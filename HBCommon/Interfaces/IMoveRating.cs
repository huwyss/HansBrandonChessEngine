using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCommon
{
    public interface IMoveRating<TMove> where TMove : IGenericMove
    {
        TMove Move { get; set; }
        IList<TMove> PrincipalVariation { get; set; }
        int Alpha { get; set; }
        int Beta { get; set; }
        int Score { get; set; }
        int EvaluationLevel { get; set; }
        bool WhiteWins { get; set; }
        bool BlackWins { get; set; }
        bool Stallmate { get; set; }
        IMoveRating<TMove> Clone();
        int EvaluatedPositions { get; set; }
        int Depth { get; set; }
        int SelectiveDepth { get; set; }
        int PruningCount { get; set; }
        bool IsEquallyGood(IMoveRating<TMove> otherRating);
        bool IsBetter(ChessColor color, IMoveRating<TMove> otherRating);
        bool SearchAborted { get; set; }
    }
}
