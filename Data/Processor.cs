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
            return process;
        }
        public Process? GetProcess(int processId)
        {
            var allProcesses = _readyQueue.GetList();
            foreach (var process in allProcesses)
            {
                if (process.Id == processId)
                {
                    return process;
                }
            }
            return null;
        }

        public void TerminateProcess(int processId)
        {
            var allProcesses = _readyQueue.GetList();
            var index = 0;
            var found = false;
            foreach (var process in allProcesses)
            {
                if (process.Id == processId)
                {
                    found = true;
                    TotalProcessesCpuTime -= process.CpuTime;
                    break;
                }
                index++;
            }
            if (!found) return;
            Console.WriteLine("terminate a process in processor of type : {0}", ProcessorType);
            Console.WriteLine("process Id {0}", processId);
            _readyQueue.RemoveByIndex(index + 1);
        }

        public bool IsIdeal()
        {
            return _readyQueue.Size() == 0;
        }

        public void RunProcess(int time)
        {
            var process = GetNext();
            TotalProcessesCpuTime -= time;
            process.CpuTime -= time;
            Console.WriteLine("running a process in processor of type : {0}", ProcessorType);
            Console.WriteLine("process Id {0}, process cp time {1}", process.Id, process.CpuTime);
            TotalProcessorBusyTime += time;
            Add(process);
        }

        public Process GetNextWithOutRemove()
        {
            if (_readyQueue.Size() == 0)
                throw new InvalidOperationException("Ready queue is empty");
            var process = _readyQueue.Peek();
            return process;
        }
    }
}