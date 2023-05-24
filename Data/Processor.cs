using System;
using MultiProcessor.DataStructures;

namespace MultiProcessor.Data
{
    public class Processor
    {
        private readonly Heap<Process> _readyQueue;
        public int TotalProcessorBusyTime { get; set; }
        public int TotalProcessorIdleTime { get; set; }
        public Processor(Func<Process, Process, int> cmpFunction)
        {
            _readyQueue = new Heap<Process>(cmpFunction);
        }
        public void Add(Process process)
        {
            _readyQueue.Add(process);
        }
        public Process GetNext()
        {
            if (_readyQueue.Size() == 0)
                throw new InvalidOperationException("Ready queue is empty");
            return _readyQueue.Remove();
        }
    }
}