using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiProcessor.Data
{
    public class Scheduler
    {
        private List<Processor> _processors;
        public int CurrentTime { get; set; } = 0;
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
        }
        public void AddProcessor(ProcessorSchedulingType processorSchedulingType)
        {
            Console.WriteLine("add a new processor of type {0}", processorSchedulingType);
            _processors.Add(ProcessorFactory.GetProcessor(processorSchedulingType));
        }
        public void AddProcess(Process process)
        {
            if (_processors.Count == 0)
            {
                throw new InvalidOperationException("No processor available");
            }
            var processor = _processors.OrderBy(processor => processor.TotalProcessesCpuTime).First();
            Console.WriteLine("add a new process to a processor of type {0}, processor CT {2}, \nprocess Id {1}: process Arrival time {3}, process cp time{4}", processor.ProcessorType, process.Id, processor.TotalProcessesCpuTime, process.ArrivalTime, process.CpuTime);
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

        public void Start()
        {
            while (CurrentTime < 20)
            {
                Console.WriteLine("current Time {0}", CurrentTime);
                foreach (var processor in _processors)
                {
                    if (processor.IsIdeal()) 
                        continue;
                    var process = processor.GetNextWithOutRemove();
                    if (process.ArrivalTime > CurrentTime) 
                        continue;
                    if (process.CpuTime == 0)
                    {
                        process.State = ProcessState.Trm;
                        processor.TerminateProcess(process.Id);
                        continue;
                    }
                    
                    processor.RunProcess(1);
                }
                CurrentTime++;
            }
        }
    }
}