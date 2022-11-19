using System;

namespace MantaChessEngine
{
    public class MantaEngineException : Exception
    {
        public MantaEngineException(string message)
            : base(message)
        { }
    }
}
