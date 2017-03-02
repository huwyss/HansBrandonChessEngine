﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public interface ISearchService
    {
        IMove Search(Board board, Definitions.ChessColor color, out float score);
    }
}