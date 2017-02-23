using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPVCalculator.Services
{
    public interface ILoggingProvider
    {
        void Log(string Message);
    }

}