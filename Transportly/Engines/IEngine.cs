using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace Transportly.Engines
{
    public abstract class EngineBase
    {
        public string FilePath;
        public abstract void DoWork();
    }
}
