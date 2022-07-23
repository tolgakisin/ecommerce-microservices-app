using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestrator.Common.Enums
{
    public enum EventState : byte
    {
        Started = 1,
        Finished = 2,
        Failed = 3,
        ReverseStarted = 4,
        ReverseFinished = 5,
        ReverseFailed = 6,
        Error = 7,
        NotFound = 8
    }
}
