using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPVCalculator.Services
{
    public interface IStateProvider
    {
        StateSettings this[string key]
        {
            get;
            set;
        }

    }
}