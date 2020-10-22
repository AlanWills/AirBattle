using AirBattle.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAirBattle.Mocks
{
    public class MockEventListener : IEventListener
    {
        public bool WasEventRaised { get; private set; }

        public void Reset()
        {
            WasEventRaised = false;
        }

        public void OnEventRaised()
        {
            WasEventRaised = true;
        }
    }
}
