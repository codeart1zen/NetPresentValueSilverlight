using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPVCalculator.Services;

namespace NPVCalculatorServiceTests
{
    public class MockLoggingProvider : ILoggingProvider
    {
        public List<string> LogMsgs = new List<string>();
        public void Log(string Message)
        {
            LogMsgs.Add(Message);
        }
    }

}
