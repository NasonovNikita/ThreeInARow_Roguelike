using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Other
{
    public class ProcessHolder
    {
        private List<SmartCoroutine> _processes = new();

        public void Add(SmartCoroutine process)
        {
            _processes.Add(process);
        }

        public void Clear()
        {
            _processes = new List<SmartCoroutine>();
        }

        public IEnumerator WaitUntilAllFinished()
        {
            var processesAtThisMoment = _processes.ToList();
            foreach (var process in _processes.ToList())
            {
                yield return process;
            }

            if (_processes.Exists(process =>
                    !processesAtThisMoment.Contains(process)))
            {
                yield return WaitUntilAllFinished();
            }
        }
    }
}