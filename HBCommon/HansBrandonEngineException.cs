using System;

namespace HBCommon
{
    public class HansBrandonEngineException : Exception
    {
        public HansBrandonEngineException(string message)
            : base(message)
        { }
    }

    public class HansBrandonSearchAbortedException : HansBrandonEngineException
    {
        public int AbortedOnLevel { get; set; }

        public HansBrandonSearchAbortedException(string message, int abortedOnLevel)
        : base(message)
        {
            AbortedOnLevel = abortedOnLevel;
        }
    }
}
