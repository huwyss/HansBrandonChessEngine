using System;

namespace MantaCommon
{
    public class MantaEngineException : Exception
    {
        public MantaEngineException(string message)
            : base(message)
        { }
    }
}
