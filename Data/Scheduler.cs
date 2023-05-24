using System.Collections.Generic;

namespace MultiProcessor.Data
{
    public class Scheduler
    {
        private List<Processor> _processors;
        public int RoundRobinSlice { get; set; } = 1;
        
        public Scheduler()
        {
            _processors = new List<Processor>();
        }

        public void AddNewProcessor(ProcessorSchedulingType processorSchedulingType)
        {
            _processors.Add(ProcessorFactory.GetProcessor(processorSchedulingType));
        }
        public void AddProcess(Process process)
        {
            
        }
    }
}