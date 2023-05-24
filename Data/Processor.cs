using System;
using MultiProcessor.DataStructures;

namespace MultiProcessor.Data
{
    public enum ProcessorSchedulingType
    {
        FirstComeFirstService, 
        ShortestJobFirst,
        RoundRobin
    }
    public class Processor
    {
        private readonly Heap<Process> _readyQueue;
        public int TotalProcessorBusyTime { get; set; }
        public int TotalProcessorIdleTime { get; set; }
        public int TotalProcessesCpuTime { get; set; }
        public ProcessorSchedulingType ProcessorType { get; set; }
        public Processor(Func<Process, Process, int> cmpFunction)
        {
            _readyQueue = new Heap<Process>(cmpFunction);
        }
        public void Add(Process process)
        {
            TotalProcessesCpuTime += process.CpuTime;
            _readyQueue.Add(process);
        }
        public Process GetNext()
        {
            if (_readyQueue.Size() == 0)
                throw new InvalidOperationException("Ready queue is empty");
            var process = _readyQueue.Remove();
            TotalProcessesCpuTime -= process.CpuTime;
            return process;
        }
    }
}