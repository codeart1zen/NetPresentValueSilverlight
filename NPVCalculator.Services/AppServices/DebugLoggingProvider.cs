using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NPVCalculator.Services
{
    public class DebugLoggingProvider : ILoggingProvider
    {
        public void Log(string Message)
        {
            //Trace.WriteLine(Message);
            Debug.WriteLine(Message);
        }
    }
}
