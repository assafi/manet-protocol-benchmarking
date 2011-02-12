using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManetProtocolAdaptors
{
    public interface ILogger
    {
        /// <summary>
        /// Adds a line to the logger.
        /// </summary>
        /// <param name="line"> A line to be added to the log</param>
        /// <returns></returns>
        void addLineToLog(String line);
    }
}
