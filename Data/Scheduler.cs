using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiProcessor.Data
{
    public class Scheduler
    {
        private List<Processor> _processors;
        private List<Process> _processes;
        public int CurrentTime { get; } = 0;
        // to migrate from RoundRobin To ShortJobFirst 
        public int RrMigrationThreshold { get; set; } = 5;
        // to migrate from FirstComeFirstService to RoundRobin 
        public int MaxWaitTimeToMigrate { get; set; } = 20;
        // time for RoundRobin 
        public int RoundRobinSlice { get; set; } = 1;
        // every StealingTime Scheduler Performs stealing operation 
        public int StealingTime { get; set; } = 10;
        public double StealLimit { get; set; } = 0.4;
        public Scheduler()
        {
            _processors = new List<Processor>();
            _processes = new List<Process>();
        }
        public void AddProcessor(ProcessorSchedulingType processorSchedulingType)
        {
            _processors.Add(ProcessorFactory.GetProcessor(processorSchedulingType));
        }
        public void AddProcess(Process process)
        {
            if (_processors.Count == 0)
            {
                throw new InvalidOperationException("No processor available");
            }
            var processor = _processors.OrderBy(processor => processor.TotalProcessesCpuTime).First();
            processor.Add(process);
        }
        public void PerformStealing()
        {
            while(true)
            {
                var shortProcessorQueue = _processors.OrderBy(processor => processor.TotalProcessesCpuTime)
                    .First();
                var longestProcessorQueue = _processors.OrderByDescending(processor => processor.TotalProcessesCpuTime)
                    .First();
                var stealLimit = (1.0 * longestProcessorQueue.TotalProcessesCpuTime - shortProcessorQueue.TotalProcessesCpuTime) / 
                                 longestProcessorQueue.TotalProcessesCpuTime;
                if (stealLimit < StealLimit)
                    break;
                var process = longestProcessorQueue.GetNext();
                shortProcessorQueue.Add(process);
            }
        }
        public void KillProcess(int processId)
        {
            foreach(var processor in _processors.
                        Where(processor=>processor.ProcessorType == ProcessorSchedulingType.FirstComeFirstService))
            {
                var process = processor.GetProcess(processId);
                if (process == null) continue;
                if (process.State != ProcessState.Rdy && process.State != ProcessState.Run)
                    break;
                // this is the process to be [removed / killed] 
                processor.TerminateProcess(processId);
            }
        }
    }
}