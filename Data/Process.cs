using System;

namespace MultiProcessor.Data
{
    public enum ProcessState
    {
        New, 
        Rdy, 
        Run, 
        Blk, 
        Trm, 
        Orph
    }
    public class Process
    {
        public int Id { get; set; }
        public int ArrivalTime { get; set; }
        public int ResponseTime { get; set; }
        public int CpuTime { get; set; }
        public int TerminationTime { get; set; }
        public int TurnaroundDuration { get; set; }
        public int WaitingTime { get; set; }
        public ProcessState State { get; set; }
        
        public Process()
        {
        }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}