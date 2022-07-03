using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestrator.Common.Enums
{
    public enum EventState : byte
    {
        Pending = 1,
        Finished = 2,
        Failed = 3
    }
}
