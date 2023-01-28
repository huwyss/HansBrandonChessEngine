using System;

namespace MantaCommon
{
    public class MantaEngineException : Exception
    {
        public MantaEngineException(string message)
            : base(message)
        { }
    }

    public class MantaSearchAbortedException : MantaEngineException
    {
        public int AbortedOnLevel { get; set; }

        public MantaSearchAbortedException(string message, int abortedOnLevel)
        : base(message)
        {
            AbortedOnLevel = abortedOnLevel;
        }
    }
}
