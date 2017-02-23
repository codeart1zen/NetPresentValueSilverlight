using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPVCalculator.Services
{
    public class AspNetInMemoryStateProvider : IStateProvider
    {
        public StateSettings this[string SessionId]
        {
            get
            {
                return (HttpContext.Current.Application[SessionId] as StateSettings);
            }
            set
            {                
                HttpContext.Current.Application[SessionId] = value;

            }
        }
   }
}