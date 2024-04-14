using System;

namespace TEMP
{
    public class TestBaseEventClass
    {
        public event Action TestEvent;

        protected void InvokeTestEvent()
        {
            TestEvent?.Invoke();
        }
    }

    public class Imp : TestBaseEventClass
    {
        public void F()
        {

            InvokeTestEvent();
        }
    }
}