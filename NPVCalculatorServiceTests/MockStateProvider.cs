using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPVCalculator.Services;

namespace NPVCalculatorServiceTests
{
    class MockStateProvider : IStateProvider
    {
        public Dictionary<string, StateSettings> _state = new Dictionary<string, StateSettings>();

        public StateSettings this[string key]
        {
            get
            {
                return _state[key];
            }
            set
            {
                _state[key] = value;
            }
        }
    }
}
