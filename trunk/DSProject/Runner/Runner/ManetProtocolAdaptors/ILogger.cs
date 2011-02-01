using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManetProtocolAdaptors
{
    public interface ILogger
    {
        void addLineToLog(String line);
    }
}
