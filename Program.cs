using System;
using System.Collections.Generic;
using MultiProcessor.Data;
namespace MultiProcessor
{
    internal class Program
    { 
        public void Test()
        {
            var processes = new List<Process> 
            {
                new Process
                {
                    Id = 1, 
                    State = ProcessState.New,
                    ArrivalTime = 1,
                    CpuTime = 3
                },
                new Process
                {
                    Id = 2, 
                    State = ProcessState.New,
                    ArrivalTime = 1,
                    CpuTime = 3
                },
                new Process
                {
                    Id = 3, 
                    State = ProcessState.New,
                    ArrivalTime = 1,
                    CpuTime = 3
                },
                new Process
                {
                    Id = 4, 
                    State = ProcessState.New,
                    ArrivalTime = 1,
                    CpuTime = 8
                },
                new Process
                {
                    Id = 5, 
                    State = ProcessState.New,
                    ArrivalTime = 1,
                    CpuTime = 7
                },
                
            };
            var multiProcessor = new Scheduler();
            multiProcessor.AddProcessor(ProcessorSchedulingType.ShortestJobFirst);
            multiProcessor.AddProcessor(ProcessorSchedulingType.FirstComeFirstService);
            multiProcessor.AddProcessor(ProcessorSchedulingType.RoundRobin);
            foreach (var process in processes)
            {
                multiProcessor.AddProcess(process);
            }
            multiProcessor.Start();
        }
        
        public static void Main(string[] args)
        {
        }
    }
}