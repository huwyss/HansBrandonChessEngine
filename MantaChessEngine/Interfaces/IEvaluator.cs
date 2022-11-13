﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public interface IEvaluator
    {
        /// <summary>
        /// Returns the score for white and black.
        /// </summary>
        int Evaluate(IBoard board);
    }
}
